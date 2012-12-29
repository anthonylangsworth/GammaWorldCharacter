using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Gear.Weapons;

namespace GammaWorldCharacter.Powers
{
    /// <summary>
    /// The attack bonus for a <see cref="BasicAttack"/>.
    /// </summary>
    public class BasicAttackAttackBonus: WeaponAttackBonus
    {
        /// <summary>
        /// Create a new <see cref="BasicAttackAttackBonus"/>.
        /// </summary>
        public BasicAttackAttackBonus()
            : base("Basic Attack attack bonus", new ScoreType[0], Hand.Main)
        {
            // Do nothing
        }

        /// <summary>
        /// Depend on each of the <see cref="Weapon.BasicAttackAbilityScores"/>.
        /// </summary>
        /// <param name="addDependency">
        /// The list of depdencies.
        /// </param>
        /// <param name="character">
        /// The <see cref="Character"/> to add dependencies for.
        /// </param>
        protected override void AddDependencies(Action<ModifierSource, ModifierSource> addDependency, Character character)
        {
            Weapon weapon;

            base.AddDependencies(addDependency, character);

            weapon = character.GetHeldItem<Weapon>(Hand);
            if (weapon != null)
            {
                foreach (ScoreType scoreType in weapon.BasicAttackAbilityScores)
                {
                    addDependency(character[scoreType], this);
                }
            }
        }

        /// <summary>
        /// Add modifiers.
        /// </summary>
        /// <param name="stage">
        /// The stage during character update this is called.
        /// </param>
        /// <param name="addModifier">
        /// Add modifiers by calling this method.
        /// </param>
        /// <param name="character">
        /// The character to add modifiers for.
        /// </param>
        protected override void AddModifiers(CharacterUpdateStage stage, Action<Modifier> addModifier, Character character)
        {
            Weapon weapon;

            base.AddModifiers(stage, addModifier, character);

            weapon = character.GetHeldItem<Weapon>(Hand);
            if (weapon != null)
            {
                // Add the greatest ability score's modifier
                BonusHelper.AddGreatestScoreModifier(this, addModifier, character, weapon.BasicAttackAbilityScores);
            }
        }
    }
}
