using System;
using System.Collections.Generic;
using GammaWorldCharacter.Gear.Weapons;
using GammaWorldCharacter.Origins;

namespace GammaWorldCharacter.Powers
{
    /// <summary>
    /// An attack power that is only usable when the character is wielding a weapon.
    /// </summary>
    public class WeaponAttackPower : AttackPower
    {
        /// <summary>
        /// Create an <see cref="WeaponAttackPower"/>.
        /// </summary>
        /// <param name="name">
        /// The power's name.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="name"/> can be null.
        /// </exception>
        protected WeaponAttackPower(string name)
            : base(name)
        {
            // Do nothing
        }

        /// <summary>
        /// Is this power usable?
        /// </summary>
        /// <param name="character">
        /// The <see cref="Character"/> to check.
        /// </param>
        /// <returns>
        /// True if the power is usable, false if not.
        /// </returns>
        public override bool IsUsable(Character character)
        {
            return base.IsUsable(character) && character.GetHeldItem<Weapon>(Hand.Main) != null;
        }

        /// <summary>
        /// Create a standard attack for this type of attack power.
        /// </summary>
        /// <remarks>
        /// This method uses <see cref="WeaponAttackBonus"/> for the attack bonus, <see cref="WeaponDamage"/> for
        /// the damage and <see cref="WeaponDamageBonus"/> for the damage bonus. The ability score bonus
        /// is added to both attack and damage rolls.
        /// </remarks>
        /// <param name="target">
        /// What this attack affects.
        /// </param>
        /// <param name="weaponHand">
        /// The hand the weapon being used is held in, usually Hand.Main.
        /// </param>
        /// <param name="abilityScore">
        /// The ability score to add it's modifier to attack and damage rolls, usually ScoreType.Strength.
        /// </param>
        /// <param name="attackedDefense">
        /// The defense being attacked, usually ScoreType.ArmorClass.
        /// </param>
        /// <param name="damageDiceMultiplier">
        /// The number of times the weapon damage dice are rolled.
        /// </param>
        /// <param name="levelMultiplier">
        /// Most powers add the character's level to damage, meaning this should be 1.
        /// </param>
        /// <param name="additionalText">
        /// Text after the damage score. If the power has no additional affects, this is usually
        /// "damage".
        /// </param>
        /// <param name="additionalScores">
        /// Additional scores used by the power.
        /// </param>
        /// <param name="missText">
        /// A description of the attack's effect on a miss or null if there is no effect on a miss.
        /// </param>
        /// <returns>
        /// An <see cref="AttackDetails"/> representing the attack.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// No argument except for <paramref name="missText"/> can be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="abilityScore"/> must be an abilty score and <paramref name="attackedDefense"/> must 
        /// be a defense.
        /// </exception>
        protected virtual void AddAttack(string target, Hand weaponHand, ScoreType abilityScore,
            ScoreType attackedDefense, int damageDiceMultiplier, int levelMultiplier, string additionalText, 
            string missText, params ModifierSource[] additionalScores)
        {
            if (string.IsNullOrEmpty(target))
            {
                throw new ArgumentNullException("target");
            }
            if (!ScoreTypeHelper.IsAbilityScore(abilityScore))
            {
                throw new ArgumentException("Not an ability score", "abilityScore");
            }
            if (!ScoreTypeHelper.IsDefense(attackedDefense))
            {
                throw new ArgumentException("Not a defense", "attackedDefense");
            }
            if (damageDiceMultiplier <= 0)
            {
                throw new ArgumentException("Must be positive", "damageDiceMultiplier");
            }
            if (string.IsNullOrEmpty(additionalText))
            {
                throw new ArgumentNullException("additionalText");
            }
            if (additionalScores == null)
            {
                throw new ArgumentNullException("additionalScores");
            }
            if (Array.IndexOf(additionalScores, null) != -1)
            {
                throw new ArgumentNullException("additionalScores", "One or more elements are null");
            }

            AddAttack(new WeaponAttackDetails(target, 
                new WeaponAttackBonus(string.Format("{0} hand {1} vs {2} '{3}' attack bonus", weaponHand, abilityScore, attackedDefense, Name), 
                    new ScoreType[] { abilityScore }, weaponHand),
                new WeaponDamage(string.Format("{0} hand {1} vs {2} '{3}' damage", weaponHand, abilityScore, attackedDefense, Name), 
                    weaponHand, damageDiceMultiplier),
                new WeaponDamageBonus(string.Format("{0} hand {1} vs {2} '{3}' damage bonus", weaponHand, abilityScore, attackedDefense, Name), 
                    new ScoreType[] { abilityScore }, weaponHand, levelMultiplier),
                attackedDefense, additionalScores, additionalText, missText));
        }
    }
}
