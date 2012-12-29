using System;
using System.Collections.Generic;
using GammaWorldCharacter.Gear;
using GammaWorldCharacter.Powers;

namespace GammaWorldCharacterViewer.Renderers
{
    /// <summary>
    /// For the given main hand and off hand, call the predicate for each usable power
    /// to determine whether to display it or not.
    /// </summary>
    public class ItemPowerDisplay
    {
        /// <summary>
        /// Create a new <see cref="ItemPowerDisplay"/>.
        /// </summary>
        /// <param name="mainHand">
        /// The main hand <see cref="Item"/>.
        /// </param>
        /// <param name="offHand">
        /// The off hand <see cref="Item"/>.  This can be null to indicate not equipping
        /// anything in the off hand.
        /// </param>
        /// <param name="predicate">
        /// The <see cref="Predicate"/> that determines whether or not to
        /// show each usable power for the given item combination.
        /// </param>
        public ItemPowerDisplay(Item mainHand, Item offHand, Predicate<Power> predicate)
        {
            if (mainHand == null)
            {
                throw new ArgumentNullException("mainHand");
            }
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            MainHand = mainHand;
            OffHand = offHand;
            Predicate = predicate;
        }

        /// <summary>
        /// The item to equip in the main hand.
        /// </summary>
        public Item MainHand
        {
            get;
            private set;
        }

        /// <summary>
        /// The item to equip in the off hand. This can be null, indicating nothing
        /// should be equipped in the off hand.
        /// </summary>
        public Item OffHand
        {
            get;
            private set;
        }

        /// <summary>
        /// Call this to determine whether or not to display a power when the specified 
        /// items are equipped.
        /// </summary>
        public Predicate<Power> Predicate
        {
            get;
            private set;
        }
    }
}
