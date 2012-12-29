﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Origins;
using GammaWorldCharacter.Gear.Weapons;

namespace GammaWorldCharacter.Powers.Origins
{
    /// <summary>
    /// The novice Hypercognitive power Uncanny Strike.
    /// </summary>
    public class UncannyStrike : WeaponAttackPower
    {
        /// <summary>
        /// Create a new <see cref="UncannyStrike"/>.
        /// </summary>
        public UncannyStrike()
            : base("Uncanny Strike", typeof(Hypercognitive), 1)
        {
            SetDescription("With a glance, you asses your foe's weaknesses and strike to enhance that disadvantage.");
            SetPowerDetails(PowerFrequency.AtWill, PowerSource.Psi, DamageTypes.Physical, 
                EffectTypes.None, ActionType.Standard, null);
            SetAttackTypeAndRange(AttackType.Melee, "weapon");
            AddAttack("One creature", Hand.Main, ScoreType.Wisdom, ScoreType.ArmorClass, 1, 1,
                "physical damage and the target grants combat advantage until the end of your next turn", null);
        }

        /// <summary>
        /// Determine whether the attack is melee or ranged.
        /// </summary>
        /// <param name="addDependency"></param>
        /// <param name="character"></param>
        protected override void AddDependencies(Action<ModifierSource, ModifierSource> addDependency, Character character)
        {
            base.AddDependencies(addDependency, character);

            Weapon weapon;
            weapon = character.GetHeldItem<Weapon>(Hand.Main);
            if (weapon != null
                && weapon is RangedWeapon)
            {
                SetAttackTypeAndRange(AttackType.Ranged, "weapon");
            }
            else
            {
                SetAttackTypeAndRange(AttackType.Melee, "weapon");
            }
        }
    }
}
