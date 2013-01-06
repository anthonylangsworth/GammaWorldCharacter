using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers.Effects.EffectComponents
{
    /// <summary>
    /// Shift the target (i.e. move it without providing opportunity attacks for moving).
    /// </summary>
    public class ShiftEffect : EffectComponent
    {
        /// <summary>
        /// Create a new <see cref="ShiftEffect"/>.
        /// </summary>
        /// <param name="target">
        /// The <see cref="Target"/> this effect component acts on. This
        /// cannot be null.
        /// </param>
        /// <param name="squares">
        /// The number of squares the target is pushed.
        /// </param>
        /// <param name="actionType">
        /// The action the target can shift as.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>        
        public ShiftEffect(Target target, int squares, ActionType actionType)
            : this(target, new ConstantValue(squares), actionType)
        {
            if (squares <= 0)
            {
                throw new ArgumentException("squares must be positive", "squares");
            }
        }

        /// <summary>
        /// Create a new <see cref="ShiftEffect"/>.
        /// </summary>
        /// <param name="target">
        /// The <see cref="Target"/> this effect component acts on. This
        /// cannot be null.
        /// </param>
        /// <param name="squares">
        /// An <see cref="ICharacterScoreValue"/> containing the number of squares moved.
        /// </param>
        /// <param name="actionType">
        /// The action the target can shift as.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>        
        public ShiftEffect(Target target, ICharacterScoreValue squares, ActionType actionType)
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
        /// The number of squares the target is pushed.
        /// </summary>
        public ICharacterScoreValue Squares
        {
            get;
            private set;
        }

        /// <summary>
        /// The action
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
            yield return new EffectSpan(string.Format("can shift {0} squares as a {1} action",
                Squares.GetValue(character), ActionType.ToString().ToLower()));
        }
    }
}
