using System;
using System.Linq;
using System.Collections.Generic;

namespace GammaWorldCharacter
{
    /// <summary>
    /// Helper methods for dealing with <see cref="ScoreType"/>s.
    /// </summary>
    public static class ScoreTypeHelper
    {
        /// <summary>
        /// The ability scores.
        /// </summary>
        public static IEnumerable<ScoreType> AbilityScores
        {
            get
            {
                return new ScoreType[]
                {
                    ScoreType.Strength,
                    ScoreType.Constitution,
                    ScoreType.Dexterity,
                    ScoreType.Intelligence,
                    ScoreType.Wisdom,
                    ScoreType.Charisma
                };
            }
        }

        /// <summary>
        /// Does the given list of <see cref="ScoreType"/>s contain duplicates?
        /// </summary>
        /// <param name="scoreTypes">
        /// The list to check.
        /// </param>
        /// <returns>
        /// True if it contains duplicates, false otherwise.
        /// </returns>
        public static bool ContainsDuplicates(ICollection<ScoreType> scoreTypes)
        {
            List<ScoreType> list;
            bool result;

            list = new List<ScoreType>();
            list.AddRange(scoreTypes);

            result = false;
            foreach (ScoreType scoreType in scoreTypes)
            {
                if (list.FindAll(x => x.Equals(scoreType)).Count > 1)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Defenses.
        /// </summary>
        public static IEnumerable<ScoreType> Defenses
        {
            get
            {
                return new ScoreType[]
                {
                    ScoreType.ArmorClass,
                    ScoreType.Fortitude,
                    ScoreType.Reflex,
                    ScoreType.Will
                };
            }
        }

        /// <summary>
        /// Is the given score an ability score (i.e. Strength, Constitution, Dexterity, Intelligence, Wisdom or Charisma)?
        /// </summary>
        /// <param name="scoreType">
        /// The <see cref="ScoreType"/> to check.
        /// </param>
        /// <returns>
        /// True if it is an ability score, false otherwise.
        /// </returns>
        public static bool IsAbilityScore(ScoreType scoreType)
        {
            return AbilityScores.Contains(scoreType);
        }

        /// <summary>
        /// Is the given score a defense, e.g. Armor Class, Fortitude, Reflex or Will.
        /// </summary>
        /// <param name="scoreType">
        /// The <see cref="ScoreType"/> to check.
        /// </param>
        /// <returns>
        /// True if the scoreType is a defense or false of not.
        /// </returns>
        public static bool IsDefense(ScoreType scoreType)
        {
            return Defenses.Contains(scoreType);
        }

        /// <summary>
        /// Is the given <see cref="ScoreType"/> as skill?
        /// </summary>
        /// <param name="scoreType">
        /// The ScoreType to check.
        /// </param>
        /// <returns>
        /// True if it is a skill, false otherwise.
        /// </returns>
        public static bool IsSkill(ScoreType scoreType)
        {
            return Skills.Contains(scoreType);
        }

        /// <summary>
        /// The skills.
        /// </summary>
        public static IEnumerable<ScoreType> Skills
        {
            get
            {
                return new ScoreType[]
                {
                    ScoreType.Acrobatics,
                    ScoreType.Athletics,
                    ScoreType.Conspiracy,
                    ScoreType.Insight,
                    ScoreType.Interaction,
                    ScoreType.Nature,
                    ScoreType.Mechanics,
                    ScoreType.Perception,
                    ScoreType.Science,
                    ScoreType.Stealth
                };
            }
        }

        /// <summary>
        /// Resistances.
        /// </summary>
        public static IEnumerable<ScoreType> Resistances
        {
            get
            {
                return new ScoreType[]
                {
                    ScoreType.FireResistance,
                    ScoreType.ElectricityResistance,
                    ScoreType.ColdResistance,
                    ScoreType.PhysicalResistance
                };
            }
        }

        /// <summary>
        /// Vulnerabilities.
        /// </summary>
        public static IEnumerable<ScoreType> Vulnerabilities
        {
            get
            {
                return new ScoreType[]
                {
                    ScoreType.FireVulnerability
                };
            }
        }

        /// <summary>
        /// Character movement modes
        /// </summary>
        public static IEnumerable<ScoreType> MovementModes
        {
            get
            {
                return new ScoreType[]
                {
                    ScoreType.Speed,
                    ScoreType.Fly,
                    ScoreType.Climb,
                    ScoreType.Swim
                };
            }
        }
    }
}
