using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Gear;
using GammaWorldCharacter.Origins;
using GammaWorldCharacter.Scores;
using GammaWorldCharacter.Test.Unit.Origins;
using NUnit.Framework;

namespace GammaWorldCharacter.Test.Unit
{
    [TestFixture]
    public class TestCharacter
    {
        [Test]
        public void TestConstructor_NullAbilityScores()
        {
            Assert.That(
                () => new Character(null, new NullOrigin(), new NullOrigin(), ScoreType.Athletics),
                Throws.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("abilityScores"));
        }

        [Test]
        public void TestConstructor_NullPrimaryOrigin()
        {
            Assert.That(
                () => new Character(new int[] {15, 15, 15, 15, 15}, null, new NullOrigin(), ScoreType.Athletics),
                Throws.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("primaryOrigin"));
        }

        [Test]
        public void TestConstructor_NullSecondaryOrigin()
        {
            Assert.That(
                () => new Character(new int[] {15, 15, 15, 15, 15}, new NullOrigin(), null, ScoreType.Athletics),
                Throws.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("secondaryOrigin"));
        }

        [Test]
        public void TestConstructor_NotSkill()
        {
            Assert.That(
                () =>
                new Character(new int[] {15, 15, 15, 15, 15}, new NullOrigin(), new NullOrigin(), ScoreType.ArmorClass),
                Throws.ArgumentException.And.Property("ParamName").EqualTo("trainedSkill"));
        }

        [Test]
        public void TestConstructor()
        {
            Origin primaryOrigin = new NullOrigin();
            Origin secondaryOrigin = new NullOrigin();
            int[] abilityScores = new int[] {15, 14, 13, 12, 11};

            Character character = new Character(abilityScores, primaryOrigin, secondaryOrigin, ScoreType.Acrobatics);

            Assert.That(character.PrimaryOrigin, Is.SameAs(primaryOrigin), "Invalid Primary Origin");
            Assert.That(character.SecondaryOrigin, Is.SameAs(secondaryOrigin), "Invalid Secondary Origin");
            Assert.That(character.Strength.Total, Is.EqualTo(20));
            Assert.That(character.Constitution.Total, Is.EqualTo(15));
            Assert.That(character.Dexterity.Total, Is.EqualTo(14));
            Assert.That(character.Intelligence.Total, Is.EqualTo(13));
            Assert.That(character.Wisdom.Total, Is.EqualTo(12));
            Assert.That(character.Charisma.Total, Is.EqualTo(11));

            var skills = ScoreTypeHelper.Skills.Select(x => new
                {
                    Skill = x,
                    Trained = character.IsTrainedInSkill(x)
                });

            Assert.That(skills.Count(x => x.Trained), Is.EqualTo(1), "Multiple trained skills");
            Assert.That(skills.Single(x => x.Trained).Skill, Is.EqualTo(ScoreType.Acrobatics));
            Assert.That(character.IsTrainedInSkill(ScoreType.Acrobatics), Is.True, "Not trained in acrobatics");
        }

        [Test]
        public void TestIsTrainedInSkill_NotASkill()
        {
            Character character = new Character(new int[] { 15, 15, 15, 15, 15 }, new NullOrigin(), new NullOrigin(),
                                                ScoreType.Athletics);
            Assert.That(() => character.IsTrainedInSkill(ScoreType.ArmorClass),
                        Throws.ArgumentException.And.Property("ParamName").EqualTo("skill"));
        }

        [Test]
        public void TestName_Assignment()
        {
            const string testName = "Test";
            Character character = Character;
            Assert.That(character.Name, Is.EqualTo("Unnamed"));
            character.Name = testName;
            Assert.That(character.Name, Is.EqualTo(testName));
        }

        [Test]
        public void TestName_Null()
        {
            Character character = Character;
            Assert.That(() => character.Name = null,
                        Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void TestName_Empty()
        {
            Character character = Character;
            Assert.That(() => character.Name = string.Empty,
                        Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void TestName_Whitespace()
        {
            Character character = Character;
            Assert.That(() => character.Name = " ",
                        Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void TestPlayerName_Assignment()
        {
            const string testName = "Test";
            Character character = Character;
            Assert.That(character.PlayerName, Is.EqualTo("Unnamed"));
            character.PlayerName = testName;
            Assert.That(character.PlayerName, Is.EqualTo(testName));
        }

        [Test]
        public void TestPlayerName_Null()
        {
            Character character = Character;
            Assert.That(() => character.PlayerName = null,
                        Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void TestPlayerName_Empty()
        {
            Character character = Character;
            Assert.That(() => character.PlayerName = string.Empty,
                        Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void TestPlayerName_Whitespace()
        {
            Character character = Character;
            Assert.That(() => character.PlayerName = " ",
                        Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void TestSetEquippedItem_SlotNone()
        {
            Character character = Character;
            Assert.That(() => character.SetEquippedItem(new Item("Foo", Slot.None)), 
                Throws.ArgumentException.And.Property("ParamName").EqualTo("item"));
        }

        [Test]
        public void TestSetEquippedItem_SlotWeapon()
        {
            Character character = Character;
            Assert.That(() => character.SetEquippedItem(new Item("Foo", Slot.Weapon)),
                Throws.ArgumentException.And.Property("ParamName").EqualTo("item"));
        }

        [Test]
        public void TestSetEquippedItem_SlotBody()
        {
            Character character = Character;
            Item previousItem;
            Item newItem = new Item("Foo", Slot.Body);

            previousItem = character.SetEquippedItem(newItem);

            Assert.That(previousItem, Is.Null, "Invalid previous item");
            Assert.That(character.GetEquippedItem<Item>(Slot.Body), 
                Is.SameAs(newItem), "Invalid new item");
        }

        /// <summary>
        /// A dummy character for testing.
        /// </summary>
        private Character Character
        {
            get
            {
                return new Character(new int[] {15, 15, 15, 15, 15}, new NullOrigin(), new NullOrigin(),
                                     ScoreType.Athletics);
            }
        }
    }
}
