using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Powers.Effects;
using GammaWorldCharacter.Samples;
using NUnit.Framework;

namespace GammaWorldCharacter.Test.Unit.Powers.Effects
{
    [TestFixture]
    public class TestCharacterScore
    {
        [Test]
        public void TestConstructor()
        {
            ScoreType scoreType = ScoreType.Fortitude;
            CharacterScore characterScore = new CharacterScore(scoreType);

            Assert.That(characterScore.ScoreType, Is.EqualTo(scoreType));
        }

        [Test]
        public void TestGetValue_NullValue()
        {
            CharacterScore characterScore = new CharacterScore(ScoreType.Fortitude);

            Assert.That(() => characterScore.GetValue(null), 
                Throws.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("character"));
        }

        [Test]
        public void TestGetValue()
        {
            CharacterScore characterScore = new CharacterScore(ScoreType.Fortitude);

            Assert.That(characterScore.GetValue(Level01Characters.Clip),
                Is.EqualTo(17));
        }
    }
}
