using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Gear
{
    /// <summary>
    /// A standard adventurer's kit (PHB 222).
    /// </summary>
    public static class ExplorersKit
    {
        /// <summary>
        /// The contents of a standard adventurer's kit.
        /// </summary>
        public static IList<Item> Contents
        {
            get
            {
                return new Item[]
                {
                    new Item("Backpack", Slot.None),
                    new Item("Bedroll", Slot.None),
                    new Item("Canteen", Slot.None),
                    new Item("Flint and steel", Slot.None),
                    new Item("Trail Rations (10 days)", Slot.None),
                    new Item("Rope (100 ft)", Slot.None),
                };
            }
        }
    }
}
