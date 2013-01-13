using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers
{
    /// <summary>
    /// Second Wind.
    /// </summary>
    public class SecondWind: UtilityPower
    {
        /// <summary>
        /// Create a <see cref="SecondWind"/>.
        /// </summary>
        public SecondWind()
            : base("Second Wind")
        {
            SetPowerDetails(PowerFrequency.Encounter, PowerSource.None, DamageTypes.None, 
                EffectTypes.Healing, ActionType.Minor, null);
            SetAttackTypeAndRange(Range.Personal(Name));

            HitPointsHealed = new Score("Second Wind hit points healed", "Second Window");
            SetEffect("You heal {0} hit points and gain +2 to all defenses until the end of your next turn.", HitPointsHealed);
        }

        /// <summary>
        /// The amount of hit points healed.
        /// </summary>
        public Score HitPointsHealed
        {
            get;
            private set;
        }

        /// <summary>
        /// Add the bloodied value to the amout of hit points healed.
        /// </summary>
        /// <param name="stage">
        /// The character update stage this is called.
        /// </param>
        /// <param name="addModifier">
        /// Add modifiers by calling this method.
        /// </param>
        /// <param name="character">
        /// The character to add modifiers for.
        /// </param>
        protected override void AddModifiers(CharacterUpdateStage stage, Action<Modifier> addModifier, Character character)
        {
            base.AddModifiers(stage, addModifier, character);
            addModifier(new Modifier(character[ScoreType.Bloodied], HitPointsHealed, character[ScoreType.Bloodied].Total));
        }
    }
}
