using System;
using System.Collections.Generic;
using GammaWorldCharacter.Gear.Weapons;

namespace GammaWorldCharacter.Powers
{
    /// <summary>
    /// An <see cref="AttackDetails">attack</see> made with a <see cref="Weapon"/>.
    /// </summary>
    public class WeaponAttackDetails: AttackDetails
    {
        /// <summary>
        /// Create a new <see cref="WeaponAttackDetails"/>.
        /// </summary>
        /// <param name="target">
        /// What the attack affects.
        /// </param>
        /// <param name="attackBonus">
        /// The <see cref="Score"/> for the attack bonus. If the attack does not use an 
        /// attack roll, this can be null. This is added to Scores if not null.
        /// </param>
        /// <param name="damage">
        /// The power's base damage. If the attack does not deal damage, this can be null.
        /// </param>
        /// <param name="damageBonus">
        /// The <see cref="Score"/> for the damage bonus. If the attack does not deal damage,
        /// this can be null. This is added to Scores if not null.
        /// </param>
        /// <param name="attackedDefense">
        /// The defense attacked. If there is no attack roll, this value is ignored.
        /// </param>
        /// <param name="additionalScores">
        /// Additional scores used by the attack, usually for additional effects like 
        /// "pushes the target Wisdom modifier squares" or "an ally gains Charisma modifier temporary hit points".
        /// </param>
        /// <param name="additionalText">
        /// Additional details of the attack included immediately after the damage bonus. This supports "{0}"
        /// being the attack bonus (if specified), "{1}" the damage (if specified), "{2}" the damage bonus (if specified)
        /// and the values in <paramref name="additionalScores"/> with the first value starting at "{3}" (assuming
        /// attack bonus, damage and damage bonus were all specified).
        /// </param>
        /// <param name="missText">
        /// A description of the effect of the power on a miss or null, if the power does nothing on a miss. This 
        /// supports the same string substitution parameters as <paramref name="additionalText"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="damage"/> and <paramref name="damageBonus"/> must be supplied or both must be null.
        /// <paramref name="additionalText"/> must also be specified. <paramref name="target"/> cannot be null.
        /// </exception>
        public WeaponAttackDetails(string target, Score attackBonus, PowerDamage damage, Score damageBonus,
            ScoreType attackedDefense, IList<ModifierSource> additionalScores, string additionalText, 
            string missText)
            : base(target, attackBonus, damage, damageBonus, attackedDefense, additionalScores, 
                additionalText, missText)
        {
            // Do nothing
        }
    }
}
