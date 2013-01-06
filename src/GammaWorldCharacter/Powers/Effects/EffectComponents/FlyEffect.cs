using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers.Effects.EffectComponents
{
    /// <summary>
    /// The target can fly.
    /// </summary>
    public class FlyEffect: EffectComponent
    {
        /// <summary>
        /// Create a new <see cref="FlyEffect"/>.
        /// </summary>
        /// <param name="target">
        /// The <see cref="Target"/> that can fly.
        /// </param>
        /// <param name="squares">
        /// How far they can fly.
        /// </param>
        /// <param name="actionType">
        /// The action used to fly, usually ActionType.Free.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>
        public FlyEffect(Target target, ICharacterScoreValue squares, ActionType actionType)
            : base(target)
        {
            if (squares == null)
            {
                throw new ArgumentNullException("squares");
            }

            this.Squares = squares;
            this.ActionType = actionType;
        }

        /// <summary>
        /// The number of squares the target can fly.
        /// </summary>
        public ICharacterScoreValue Squares
        {
            get;
            private set;
        }

        /// <summary>
        /// The action used to fly, usually ActionType.Free.
        /// </summary>
        public ActionType ActionType
        {
            get;
            private set;
        }

        
        /// <summary>
        /// Return <see cref="EffectSpan"/>s representing a human 
        /// readable display.
        /// </summary>
        /// <param name="character">
        /// The <see cref="Character"/> to base the representation from.
        /// This cannot be null.
        /// </param>
        /// <returns>
        /// <see cref="EffectSpan"/>s representing this component.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="character"/> cannot be null.
        /// </exception>
        public override IEnumerable<EffectSpan> Parse(Character character)
        {
            if (character == null)
            {
                throw new ArgumentNullException("character");
            }

            yield return new EffectSpan(string.Format("can fly {0} squares as a {1} action",
                Squares.GetValue(character), ActionType.ToString().ToLower()));

        }
    }
}
