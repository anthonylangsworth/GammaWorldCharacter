using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers
{
    /// <summary>
    /// The damage done by an attack power.
    /// </summary>
    /// <remarks>
    /// Clearly, this is not a modifier source. However, it uses the dependency system
    /// to get the damage of the current weapon. Hence it "pulls" details from other
    /// modifier sources unlike most non-score modifier sources.
    /// </remarks>
    public class PowerDamage: ModifierSource
    {
        private Dice dice;

        /// <summary>
        /// Create a new <see cref="PowerDamage"/> that uses the given dice damage.
        /// </summary>
        /// <param name="name">
        /// The name of the damage, usually "[Power] damage".
        /// </param>
        /// <param name="dice">
        /// The damage done.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Niether name nor dice can be null.
        /// </exception>
        public PowerDamage(string name, Dice dice)
            : this(name)
        {
            if (dice == null)
            {
                throw new ArgumentNullException("dice");
            }

            this.dice = dice;
        }

        /// <summary>
        /// Create a new <see cref="PowerDamage"/> whose <see cref="Dice"/> are set
        /// by the derived class.
        /// </summary>
        /// <param name="name">
        /// The name of the damage, usually "[Power name] damage".
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Niether power nor name can be null.
        /// </exception>
        protected PowerDamage(string name)
            : base(name, name)
        {
            this.dice = null;
        }

        /// <summary>
        /// The damage done by the dice.
        /// </summary>
        public Dice Dice
        {
            get
            {
                return dice;
            }
        }

        /// <summary>
        /// Construct a human readable representation of the damage.
        /// </summary>
        /// <returns>
        /// A human readable representation.
        /// </returns>
        public override string ToString()
        {
            return dice.ToString();
        }

        /// <summary>
        /// Set the <see cref="Dice"/> used for damage.
        /// </summary>
        /// <param name="dice">
        /// The dice used for damage.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// dice cannot be null.
        /// </exception>
        protected void SetDice(Dice dice)
        {
            if (dice == null)
            {
                throw new ArgumentNullException("dice");
            }

            this.dice = dice;
        }
    }
}
