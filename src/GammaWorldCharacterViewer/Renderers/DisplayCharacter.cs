using System;
using System.Collections.Generic;
using System.Linq;
using GammaWorldCharacterViewer;
using GammaWorldCharacter;
using GammaWorldCharacter.Gear.Weapons;
using GammaWorldCharacter.Gear;
using GammaWorldCharacter.Powers;

namespace GammaWorldCharacterViewer.Renderers
{
    /// <summary>
    /// A <see cref="Character"/> that includes additional details about how it should be
    /// displayed. For example, what powers should be displayed with each weapon or implement
    /// equipped.
    /// </summary>
    public class DisplayCharacter
    {
        private Character character;
        private List<ItemPowerDisplay> itemPowerDisplays;
        private Comparison<PowerEntry> entryComparer;

        /// <summary>
        /// Create a <see cref="DisplayCharacter"/> and build a list of weapon powers
        /// automatically.
        /// </summary>
        /// <param name="character">
        /// The <see cref="Character"/> to use. This cannot be null and must be wielding 
        /// a weapon in the main hand.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="character"/> cannot be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="character"/> must be wielding a weapon on the main hand.
        /// </exception>
        public DisplayCharacter(Character character)
        {
            if (character == null)
            {
                throw new ArgumentException("character");
            }

            this.character = character;
            this.itemPowerDisplays = new List<ItemPowerDisplay>(BuildItemPowerDisplayList(character));
        }

        /// <summary>
        /// Create a <see cref="DisplayCharacter"/>.
        /// </summary>
        /// <param name="character">
        /// The <see cref="Character"/> to use.
        /// </param>
        /// <param name="itemPowerDisplays">
        /// The list of powers to display for each main hand and off hand combination.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// None of the arguments can be null.
        /// </exception>
        public DisplayCharacter(Character character, IEnumerable<ItemPowerDisplay> itemPowerDisplays)
        {
            if (character == null)
            {
                throw new ArgumentException("character");
            }
            if (itemPowerDisplays == null)
            {
                throw new ArgumentException("character");
            }

            this.character = character;
            this.itemPowerDisplays = new List<ItemPowerDisplay>();
            this.itemPowerDisplays.AddRange(itemPowerDisplays);
            this.entryComparer = null;
        }

        /// <summary>
        /// Create a <see cref="DisplayCharacter"/>.
        /// </summary>
        /// <param name="character">
        /// The <see cref="Character"/> to use.
        /// </param>
        /// <param name="itemPowerDisplays">
        /// The list of powers to display for each main hand and off hand combination.
        /// </param>
        /// <param name="entryComparer">
        /// Instructions on ordering the included powers.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// None of the arguments can be null.
        /// </exception>
        public DisplayCharacter(Character character, IEnumerable<ItemPowerDisplay> itemPowerDisplays,
            Comparison<PowerEntry> entryComparer)
        {
            if (character == null)
            {
                throw new ArgumentException("character");
            }
            if(itemPowerDisplays == null)
            {
                throw new ArgumentException("character");
            }
            if (entryComparer == null)
            {
                throw new ArgumentNullException("entryComparer");
            }

            this.character = character;
            this.itemPowerDisplays = new List<ItemPowerDisplay>();
            this.itemPowerDisplays.AddRange(itemPowerDisplays);
            this.entryComparer = entryComparer;
        }

        /// <summary>
        /// The <see cref="Character"/> being displayed.
        /// </summary>
        public Character Character
        {
            get
            {
                return character;
            }
        }

        /// <summary>
        /// How the entries will be sorted. If this is null, use the default sorting.
        /// </summary>
        public Comparison<PowerEntry> EntryComparer
        {
            get
            {
                return entryComparer;
            }
        }

        /// <summary>
        /// The powers to display for each set of items equipped in the character's hands.
        /// </summary>
        public List<ItemPowerDisplay> ItemPowerDisplays
        {
            get
            {
                return itemPowerDisplays;
            }
        }

        /// <summary>
        /// Build a list of <see cref="ItemPowerDisplay"/> automatically.
        /// </summary>
        /// <param name="character">
        /// The <see cref="GammaWorldCharacer"/> to build the list for. This cannot be null
        /// and must have a weapon in the main hand.
        /// </param>
        /// <returns>
        /// The list.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="character"/> cannot be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="character"/> must be holding a weapon in the main hand. This is primary
        /// for ease of implementation.
        /// </exception>
        public static IEnumerable<ItemPowerDisplay> BuildItemPowerDisplayList(Character character)
        {
            if (character == null)
            {
                throw new ArgumentNullException("character");
            }
            if (character.GetHeldItem<Weapon>(Hand.Main) == null)
            {
                throw new ArgumentException("character must be holding a weapon in the main hand", "character");
            }

            List<ItemPowerDisplay> result;
            Item originalMainHand;
            Item originalOffHand;

            result = new List<ItemPowerDisplay>();
            result.Add(new ItemPowerDisplay(character.GetHeldItem<Item>(Hand.Main), null, x => true));
            if (character.GetHeldItem<Weapon>(Hand.Off) != null)
            {
                result.Add(new ItemPowerDisplay(character.GetHeldItem<Item>(Hand.Off), null, x => x is WeaponAttackPower));
            }

            // Save the main hand and off hand to restore them when done
            originalMainHand = character.GetHeldItem<Item>(Hand.Main);
            originalOffHand = character.GetHeldItem<Item>(Hand.Off); ;
            try
            {
                character.SetHeldItem(Hand.Main, null);
                character.SetHeldItem(Hand.Off, null);

                result.AddRange(character.Gear.Where(x => x is Weapon).Select(
                    x => new ItemPowerDisplay(x, null, y => y is WeaponAttackPower)));
            }
            finally
            {
                // Restore held items
                character.SetHeldItem(Hand.Main, originalMainHand);
                character.SetHeldItem(Hand.Off, originalOffHand);
            }

            return result;
        }
    }
}
