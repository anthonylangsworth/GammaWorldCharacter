using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Powers;
using GammaWorldCharacter.Origins;

namespace GammaWorldCharacter.Levels
{
    /// <summary>
    /// Level 3, where characters get an origin utility power.
    /// </summary>
    public class Level03: Level, IPowerSource
    {
        private Power utilityPower;

        /// <summary>
        /// Create a new <see cref="Level03"/>.
        /// </summary>
        /// <param name="originChoice">
        /// The origin the character will use the utility power from.
        /// </param>
        public Level03(OriginChoice originChoice)
            : base(3)
        {
            UtilityPowerOrigin = originChoice;
        }

        /// <summary>
        /// The origin the utility power will come form.
        /// </summary>
        public OriginChoice UtilityPowerOrigin
        {
            get;
            private set;
        }

        /// <summary>
        /// Get the power from the appropriate origin.
        /// </summary>
        /// <param name="stage"></param>
        /// <param name="addModifier"></param>
        /// <param name="character"></param>
        protected override void AddModifiers(CharacterUpdateStage stage, Action<Modifier> addModifier, Character character)
        {
            base.AddModifiers(stage, addModifier, character);

            Origin origin;
            if (UtilityPowerOrigin == OriginChoice.Primary)
            {
                origin = character.PrimaryOrigin;
            }
            else
            {
                origin = character.SecondaryOrigin;
            }

            utilityPower = origin.UtilityPower;
        }

        /// <summary>
        /// The <see cref="Power"/>s supplied by this level, specifically
        /// the selected origin's utility power.
        /// </summary>
        public IEnumerable<Power> Powers
        {
            get 
            {
                if (utilityPower != null)
                {
                    yield return utilityPower;
                }
            }
        }
    }
}
