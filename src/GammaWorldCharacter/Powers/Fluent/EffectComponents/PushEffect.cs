using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers.Fluent.EffectComponents
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
            : base(target)
        {
            if (squares <= 0)
            {
                throw new ArgumentException("squares must be positive", "squares");
            }

            this.Squares = squares;
        }

        /// <summary>
        /// The number of squares the target is pushed.
        /// </summary>
        public int Squares
        {
            get;
            private set;
        }
    }
}
