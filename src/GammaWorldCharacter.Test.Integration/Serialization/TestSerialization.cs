using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Gear;
using GammaWorldCharacter.Samples;
using GammaWorldCharacter.Serialization;
using NUnit.Framework;

namespace GammaWorldCharacter.Test.Unit.Serialization
{
    [TestFixture]
    public class TestSerialization
    {
        [TestCaseSource("CharacterSource")]
        public void TestJsonSerialization(Character character)
        {
            JsonCharacterSerializer characterSerializer;
            Character newCharacter;
            string json;

            characterSerializer = new JsonCharacterSerializer();
            json = characterSerializer.Serialize(character);
            newCharacter = characterSerializer.Deserialize(json);

            Assert.That(newCharacter, Is.EqualTo(character), "Characters differ");
            Assert.That(newCharacter.GetHeldItem<Item>(Hand.Main), Is.EqualTo(character.GetHeldItem<Item>(Hand.Main)),
                "Main hands differ");
            Assert.That(newCharacter.GetHeldItem<Item>(Hand.Off), Is.EqualTo(character.GetHeldItem<Item>(Hand.Off)),
                "Main hands differ");
            Assert.That(
                Array.ConvertAll((Slot[])Enum.GetValues(typeof(Slot)), x => newCharacter.GetEquippedItem<Item>(x)),
                Is.EquivalentTo(Array.ConvertAll((Slot[])Enum.GetValues(typeof(Slot)), character.GetEquippedItem<Item>)),
                "Equipped items differ");
            Assert.That(
                newCharacter.Gear.OrderBy(x => x.Name),
                Is.EquivalentTo(character.Gear.OrderBy(x => x.Name)),
                "Carried items differ");
            Assert.That(
                newCharacter.Levels.OrderBy(x => x.Number),
                Is.EquivalentTo(character.Levels.OrderBy(x => x.Number)),
                "Levels differ");
        }

        /// <summary>
        /// The source of characters for serialization tests.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Character> CharacterSource()
        {
            using (CompositionContainer container = new CompositionContainer(
                new AssemblyCatalog("GammaWorldCharacter.Samples.dll")))
            {
                return container.GetExportedValues<Character>();
            }
        }

        /// <summary>
        /// Add a line number before each line.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string NumberLines(string text)
        {
            int i = 1;
            return string.Join("\n", text.Split('\n').Select(x => i++.ToString("000") + ' ' + x));
        }
    }
}
