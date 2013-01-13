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
        /// The ability score modifiers.
        /// </summary>
        public static IEnumerable<ScoreType> AbilityScoreModifiers
        {
            get
            {
                return new ScoreType[]
                {
                    ScoreType.StrengthModifier,
                    ScoreType.ConstitutionModifier,
                    ScoreType.DexterityModifier,
                    ScoreType.IntelligenceModifier,
                    ScoreType.WisdomModifier,
                    ScoreType.CharismaModifier
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

        /// <summary>
        /// Convert the given <paramref name="scoreType"/> to a 
        /// human readable string.
        /// </summary>
        /// <param name="scoreType">
        /// </param>
        /// <returns></returns>
        public static string ToString(ScoreType scoreType)
        {
            string result = string.Empty;
            switch (scoreType)
            {
                case ScoreType.Strength:
                    result = "Strength";
				    break;
                case ScoreType.Constitution:
                    result = "Constitution";
				    break;
                case ScoreType.Dexterity:
                    result = "Dexterity";
				    break;
                case ScoreType.Intelligence:
                    result = "Intelligence";
				    break;
                case ScoreType.Wisdom:
                    result = "Wisdom";
				    break;
                case ScoreType.Charisma:
                    result = "Charisma";
				    break;
                case ScoreType.StrengthModifier:
                    result = "Strength Modifier";
                    break;
                case ScoreType.ConstitutionModifier:
                    result = "Constitution Modifier";
                    break;
                case ScoreType.DexterityModifier:
                    result = "Dexterity Modifier";
                    break;
                case ScoreType.IntelligenceModifier:
                    result = "Intelligence Modifier";
                    break;
                case ScoreType.WisdomModifier:
                    result = "Wisdom Modifier";
                    break;
                case ScoreType.CharismaModifier:
                    result = "Charisma Modifier";
                    break;
                case ScoreType.Initiative:
                    result = "Initiative";
				    break;
                case ScoreType.HitPoints:
                    result = "Hit Points";
				    break;
                case ScoreType.Bloodied:
                    result = "Bloodied";
				    break;
                case ScoreType.ArmorClass:
                    result = "Armor Class";
				    break;
                case ScoreType.Fortitude:
                    result = "Fortitude";
				    break;
                case ScoreType.Reflex:
                    result = "Reflex";
				    break;
                case ScoreType.Will:
                    result = "Will";
				    break;
                case ScoreType.Speed:
                    result = "Speed";
				    break;
                case ScoreType.Fly:
                    result = "Fly";
				    break;
                case ScoreType.Climb:
                    result = "Climb";
				    break;
                case ScoreType.Swim:
                    result = "Swim";
				    break;
                case ScoreType.SavingThrows:
                    result = "Saving Throws";
				    break;
                case ScoreType.Acrobatics:
                    result = "Acrobatics";
				    break;
                case ScoreType.Athletics:
                    result = "Athletics";
				    break;
                case ScoreType.Conspiracy:
                    result = "Conspiracy";
				    break;
                case ScoreType.Insight:
                    result = "Insight";
				    break;
                case ScoreType.Interaction:
                    result = "Interaction";
				    break;
                case ScoreType.Mechanics:
                    result = "Mechanics";
				    break;
                case ScoreType.Nature:
                    result = "Nature";
				    break;
                case ScoreType.Perception:
                    result = "Perception";
				    break;
                case ScoreType.Science:
                    result = "Science";
				    break;
                case ScoreType.Stealth:
                    result = "Stealth";
				    break;
                case ScoreType.Level:
                    result = "Level";
				    break;
                case ScoreType.OpportunityAttackAttackBonus:
                    result = "Opportunity Attack Attack Bonus";
				    break;
                case ScoreType.OpportunityAttackArmorClassBonus:
                    result = "Opportunity Attack Armor Class Bonus";
				    break;
                case ScoreType.StrengthCheck:
                    result = "Strength Check";
				    break;
                case ScoreType.ConstitutionCheck:
                    result = "Constitution Check";
				    break;
                case ScoreType.DexterityCheck:
                    result = "Dexterity Check";
				    break;
                case ScoreType.IntelligenceCheck:
                    result = "Intelligence Check";
				    break;
                case ScoreType.WisdomCheck:
                    result = "Wisdom Check";
				    break;
                case ScoreType.CharismaCheck:
                    result = "Charisma Check";
				    break;
                case ScoreType.FireResistance:
                    result = "Fire Resistance";
				    break;
                case ScoreType.ElectricityResistance:
                    result = "Electricity Resistance";
				    break;
                case ScoreType.ColdResistance:
                    result = "Cold Resistance";
				    break;
                case ScoreType.PhysicalResistance:
                    result = "Physical Resistance";
				    break;
                case ScoreType.FireVulnerability:
                    result = "Fire Vulnerability";
                    break;
                default:
                    throw new ArgumentException("Unknown ScoreType", "scoreType");
            }
            return result;
        }
    }
}
