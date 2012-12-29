using System;
using System.Collections.Generic;
using GammaWorldCharacter.Gear;
using GammaWorldCharacter.Gear.Armor;
using GammaWorldCharacter.Gear.Weapons;

namespace GammaWorldCharacter.Powers
{
    /// <summary>
    /// A damage bonus with a weapon equipped by the character.
    /// </summary>
    public class WeaponDamageBonus : AbilityPlusLevelBonus
    {
        private Hand hand;

        /// <summary>
        /// Create a new <see cref="WeaponDamageBonus"/>.
        /// </summary>
        /// <param name="name">
        /// The name of the damage bonus, usually "[Power] damage bonus".
        /// </param>
        /// <param name="abilityScores">
        /// The ability scores whose modifiers are added to the bonus.
        /// </param>
        /// <param name="hand">
        /// The hand holding the weapon this is the damage bonus for.
        /// </param>
        /// <param name="levelMultiplier">
        /// Most powers add the character's level to the damage (this should be 1) but 
        /// some omit it (this should be zero) or a multiple (this should be the multiplier).
        /// </param>
        public WeaponDamageBonus(string name, IList<ScoreType> abilityScores, Hand hand, int levelMultiplier)
            : base(name, abilityScores, levelMultiplier)
        {
            this.hand = hand;
        }

        /// <summary>
        /// The hand of the weapon this is the damage bonus for.
        /// </summary>
        public Hand Hand
        {
            get
            {
                return hand;
            }
        }
    }
}
