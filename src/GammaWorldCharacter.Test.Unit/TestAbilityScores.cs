using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GammaWorldCharacter.Test.Unit.Origins;
using NUnit.Framework.Constraints;

namespace GammaWorldCharacter.Test.Unit
{
    [TestFixture]
    public class TestAbilityScores
    {
        [TestCase(ScoreType.Strength, ScoreType.Constitution, 18, 16, 14, 12, 10, 8)]
        [TestCase(ScoreType.Strength, ScoreType.Strength, 20, 14, 12, 10, 8, 6)]
        [TestCase(ScoreType.Intelligence, ScoreType.Dexterity, 14, 12, 16, 18, 10, 8)]
        [TestCase(ScoreType.Wisdom, ScoreType.Charisma, 14, 12, 10, 8, 18, 16)]
        public void Test_AbilityScore_Creation(ScoreType primaryOriginAbilityScore, ScoreType secondaryOriginAbilityScore,
            int expectedStrength, int expectedConstitution, int expectedDexterity, int expectedIntelligence, int expectedWisdom, int expectedCharisma)
        {
            AbilityScores abilityScores;

            abilityScores = new AbilityScores(new NullOrigin(primaryOriginAbilityScore), new NullOrigin(secondaryOriginAbilityScore),
                new int[]{ 14, 12, 10, 8, 6});

            Assert.That(abilityScores[ScoreType.Strength], Is.EqualTo(expectedStrength), "Incorrect Strength");
            Assert.That(abilityScores[ScoreType.Constitution], Is.EqualTo(expectedConstitution), "Incorrect Constitution");
            Assert.That(abilityScores[ScoreType.Dexterity], Is.EqualTo(expectedDexterity), "Incorrect Dexterity");
            Assert.That(abilityScores[ScoreType.Intelligence], Is.EqualTo(expectedIntelligence), "Incorrect Intelligence");
            Assert.That(abilityScores[ScoreType.Wisdom], Is.EqualTo(expectedWisdom), "Incorrect Wisdom");
            Assert.That(abilityScores[ScoreType.Charisma], Is.EqualTo(expectedCharisma), "Incorrect Charisma");
        }

        [Test]
        public void Test_AbilityScoreCreation_NullPrimaryOrigin()
        {
            Assert.That(() => new AbilityScores(null, new NullOrigin(), new int[] { 14, 12, 10, 8, 6 }),
                Throws.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("primaryOrigin"));
        }

        [Test]
        public void Test_AbilityScoreCreation_NullSecondaryOrigin()
        {
            Assert.That(() => new AbilityScores(new NullOrigin(), null, new int[] { 14, 12, 10, 8, 6 }),
                Throws.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("secondaryOrigin"));
        }

        [Test]
        public void Test_AbilityScoreCreation_NullAbilityScores()
        {
            Assert.That(() => new AbilityScores(new NullOrigin(), new NullOrigin(), null),
                Throws.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("abilityScores"));
        }

        [Test]
        public void Test_AbilityScoreCreation_AbilityScoreLengthIs4WhenSameOriginPrimaryAbilityScore()
        {
            Assert.That(() => new AbilityScores(new NullOrigin(ScoreType.Strength), new NullOrigin(ScoreType.Strength), new int[] { 14, 12, 10, 8 }),
                Throws.ArgumentException.And.Property("ParamName").EqualTo("abilityScores"));
        }

        [Test]
        public void Test_AbilityScoreCreation_AbilityScoreLengthIs5WhenDifferentOriginPrimaryAbilityScores()
        {
            Assert.That(() => new AbilityScores(new NullOrigin(ScoreType.Strength), new NullOrigin(ScoreType.Intelligence), new int[] { 14, 12, 10 }),
                Throws.ArgumentException.And.Property("ParamName").EqualTo("abilityScores"));
        }

        [TestCase(20, true)]
        [TestCase(19, true)]
        [TestCase(18, false)]
        [TestCase(4, false)]
        [TestCase(3, false)]
        [TestCase(2, true)]
        [TestCase(0, true)]
        [TestCase(-1, true)]
        public void Test_AbilityScoreCreation_InvalidAbilityScore(int score, bool exceptionExpected)
        {
            Constraint constraint;

            constraint = exceptionExpected ?
                (Constraint) Throws.ArgumentException.And.Property("ParamName").EqualTo("abilityScores") :
                (Constraint) Throws.Nothing;
            Assert.That(
                () => new AbilityScores(new NullOrigin(ScoreType.Strength), new NullOrigin(ScoreType.Intelligence), new int[] { score, 12, 10, 8 }),
                constraint);
        }
    }
}
