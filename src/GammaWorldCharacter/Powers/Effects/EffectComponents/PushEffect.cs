using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers.Effects.EffectComponents
{
    /// <summary>
    /// The target is pushed one or more squares.
    /// </summary>
    public class PushEffect: EffectComponent
    {
        /// <summary>
        /// Create a new <see cref="PushEffect"/>.
        /// </summary>
        /// <param name="target">
        /// The <see cref="Target"/> this effect component acts on. This
        /// cannot be null.
        /// </param>
        /// <param name="squares">
        /// The number of squares the target is pushed.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>        
        public PushEffect(Target target, int squares)
            : this(target, new ConstantValue(squares))
        {
            if (squares <= 0)
            {
                throw new ArgumentException("squares must be positive", "squares");
            }
        }

        /// <summary>
        /// Create a new <see cref="PushEffect"/>.
        /// </summary>
        /// <param name="target">
        /// The <see cref="Target"/> this effect component acts on. This
        /// cannot be null.
        /// </param>
        /// <param name="squares">
        /// An <see cref="ICharacterScoreValue"/> representing the number of squares pushed.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>        
        public PushEffect(Target target, ICharacterScoreValue squares)
            : base(target)
        {
            if (squares == null)
            {
                throw new ArgumentNullException("squares");
            }

            this.Squares = squares;
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

            yield return new EffectSpan(string.Format("you push the target {0} squares",
                Squares.GetValue(character)));
        }
    }
}
