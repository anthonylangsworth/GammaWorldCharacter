using System;
using System.Linq;
using System.Collections.Generic;
using GammaWorldCharacter.Gear;
using GammaWorldCharacter.Powers;
using GammaWorldCharacter;

namespace GammaWorldCharacterViewer.Renderers
{
    /// <summary>
    /// Helper methods for character renderers/
    /// </summary>
    public static class CharacterRendererHelper
    {
        /// <summary>
        /// Get a printable version of the <see cref="ActionType"/> enum value.
        /// </summary>
        /// <param name="actionType">
        /// The action type.
        /// </param>
        /// <returns>
        /// A printable version.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Unknown action type.
        /// </exception>
        public static string GetActionType(ActionType actionType)
        {
            switch (actionType)
            {
                case ActionType.Free:
                    return "free";
                case ActionType.ImmediateInterrupt:
                    return "immediate interrupt";
                case ActionType.ImmediateReaction:
                    return "immediate reaction";
                case ActionType.Minor:
                    return "minor";
                case ActionType.Move:
                    return "move";
                case ActionType.Standard:
                    return "standard";
                default:
                    throw new ArgumentException(
                        string.Format("Unknown action type '{0}'", actionType));
            }
        }

        /// <summary>
        /// Construct a dictionary of the items carried but not equipped by the character.
        /// </summary>
        /// <param name="character">
        /// The <see cref="Character"/> to get the gear for.
        /// </param>
        /// <returns>
        /// A <see cref="Dictionary&lt;K,T&gt;">Dictionary&lt;Item, int&gt;</see> associating items to
        /// the number equipped.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>
        /// <seealso cref="GetEquippedGear"/>
        public static Dictionary<Item, int> GetCarriedGear(Character character)
        {
            if (character == null)
            {
                throw new ArgumentNullException("character");
            }

            Dictionary<Item, int> equipment;

            // Get each unique item and count the occurance of each
            equipment = new Dictionary<Item, int>();
            character.Gear.ForEach(x => AddItem(x, equipment));

            return equipment;
        }

        /// <summary>
        /// Construct a dictionary of the items held or worn by the character.
        /// </summary>
        /// <param name="character">
        /// The <see cref="Character"/> to get the gear for.
        /// </param>
        /// <returns>
        /// A <see cref="Dictionary&lt;K,T&gt;">Dictionary&lt;Item, int&gt;</see> associating items to
        /// the number equipped.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>
        /// <seealso cref="GetCarriedGear"/>
        public static Dictionary<Item, int> GetEquippedGear(Character character)
        {
            if (character == null)
            {
                throw new ArgumentNullException("character");
            }

            Dictionary<Item, int> equipment;
            Slot currentSlot;

            equipment = new Dictionary<Item, int>();

            if (character.GetHeldItem<Item>(Hand.Main) != null)
            {
                AddItem(character.GetHeldItem<Item>(Hand.Main), equipment);
            }
            if (character.GetHeldItem<Item>(Hand.Off) != null)
            {
                AddItem(character.GetHeldItem<Item>(Hand.Off), equipment);
            }
            foreach (int enumValue in Enum.GetValues(typeof(Slot)))
            {
                currentSlot = (Slot)enumValue;
                if (currentSlot != Slot.None && currentSlot != Slot.Weapon)
                {
                    if (character.GetEquippedItem<Item>(currentSlot) != null)
                    {
                        AddItem(character.GetEquippedItem<Item>(currentSlot), equipment);
                    }
                }
            }

            return equipment;
        }

        /// <summary>
        /// Get the format string to pass to <see cref="Score"/>.ToString().
        /// </summary>
        /// <param name="scoreDisplayType">
        /// Is the score a total or a modifier?
        /// </param>
        /// <param name="showModifiers">
        /// True if modifiers should be expanded and shown, false if only the score shown.
        /// </param>
        /// <returns>
        /// The arguemnt to ToString().
        /// </returns>
        /// <exception cref="ArgumentException">
        /// An unknwon ScoreDisplayType was passed.
        /// </exception>
        public static string GetFormatString(ScoreDisplayType scoreDisplayType, bool showModifiers)
        {
            string result;

            if (scoreDisplayType == ScoreDisplayType.Score)
            {
                result = showModifiers ? "FS" : "CS";
            }
            else if (scoreDisplayType == ScoreDisplayType.Modifier)
            {
                result = showModifiers ? "FM" : "CM";
            }
            else
            {
                throw new ArgumentException("Unknown ScoreDisplayType", "scoreDisplaytype");
            }

            return result;
        }

