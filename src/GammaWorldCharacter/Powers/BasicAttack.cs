using System;
using System.Collections.Generic;
using GammaWorldCharacter.Gear.Weapons;

namespace GammaWorldCharacter.Powers
{
    /// <summary>
    /// A basic attack.
    /// </summary>
    public class BasicAttack: WeaponAttackPower
    {
        /// <summary>
        /// Create a new <see cref="BasicAttack"/>,
        /// </summary>
        public BasicAttack()
            : base("Basic Attack", null, 1) 
        {
            SetDescription("For you, this is the most natural thing in the world. For your target, it's a really bad day.");
            SetPowerDetails(PowerFrequency.AtWill, PowerSource.None,
                DamageTypes.Physical, EffectTypes.None, ActionType.Standard, null);
            SetAttackTypeAndRange(AttackType.Melee, "1"); 

            AddAttack(new WeaponAttackDetails("One creature",
                new BasicAttackAttackBonus(),
                new WeaponDamage("Basic Attack damage", Hand.Main, 1),
                new BasicAttackDamageBonus(),
                ScoreType.ArmorClass, new ModifierSource[0], "physical damage.", null));
        }

        /// <summary>
        /// Set the <see cref="AttackTypeAndRange"/>.
        /// </summary>
        /// <param name="addDependency">
        /// </param>
        /// <param name="character"></param>
        protected override void AddDependencies(Action<ModifierSource, ModifierSource> addDependency, Character character)
        {
            Weapon weapon;

            base.AddDependencies(addDependency, character);

            weapon = character.GetHeldItem<Weapon>(Hand.Main);
            if (weapon is RangedWeapon)
            {
                SetAttackTypeAndRange(AttackType.Ranged, ((RangedWeapon)weapon).Range.ToString());
            }
            else
            {
                // This is needed for subsequent updates
                SetAttackTypeAndRange(AttackType.Melee, "1");
            }
        }

        /// <summary>
        /// Can the specified <see cref="Character"/> use this power at this time?
        /// </summary>
        /// <param name="character">
        /// The Character to test.
        /// </param>
        /// <returns>
        /// True if the ability can be used, false otherwise.
        /// </returns>
        public override bool IsUsable(Character character)
        {
            return base.IsUsable(character)
                && character.GetHeldItem<Weapon>(Hand.Main) != null;
        }
    }
}
