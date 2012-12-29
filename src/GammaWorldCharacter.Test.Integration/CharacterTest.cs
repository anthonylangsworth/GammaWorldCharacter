using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GammaWorldCharacter;
using GammaWorldCharacter.Gear;
using GammaWorldCharacter.Origins;
using GammaWorldCharacter.Powers;
using GammaWorldCharacter.Traits;
using GammaWorldCharacter.Samples;

namespace GammaWorldCharacter.Test.Integration
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public abstract class CharacterTest
    {
        public virtual void TestScore(ScoreType scoreType, int expectedValue)
        {
            Assert.That(Character[scoreType].Total, Is.EqualTo(expectedValue),
                string.Format("Incorrect {0}", scoreType.ToString()));
        }

        public virtual void TestAttackPower(Type type, int attack, int attackBonus, int damageDiceCount, DiceType damageDiceType, int damageBonus, string additionalText)
        {
            AttackPower attackPower;

            attackPower = Character.GetPowers().Where(x => (x.GetType() == type)).First() as AttackPower;
            Assert.NotNull(attackPower, "Attack power not found");
            Assert.GreaterOrEqual(attack, 0, "Attack index is negative");
            Assert.Less(attack, attackPower.Attacks.Count, "Attack index is greater than the number of the power's attacks");

            Assert.AreEqual(attackBonus, attackPower.Attacks[attack].AttackBonus.Total, "Attack bonuses differ");
            if (damageDiceCount > 0)
            {
                Assert.AreEqual(damageDiceCount, attackPower.Attacks[attack].Damage.Dice.Number, "Damage dice count differs");
                Assert.AreEqual(damageDiceType, attackPower.Attacks[attack].Damage.Dice.DiceType, "Damage dice type differs");
                Assert.AreEqual(damageBonus, attackPower.Attacks[attack].DamageBonus.Total, "Damage differs");
            }
            Assert.AreEqual(additionalText, attackPower.Attacks[attack].AdditionalText, "Additional text differs");
        }

        public virtual void TestUtilityPower(Type type)
        {
            UtilityPower utilityPower;

            utilityPower = Character.GetPowers().Where(x => (x.GetType() == type)).First() as UtilityPower;
            Assert.NotNull(utilityPower, "Utility power not found");
        }

        public virtual void TestPower(Type type, ActionType actionType, AttackType attackType, string range, DamageTypes damageTypes, string effect,
            EffectTypes effectTypes, PowerFrequency frequency, PowerSource powerSource, string trigger)
        {
            Power power;

            power = Character.GetPowers().Where(x => (x.GetType() == type)).First();
            Assert.NotNull(power, "Power not found");

            Assert.AreEqual(actionType, power.Action);
            Assert.AreEqual(attackType, power.AttackTypeAndRange.AttackType);
            Assert.AreEqual(range, power.AttackTypeAndRange.Range);
            Assert.AreEqual(damageTypes, power.DamageTypes);
            Assert.AreEqual(effect != null, power.HasEffect);
            if (effect != null)
            {
                Assert.AreEqual(effect, power.Effect);
            }
            Assert.AreEqual(effectTypes, power.EffectTypes);
            Assert.AreEqual(frequency, power.Frequency);
            Assert.AreEqual(powerSource, power.PowerSource);
            Assert.AreEqual(trigger != null, power.HasTrigger);
            if (trigger != null)
            {
                Assert.AreEqual(trigger, power.Trigger);
            }
        }

        /// <summary>
        /// The character to test.
        /// </summary>
        protected abstract Character Character
        {
            get;
        }
    }
}