        /// <summary>
        /// Return the image URL for the given <see cref="Power"/>.
        /// </summary>
        /// <param name="power">
        /// The <see cref="Power"/>.
        /// </param>
        /// <returns>
        /// The <see cref="Uri"/> containing the relative URL to the images or null,
        /// if there is no image for the attack type.
        /// </returns>
        public static Uri GetImageUrl(Power power)
        {
            if(power == null)
            {
                throw new ArgumentNullException("power");
            }

            Uri result;

            switch(power.AttackTypeAndRange.AttackType)
            {
                case AttackType.Area:
                    result = new Uri("images/icon_area.gif", UriKind.Relative);
                    break;
                case AttackType.Close:
                    result = new Uri("images/icon_close.gif", UriKind.Relative);
                    break;
                case AttackType.Melee:
                    if(power is BasicAttack)
                    {
                        result = new Uri("images/icon_meleebasic.gif", UriKind.Relative);
                    }
                    else
                    {
                        result = new Uri("images/icon_melee.gif", UriKind.Relative);
                    }
                    break;
                case AttackType.Personal:
                    result = null;
                    break;
                case AttackType.Ranged:
                    if (power is BasicAttack)
                    {
                        result = new Uri("images/icon_rangebasic.gif", UriKind.Relative);
                    }
                    else
                    {
                        result = new Uri("images/icon_range.gif", UriKind.Relative);
                    }
                    break;
                default:
                    throw new ArgumentException(
                        string.Format("Unknown attack type '{0}'", power.AttackTypeAndRange.AttackType));
            }

            return result;
        }

        /// <summary>
        /// Get a printable version of the <see cref="PowerFrequency"/> enum value.
        /// </summary>
        /// <param name="powerFrequency">
        /// The frequency.
        /// </param>
        /// <returns>
        /// A printable version.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Unknown frequency.
        /// </exception>
        public static string GetPowerFrequency(PowerFrequency powerFrequency)
        {
            switch(powerFrequency)
            {
                case PowerFrequency.AtWill:
                    return "at will";
                case PowerFrequency.Encounter:
                    return "encounter";
                case PowerFrequency.Consumable:
                    return "consumable";
                default:
                    throw new ArgumentException(
                        string.Format("Unknown frequency '{0}'", powerFrequency));
            }
        }

        /// <summary>
        /// Return the human readable name of a resistance.
        /// </summary>
        /// <param name="scoreType">
        /// The resistance.
        /// </param>
        /// <returns>
        /// The human readable name.
        /// </returns>
        public static string GetResistName(ScoreType scoreType)
        {
            string result;

            switch (scoreType)
            {
                case ScoreType.FireResistance:
                    result = "fire";
                    break;
                case ScoreType.ElectricityResistance:
                    result = "electricity";
                    break;
                case ScoreType.ColdResistance:
                    result = "cold";
                    break;
                case ScoreType.PhysicalResistance:
                    result = "physical";
                    break;
                default:
                    throw new ArgumentException("Not a resistance", "scoreType");
            }

            return result;
        }

        /// <summary>
        /// Return the human readable name of a vulnerability.
        /// </summary>
        /// <param name="scoreType">
        /// The resistance.
        /// </param>
        /// <returns>
        /// The human readable name.
        /// </returns>
        public static string GetVulnerabilityName(ScoreType scoreType)
        {
            string result;

            switch (scoreType)
            {
                case ScoreType.FireVulnerability:
                    result = "fire";
                    break;
                default:
                    throw new ArgumentException("Not a vulnerability", "scoreType");
            }

            return result;
        }

        /// <summary>
        /// Add an item to the list of equipment. 
        /// </summary>
        /// <param name="item">
        /// The item to add.
        /// </param>
        /// <param name="equipment">
        /// The list of equipment to use. The key is the item and the value is the number of times it is present.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>
        private static void AddItem(Item item, Dictionary<Item, int> equipment)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            if (equipment == null)
            {
                throw new ArgumentNullException("equipment");
            }

            if (equipment.ContainsKey(item))
            {
                equipment[item] = equipment[item] + 1;
            }
            else
            {
                equipment.Add(item, 1);
            }
        }
    }

    /// <summary>
    /// Whether the value being displayed is a total or a modifier.
    /// </summary>
    public enum ScoreDisplayType
    {
        /// <summary>
        /// The value displayed is a total.
        /// </summary>
        Score,
        /// <summary>
        /// The value displayed is a modifier.
        /// </summary>
        Modifier
    }
}
