using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter
{
    /// <summary>
    /// Rolling a number of the same type of dice.
    /// </summary>
    public class Dice: IEquatable<Dice>
    {
        private const int noLastRoll = 0;

        private int number;
        private int lastRoll;
        private DiceType diceType;

        /// <summary>
        /// Create a new <see cref="Dice"/>.
        /// </summary>
        /// <param name="number">
        /// The number of dice to roll.
        /// </param>
        /// <param name="diceType">
        /// The type of dice to roll.
        /// </param>
        /// <exception cref="ArgumentException">
        /// <paramref name="number"/> must be positive.
        /// </exception>
        public Dice(int number, DiceType diceType)
        {
            if (number < 1)
            {
                throw new ArgumentException("number must be positive", "number");
            }

            this.lastRoll = noLastRoll;
            this.number = number;
            this.diceType = diceType;
        }

        /// <summary>
        /// The type of dice to roll.
        /// </summary>
        public DiceType DiceType
        {
            get
            {
                return diceType;
            }
        }

        /// <summary>
        /// Are the two <see cref="Dice"/> equal, ignoring the last roll?
        /// </summary>
        /// <param name="obj">
        /// The dice to compare with this one.
        /// </param>
        /// <returns>
        /// True if they are equal, false otherwise.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj is Dice)
            {
                return Equals((Dice)obj);
            }
            else
            {
                return base.Equals(obj);
            }
        }

        /// <summary>
        /// Are the two <see cref="Dice"/> equal, ignoring the last roll?
        /// </summary>
        /// <param name="dice">
        /// The dice to compare with this one.
        /// </param>
        /// <returns>
        /// True if they are equal, false otherwise or <paramref name="dice"/> is null.
        /// </returns>
        public bool Equals(Dice dice)
        {
            if (dice == null)
            {
                return false;
            }
            else
            {
                return dice.Number == Number
                    && dice.DiceType == DiceType;
            }
        }

        /// <summary>
        /// A hash code for this object.
        /// </summary>
        /// <returns>
        /// A hash code.
        /// </returns>
        public override int GetHashCode()
        {
            return (GetType().FullName + "|" + number + "|" + diceType).GetHashCode();
        }

        /// <summary>
        /// The result of the last roll or 0, if they have not been rolled.
        /// </summary>
        /// <seealso cref="Roll"/>
        public int LastRoll
        {
            get
            {
                return lastRoll;
            }
        }

        /// <summary>
        /// The maximum possible roll.
        /// </summary>
        public int MaxRoll
        {
            get
            {
                return number * (int)diceType;
            }
        }

        /// <summary>
        /// The minimum possible roll.
        /// </summary>
        public int MinRoll
        {
            get
            {
                return number;
            }
        }

        /// <summary>
        /// The number of dice to roll.
        /// </summary>
        public int Number
        {
            get
            {
                return number;
            }
        }

        /// <summary>
        /// Roll the specified dice.
        /// </summary>
        /// <returns>
        /// The result of the roll.
        /// </returns>
        /// <seealso cref="LastRoll"/>
        public int Roll()
        {
            int result;
            Random random;

            random = new Random();
            result = 0;
            for (int i = 0; i < number; i++)
            {
                result += random.Next() % (int) diceType + 1;
            }

            lastRoll = result;
            return result;
        }

        /// <summary>
        /// A human readable representation of this object.
        /// </summary>
        /// <returns>
        /// A description of this object.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0}{1}", number, diceType);
        }
    }
}
