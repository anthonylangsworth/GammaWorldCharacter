using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows;
using System.Collections;
using GammaWorldCharacter;
using GammaWorldCharacter.Gear;
using GammaWorldCharacter.Levels;
using GammaWorldCharacter.Powers;
using GammaWorldCharacter.Gear.Weapons;
using GammaWorldCharacter.Gear.Armor;
using GammaWorldCharacter.Origins;
using GammaWorldCharacter.Powers.Effects;
using GammaWorldCharacter.Traits;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Shapes;
using GammaWorldCharacter.Scores;

namespace GammaWorldCharacterViewer.Renderers
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomDocumentRenderer: FlowDocumentRenderer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="displayCharacter"></param>
        /// <exception cref="ArgumentNullException">
        /// No arguments can be null.
        /// </exception>
        public override FlowDocument Render(DisplayCharacter displayCharacter)
        {
            if (displayCharacter == null)
            {
                throw new ArgumentNullException("displayCharacter");
            }

            FlowDocument result;

            result = new FlowDocument();

            StyleHelper.AddStyles<Block>(result);
            AddImages(result);

            result.Blocks.Add(RenderHeader( displayCharacter.Character, result.Resources));
            result.Blocks.Add(RenderStatistics( displayCharacter.Character, result.Resources));
            result.Blocks.AddRange(RenderPowers(displayCharacter, result.Resources));
            result.Blocks.Add(RenderAbilities( displayCharacter.Character, result.Resources));
            result.Blocks.Add(RenderGear( displayCharacter.Character, result.Resources));

            return result;
        }

        #region Image Management

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flowDocument"></param>
        private static void AddImages(FlowDocument flowDocument)
        {
            flowDocument.Resources.Add(BasicMeleeImageName, 
                new BitmapImage() { UriSource = new Uri("/Images/icon_meleebasic.gif", UriKind.Relative) });
            flowDocument.Resources.Add(BasicRangedImageName,
                new BitmapImage() { UriSource = new Uri("/Images/icon_rangedbasic.gif", UriKind.Relative) });
        }

        /// <summary>
        /// 
        /// </summary>
        private static readonly string BasicMeleeImageName = "BasicMelee";
        /// <summary>
        /// 
        /// </summary>
        private static readonly string BasicRangedImageName = "BasicRanged";

        #endregion

        #region Sections

        /// <summary>
        /// Render the <paramref name="character"/>'s name, class, level and so on.
        /// </summary>
        /// <param name="character">
        /// The <see cref="Character"/> to render. This cannot be null.
        /// </param>
        /// <param name="resourceDictionary">
        /// A <see cref="ResourceDictionary"/> containing styles. This cannot be null.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No arguments can be null.
        /// </exception>
        private Block RenderHeader(Character character, ResourceDictionary resourceDictionary)
        {
            if (character == null)
            {
                throw new ArgumentNullException("GammaWorldCharacter");
            }
            if (resourceDictionary == null)
            {
                throw new ArgumentNullException("resourceDictionary");
            }
            DictionaryHelper.Expect<Style>(resourceDictionary, StyleHelper.HeaderStyleName);

            Table table;
            TableRowGroup tableRowGroup;
            TableRow tableRow;
            TableCell leftCell;
            TableCell rightCell;

            table = new Table();
            table.Style = (Style)resourceDictionary[StyleHelper.HeaderStyleName];
            table.Columns.Add(new TableColumn());
            table.Columns.Add(new TableColumn());

            leftCell = new TableCell();
            leftCell.TextAlignment = TextAlignment.Left;
            leftCell.Blocks.Add(new Paragraph(new Bold(new Run(character.Name))));

            rightCell = new TableCell();
            rightCell.TextAlignment = TextAlignment.Right;
            rightCell.Blocks.Add(new Paragraph(new Run(string.Format("Level {0} {1} {2}", 
                character.Level, character.PrimaryOrigin.Name, character.SecondaryOrigin.Name))));

            tableRow = new TableRow();
            tableRow.Cells.Add(leftCell);
            tableRow.Cells.Add(rightCell);

            tableRowGroup = new TableRowGroup();
            tableRowGroup.Rows.Add(tableRow);

            table.RowGroups.Add(tableRowGroup);

            return table;
        }

        /// <summary>
        /// Render the <paramref name="GammaWorldCharacter"/>'s hit points, defenses and so on.
        /// </summary>
        /// <param name="character">
        /// The <see cref="Character"/> to render. This cannot be null.
        /// </param>
        /// <param name="resourceDictionary">
        /// A <see cref="ResourceDictionary"/> containing styles. This cannot be null.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No arguments can be null.
        /// </exception>
        private Block RenderStatistics(Character character, ResourceDictionary resourceDictionary)
        {
            if (character == null)
            {
                throw new ArgumentNullException("GammaWorldCharacter");
            }
            if (resourceDictionary == null)
            {
                throw new ArgumentNullException("flowDocument");
            }
            DictionaryHelper.Expect<Style>(resourceDictionary, StyleHelper.DetailRowStyleName);

            Table table;
            TableRowGroup tableRowGroup;
            TableRow tableRow;
            TableCell hpCell;
            TableCell initiativeCell;
            TableCell defensesCell;
            TableCell perceptionCell;
            TableCell speedCell;
            TableCell extrasCell;
            Paragraph paragraph; // Reused

            table = new Table();
            table.Style = (Style)resourceDictionary[StyleHelper.DetailRowStyleName];
            // Hack: Used because star widths are not supported
            table.Columns.Add(new TableColumn());
            table.Columns.Add(new TableColumn());
            table.Columns.Add(new TableColumn());
            table.Columns.Add(new TableColumn());
            table.Columns.Add(new TableColumn());

            tableRowGroup = new TableRowGroup();

            // Top left cell containing HP, Bloodied and healing surge info
            paragraph = new Paragraph();
            paragraph.Inlines.Add(new Bold(new Run("HP ")));
            paragraph.Inlines.Add(new Run(character[ScoreType.HitPoints].ToString(
                CharacterRendererHelper.GetFormatString(ScoreDisplayType.Score, ShowModifiers))));
            paragraph.Inlines.Add(new Bold(new Run("  Bloodied ")));
            paragraph.Inlines.Add(new Run(character[ScoreType.Bloodied].ToString(
                CharacterRendererHelper.GetFormatString(ScoreDisplayType.Score, ShowModifiers))));
            hpCell = new TableCell();
            hpCell.ColumnSpan = table.Columns.Count - 1;
            hpCell.Blocks.Add(paragraph);

            // Top right cell containing Initiative
            paragraph = new Paragraph();
            paragraph.Inlines.Add(new Bold(new Run("Initiative ")));
            paragraph.Inlines.Add(new Run(character[ScoreType.Initiative].ToString(
                CharacterRendererHelper.GetFormatString(ScoreDisplayType.Modifier, ShowModifiers))));
            initiativeCell = new TableCell();
            initiativeCell.TextAlignment = TextAlignment.Right;
            initiativeCell.Blocks.Add(paragraph);

            // Contruct the first row
            tableRow = new TableRow();
            tableRow.Cells.Add(hpCell);
            tableRow.Cells.Add(initiativeCell);
            tableRowGroup.Rows.Add(tableRow);

            // The middle left cell with defenses
            paragraph = new Paragraph();
            paragraph.Inlines.Add(new Bold(new Run("AC ")));
            paragraph.Inlines.Add(new Run(character[ScoreType.ArmorClass].ToString(
                CharacterRendererHelper.GetFormatString(ScoreDisplayType.Score, ShowModifiers))));
            if (character[ScoreType.OpportunityAttackArmorClassBonus].Total != 0)
            {
                paragraph.Inlines.Add(new Run(string.Format(" ({0} against opportunity attacks)",
                    character[ScoreType.ArmorClass].Total + character[ScoreType.OpportunityAttackArmorClassBonus].Total)));
            }
            paragraph.Inlines.Add(new Bold(new Run("  Fortitude ")));
            paragraph.Inlines.Add(new Run(character[ScoreType.Fortitude].ToString(
                CharacterRendererHelper.GetFormatString(ScoreDisplayType.Score, ShowModifiers))));
            paragraph.Inlines.Add(new Bold(new Run("  Reflex ")));
            paragraph.Inlines.Add(new Run(character[ScoreType.Reflex].ToString(
                CharacterRendererHelper.GetFormatString(ScoreDisplayType.Score, ShowModifiers))));
            paragraph.Inlines.Add(new Bold(new Run("  Will ")));
            paragraph.Inlines.Add(new Run(character[ScoreType.Will].ToString(
                CharacterRendererHelper.GetFormatString(ScoreDisplayType.Score, ShowModifiers))));
            defensesCell = new TableCell();
            defensesCell.ColumnSpan = table.Columns.Count - 1;
            defensesCell.Blocks.Add(paragraph);

            // Middle right cepp containing Perception
            paragraph = new Paragraph();
            paragraph.Inlines.Add(new Bold(new Run("Perception ")));
            paragraph.Inlines.Add(new Run(string.Format(" {0}",
                character[ScoreType.Perception].ToString(
                CharacterRendererHelper.GetFormatString(ScoreDisplayType.Modifier, ShowModifiers)))));
            perceptionCell = new TableCell();
            perceptionCell.TextAlignment = TextAlignment.Right;
            perceptionCell.Blocks.Add(paragraph);

            // Contruct the second row
            tableRow = new TableRow();
            tableRow.Cells.Add(defensesCell);
            tableRow.Cells.Add(perceptionCell);
            tableRowGroup.Rows.Add(tableRow);

            // Bottom left cell with speed
            paragraph = new Paragraph();
            paragraph.Inlines.Add(new Bold(new Run("Speed ")));
            paragraph.Inlines.Add(new Run(character[ScoreType.Speed].ToString(
                CharacterRendererHelper.GetFormatString(ScoreDisplayType.Score, ShowModifiers))));
            foreach (ScoreType scoreType in ScoreTypeHelper.MovementModes.Where(x => x != ScoreType.Speed && character[x].Total != 0))
            {
                paragraph.Inlines.Add(new Run("; " + scoreType.ToString().ToLower() + " " +
                    character[scoreType].ToString(CharacterRendererHelper.GetFormatString(ScoreDisplayType.Score, ShowModifiers))));
            }
            speedCell = new TableCell();
            speedCell.ColumnSpan = table.Columns.Count - 1;
            speedCell.Blocks.Add(paragraph);

            // Contruct the third row
            tableRow = new TableRow();
            tableRow.Cells.Add(speedCell);
            tableRowGroup.Rows.Add(tableRow);

            // Add Resist and Vulnerable entries if present
            if (ScoreTypeHelper.Resistances.Any(x => character[x].Total != 0)
                || ScoreTypeHelper.Vulnerabilities.Any(x => character[x].Total != 0))
            {
                bool first;
                paragraph = new Paragraph();

                if (ScoreTypeHelper.Resistances.Any(x => character[x].Total != 0))
                {
                    paragraph.Inlines.Add(new Bold(new Run("Resist ")));
                    first = true;
                    foreach (ScoreType resistance in ScoreTypeHelper.Resistances.Where(x => character[x].Total != 0))
                    {
                        paragraph.Inlines.Add(new Run(string.Format("{0} {1}",
                            character[resistance].ToString(CharacterRendererHelper.GetFormatString(ScoreDisplayType.Score, ShowModifiers)),
                            CharacterRendererHelper.GetResistName(resistance))));

                        if (first)
                        {
                            first = false;
                        }
                        else
                        {
                            paragraph.Inlines.Add(new Run(", "));
                        }
                    }
                }
                if (ScoreTypeHelper.Resistances.Any(x => character[x].Total != 0)
                    && ScoreTypeHelper.Vulnerabilities.Any(x => character[x].Total != 0))
                {
                    paragraph.Inlines.Add(new Run("; "));
                }
                if (ScoreTypeHelper.Vulnerabilities.Any(x => character[x].Total != 0))
                {
                    paragraph.Inlines.Add(new Bold(new Run("Vulnerable ")));
                    first = true;
                    foreach (ScoreType vulnerability in ScoreTypeHelper.Vulnerabilities.Where(x => character[x].Total != 0))
                    {
                        paragraph.Inlines.Add(new Run(string.Format("{0} {1}",
                            character[vulnerability].ToString(CharacterRendererHelper.GetFormatString(ScoreDisplayType.Score, ShowModifiers)),
                            CharacterRendererHelper.GetVulnerabilityName(vulnerability))));

                        if (first)
                        {
                            first = false;
                        }
                        else
                        {
                            paragraph.Inlines.Add(new Run(", "));
                        }
                    }
                }

                extrasCell = new TableCell();
                extrasCell.ColumnSpan = table.Columns.Count;
                extrasCell.Blocks.Add(paragraph);

                // Contruct the third row
                tableRow = new TableRow();
                tableRow.Cells.Add(extrasCell);
                tableRowGroup.Rows.Add(tableRow);
            }

            // Add a "Saving Throws" row if the GammaWorldCharacter has saving throw modifiers
            if (character[ScoreType.SavingThrows].AppliedModifiers.Count > 0)
            {
                paragraph = new Paragraph();

                if (character[ScoreType.SavingThrows].AppliedModifiers.Count > 0)
                {
                    paragraph.Inlines.Add(new Bold(new Run("Saving Throws ")));
                    paragraph.Inlines.Add(new Run(character[ScoreType.SavingThrows].ToString(
                        CharacterRendererHelper.GetFormatString(ScoreDisplayType.Modifier, ShowModifiers))));
                }

                extrasCell = new TableCell();
                extrasCell.ColumnSpan = table.Columns.Count;
                extrasCell.Blocks.Add(paragraph);

                // Contruct the third row
                tableRow = new TableRow();
                tableRow.Cells.Add(extrasCell);
                tableRowGroup.Rows.Add(tableRow);
            }

            table.RowGroups.Add(tableRowGroup);

            return table;
        }

        /// <summary>
        /// Render the <paramref name="GammaWorldCharacter"/>'s name, class, level and so on.
        /// </summary>
        /// <param name="GammaWorldCharacter">
        /// The <see cref="Character"/> to render. This cannot be null.
        /// </param>
        /// <param name="resourceDictionary">
        /// A <see cref="ResourceDictionary"/> containing styles. This cannot be null.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No arguments can be null.
        /// </exception>
        private Block RenderAbilities(Character GammaWorldCharacter, ResourceDictionary resourceDictionary)
        {
            if (GammaWorldCharacter == null)
            {
                throw new ArgumentNullException("GammaWorldCharacter");
            }
            if (resourceDictionary == null)
            {
                throw new ArgumentNullException("resourceDictionary");
            }
            DictionaryHelper.Expect<Style>(resourceDictionary, StyleHelper.TraitHeaderRowStyleName);

            List<Score> skills;
            Table table;
            TableRowGroup tableRowGroup;
            TableRow tableRow;
            TableCell leftCell; // Reused
            TableCell middleCell; // Reused
            TableCell rightCell; // Reused
            Paragraph paragraph; // Reused

            table = new Table();
            table.Style = (Style)resourceDictionary[StyleHelper.TraitHeaderRowStyleName];
            table.Columns.Add(new TableColumn());
            table.Columns.Add(new TableColumn());
            table.Columns.Add(new TableColumn());

            tableRowGroup = new TableRowGroup();

            // The skills row
            skills = new List<Score>();
            foreach (ScoreType skill in ScoreTypeHelper.Skills)
            {
                skills.Add(GammaWorldCharacter[skill]);
            }
            skills.Sort((x, y) => x.Name.CompareTo(y.Name));
            paragraph = new Paragraph();
            paragraph.Inlines.Add(new Bold(new Run("Skills ")));
            paragraph.Inlines.Add(new Run(string.Join(", ",
                skills.ConvertAll(x => string.Format("{0} {1}", x.Name, x.ToString(
                    CharacterRendererHelper.GetFormatString(ScoreDisplayType.Modifier, ShowModifiers)))).ToArray())));
            leftCell = new TableCell();
            leftCell.ColumnSpan = 3;
            leftCell.Blocks.Add(paragraph);

            // Contruct the third row
            tableRow = new TableRow();
            tableRow.Cells.Add(leftCell);
            tableRowGroup.Rows.Add(tableRow);

            // TODO: Convert these to loops

            // The first row of ability scores
            paragraph = new Paragraph();
            paragraph.Inlines.Add(new Bold(new Run("Str ")));
            paragraph.Inlines.Add(new Run(string.Format(" {0} ({1})",
                GammaWorldCharacter[ScoreType.Strength].ToString(
                CharacterRendererHelper.GetFormatString(ScoreDisplayType.Score, ShowModifiers)),
                ModifierHelper.FormatModifier(((AbilityScore) GammaWorldCharacter[ScoreType.Strength]).Modifier, false))));
            leftCell = new TableCell();
            leftCell.Blocks.Add(paragraph);

            paragraph = new Paragraph();
            paragraph.Inlines.Add(new Bold(new Run("Dex ")));
            paragraph.Inlines.Add(new Run(string.Format(" {0} ({1})",
                GammaWorldCharacter[ScoreType.Dexterity].ToString(
                CharacterRendererHelper.GetFormatString(ScoreDisplayType.Score, ShowModifiers)),
                ModifierHelper.FormatModifier(((AbilityScore)GammaWorldCharacter[ScoreType.Dexterity]).Modifier, false))));
            middleCell = new TableCell();
            middleCell.Blocks.Add(paragraph);

            paragraph = new Paragraph();
            paragraph.Inlines.Add(new Bold(new Run("Wis ")));
            paragraph.Inlines.Add(new Run(string.Format(" {0} ({1})",
                GammaWorldCharacter[ScoreType.Wisdom].ToString(
                CharacterRendererHelper.GetFormatString(ScoreDisplayType.Score, ShowModifiers)),
                ModifierHelper.FormatModifier(((AbilityScore) GammaWorldCharacter[ScoreType.Wisdom]).Modifier, false))));
            rightCell = new TableCell();
            rightCell.Blocks.Add(paragraph);

            // Contruct the first row of ability scores
            tableRow = new TableRow();
            tableRow.Cells.Add(leftCell);
            tableRow.Cells.Add(middleCell);
            tableRow.Cells.Add(rightCell);
            tableRowGroup.Rows.Add(tableRow);

            // The second row of ability scores
            paragraph = new Paragraph();
            paragraph.Inlines.Add(new Bold(new Run("Con ")));
            paragraph.Inlines.Add(new Run(string.Format(" {0} ({1})",
                GammaWorldCharacter[ScoreType.Constitution].ToString(
                CharacterRendererHelper.GetFormatString(ScoreDisplayType.Score, ShowModifiers)),
                ModifierHelper.FormatModifier(((AbilityScore) GammaWorldCharacter[ScoreType.Constitution]).Modifier, false))));
            leftCell = new TableCell();
            leftCell.Blocks.Add(paragraph);

            paragraph = new Paragraph();
            paragraph.Inlines.Add(new Bold(new Run("Int ")));
            paragraph.Inlines.Add(new Run(string.Format(" {0} ({1})",
                GammaWorldCharacter[ScoreType.Intelligence].ToString(
                CharacterRendererHelper.GetFormatString(ScoreDisplayType.Score, ShowModifiers)),
                ModifierHelper.FormatModifier(((AbilityScore)GammaWorldCharacter[ScoreType.Intelligence]).Modifier, false))));
            middleCell = new TableCell();
            middleCell.Blocks.Add(paragraph);

            paragraph = new Paragraph();
            paragraph.Inlines.Add(new Bold(new Run("Cha ")));
            paragraph.Inlines.Add(new Run(string.Format(" {0} ({1})",
                GammaWorldCharacter[ScoreType.Charisma].ToString(
                CharacterRendererHelper.GetFormatString(ScoreDisplayType.Score, ShowModifiers)),
                ModifierHelper.FormatModifier(((AbilityScore)GammaWorldCharacter[ScoreType.Charisma]).Modifier, false))));
            rightCell = new TableCell();
            rightCell.Blocks.Add(paragraph);

            // Contruct the second row of ability scores
            tableRow = new TableRow();
            tableRow.Cells.Add(leftCell);
            tableRow.Cells.Add(middleCell);
            tableRow.Cells.Add(rightCell);
            tableRowGroup.Rows.Add(tableRow);

            table.RowGroups.Add(tableRowGroup);

            return table;
        }

        /// <summary>
        /// Render the <paramref name="GammaWorldCharacter"/>'s name, class, level and so on.
        /// </summary>
        /// <param name="GammaWorldCharacter">
        /// The <see cref="Character"/> to render. This cannot be null.
        /// </param>
        /// <param name="resourceDictionary">
        /// A <see cref="ResourceDictionary"/> containing styles. This cannot be null.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No arguments can be null.
        /// </exception>
        private Block RenderGear(Character GammaWorldCharacter, ResourceDictionary resourceDictionary)
        {
            if (GammaWorldCharacter == null)
            {
                throw new ArgumentNullException("GammaWorldCharacter");
            }
            if (resourceDictionary == null)
            {
                throw new ArgumentNullException("resourceDictionary");
            }
            DictionaryHelper.Expect<Style>(resourceDictionary, StyleHelper.DetailRowStyleName);

            Section section;

            section = new Section();
            section.Style = (Style)resourceDictionary[StyleHelper.DetailRowStyleName];

            section.Blocks.Add(RenderGearList(
                CharacterRendererHelper.GetEquippedGear(GammaWorldCharacter), "Equipped Gear", resourceDictionary));
            section.Blocks.Add(RenderGearList(
                CharacterRendererHelper.GetCarriedGear(GammaWorldCharacter), "Other Gear", resourceDictionary));

            return section;
        }

        /// <summary>
        /// Render out the supplied gear to the given HtmlTextWriter.
        /// </summary>
        /// <param name="equipment">
        /// A <see cref="Dictionary{I, N}"/> that maps each item to the 
        /// number of that item carried by the GammaWorldCharacter. This cannot be null.
        /// Each <see cref="Item"/> must appear at least once., it the 
        /// corresponding value must be 1 or greater.
        /// </param>
        /// <param name="title">
        /// The section title to display on the GammaWorldCharacter sheet. This cannot be
        /// null or empty.
        /// </param>
        /// <param name="resourceDictionary">
        /// A <see cref="ResourceDictionary"/> containing styles.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>
        private Block RenderGearList(Dictionary<Item, int> equipment, string title, ResourceDictionary resourceDictionary)
        {
            if (equipment == null)
            {
                throw new ArgumentNullException("equipment");
            }
            foreach (Item item in equipment.Keys)
            {
                if (item == null)
                {
                    throw new ArgumentException("equipment cannot contain a null item", "equipment");
                }
                if (equipment[item] < 1)
                {
                    throw new ArgumentException(string.Format("equipment contains zero or negative count {0} for item '{1}'",
                        equipment[item], item.Name), "equipment");
                }
            }
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException("title");
            }
            DictionaryHelper.Expect<Style>(resourceDictionary, StyleHelper.DetailRowStyleName);

            List<Item> items;
            Paragraph result;

            // Show multiple occurances as parenthesized counts. Also italicize magic item names.
            items = new List<Item>();
            items.AddRange(equipment.Keys.OrderBy(x => x.Name));

            result = new Paragraph();
            result.Style = (Style)resourceDictionary[StyleHelper.DetailRowStyleName];
            result.Inlines.Add(new Bold(new Run(title + " ")));
            for(int i = 0; i < items.Count(); i++)
            {
                result.Inlines.Add(items[i].Name);
                if(equipment[items[i]] > 1)
                {
                    result.Inlines.Add(new Run(string.Format(" ({0})", equipment[items[i]])));
                }

                if(i < items.Count - 1)
                {
                    result.Inlines.Add(", ");
                }
            }

            return result;
        }

        #endregion

        #region Powers

        /// <summary>
        /// 
        /// </summary>
        /// <param name="displayCharacter"></param>
        /// <param name="resourceDictionary"></param>
        /// <returns></returns>
        private IEnumerable<Block> RenderPowers(DisplayCharacter displayCharacter, ResourceDictionary resourceDictionary)
        {
            List<PowerEntry> powerEntries;
            Item originalMainHand;
            Item originalOffHand;

            powerEntries = new List<PowerEntry>();

            // Add entries to the power list
            if (displayCharacter.ItemPowerDisplays.Count == 0)
            {
                powerEntries.AddRange(ConstructPowers( displayCharacter.Character, x => true, resourceDictionary));
            }
            else
            {
                originalMainHand = displayCharacter.Character.GetHeldItem<Item>(Hand.Main);
                originalOffHand = displayCharacter.Character.GetHeldItem<Item>(Hand.Off);

                try
                {
                    foreach (ItemPowerDisplay itemPowerDisplay in displayCharacter.ItemPowerDisplays)
                    {
                        displayCharacter.Character.SetHeldItem(Hand.Off, null);
                        displayCharacter.Character.SetHeldItem(Hand.Main, itemPowerDisplay.MainHand);
                        displayCharacter.Character.SetHeldItem(Hand.Off, itemPowerDisplay.OffHand);
                        displayCharacter.Character.Update();
                        powerEntries.AddRange(ConstructPowers(displayCharacter.Character,
                            itemPowerDisplay.Predicate, resourceDictionary));
                    }
                }
                finally
                {
                    displayCharacter.Character.SetHeldItem(Hand.Off, null);
                    displayCharacter.Character.SetHeldItem(Hand.Main, originalMainHand);
                    displayCharacter.Character.SetHeldItem(Hand.Off, originalOffHand);
                    displayCharacter.Character.Update();
                }
            }

            powerEntries.AddRange(ConstructTraits(displayCharacter.Character, resourceDictionary));

            return RenderPowerEntries(powerEntries, resourceDictionary);
        }

        /// <summary>
        /// Render out the GammaWorldCharacter's powers.
        /// </summary>
        /// <param name="GammaWorldCharacter">
        /// The <see cref="Character"/> to render.
        /// </param>
        /// <param name="predicate">
        /// Only render the power if the given predicate matches.
        /// </param>
        /// <param name="resourceDictionary">
        /// A <see cref="ResourceDictionary"/> containing styles.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>
        private IEnumerable<PowerEntry> ConstructPowers(Character GammaWorldCharacter, 
            Predicate<Power> predicate, ResourceDictionary resourceDictionary)
        {
            if (GammaWorldCharacter == null)
            {
                throw new ArgumentNullException("GammaWorldCharacter");
            }
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }
            if (resourceDictionary == null)
            {
                throw new ArgumentNullException("resourceDictionary");
            }

            List<Power> powers;
            List<Power> displayedPowers;

            powers = new List<Power>();
            powers.AddRange(GammaWorldCharacter.GetUsablePowers());

            displayedPowers = new List<Power>();
            foreach (Power power in powers)
            {
                if (!displayedPowers.Contains(power))
                {
                    if (predicate(power))
                    {
                        if (power is AttackPower)
                        {
                            yield return ConstructAttackPowerEntry(GammaWorldCharacter, (AttackPower)power, resourceDictionary);
                        }
                        else if (power is UtilityPower)
                        {
                            yield return ConstructUtilityPowerEntry(GammaWorldCharacter, (UtilityPower)power, resourceDictionary);
                        }
                        displayedPowers.Add(power);
                    }
                }
            }
        }

        /// <summary>
        /// Construct the heading for a <see cref="PowerEntry"/> for a <see cref="Power"/>.
        /// </summary>
        /// <param name="character">
        /// The <see cref="Character"/> to construct the heading for.
        /// </param>
        /// <param name="power">
        /// The <see cref="Power"/> to construct the heading for.
        /// </param>
        /// <param name="resourceDictionary">
        /// A <see cref="ResourceDictionary"/> containing styles.
        /// </param>
        /// <returns>
        /// The heading.
        /// </returns>
        private Block ConstructPowerEntryHeading(Character character, Power power, ResourceDictionary resourceDictionary)
        {
            if (character == null)
            {
                throw new ArgumentNullException("GammaWorldCharacter");
            }
            if (power == null)
            {
                throw new ArgumentNullException("power");
            }
            DictionaryHelper.Expect<Style>(resourceDictionary, StyleHelper.AtWillHeaderRowStyleName);
            DictionaryHelper.Expect<Style>(resourceDictionary, StyleHelper.EncounterHeaderRowStyleName);
            DictionaryHelper.Expect<Style>(resourceDictionary, StyleHelper.ConsumableHeaderRowStyleName);

            Paragraph result;
            Style style;

            result = new Paragraph();

            switch (power.Frequency)
            {
                case PowerFrequency.AtWill:
                    style = (Style)resourceDictionary[StyleHelper.AtWillHeaderRowStyleName];
                    break;
                case PowerFrequency.Encounter:
                    style = (Style)resourceDictionary[StyleHelper.EncounterHeaderRowStyleName];
                    break;
                case PowerFrequency.Consumable:
                    style = (Style)resourceDictionary[StyleHelper.ConsumableHeaderRowStyleName];
                    break;
                default:
                    throw new ArgumentException("Unknown power frequency", "power");
            }
            result.Style = style;

            /*
            // Add power type image
            BitmapImage bitmapImage;

            bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = Application.GetResourceStream( new Uri(@"Images/icon_melee.gif", UriKind.Relative)).Stream;
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.EndInit();

            result.Inlines.Add(new InlineUIContainer(new Image() { Source = bitmapImage, Height = 12, Width = 12 }));
             */

            result.Inlines.Add(new Bold(new Run(power.Name + " ")));
            if (power.DamageTypes != DamageTypes.None
                || power.EffectTypes != EffectTypes.None
                || power.PowerSource == PowerSource.Item)
            {
                // Bullet separator. Use the image so it will work for PDF export.
                // result.Inlines.Add(new Run(Encoding.Unicode.GetString(new byte[]{ 0xCF, 0x25 }) + " "));
                result.Inlines.Add(new InlineUIContainer(new Rectangle() 
                { 
                    Margin = new Thickness(0, 0, 0, 2), 
                    Fill = result.Foreground, 
                    Height = 5, 
                    Width = 5 
                } ));
                result.Inlines.Add(new Run (" "));

                if (power.DamageTypes != DamageTypes.None)
                {
                    result.Inlines.Add(power.DamageTypes.ToString());
                }
                if (power.DamageTypes != DamageTypes.None
                    && power.EffectTypes != EffectTypes.None)
                {
                    result.Inlines.Add(new Run(", "));
                }
                if (power.EffectTypes != EffectTypes.None)
                {
                    result.Inlines.Add(new Run(power.EffectTypes.ToString()));
                }
                if ((power.DamageTypes != DamageTypes.None
                    || power.EffectTypes != EffectTypes.None)
                    && power.PowerSource == PowerSource.Item)
                {
                    result.Inlines.Add(new Run(", "));
                }
                if (power.PowerSource == PowerSource.Item)
                {
                    result.Inlines.Add(new Run(PowerSource.Item.ToString()));
                }
            }

            return result;
        }

        /// <summary>
        /// Construct the heading for a <see cref="PowerEntry"/> for a <see cref="Power"/>.
        /// </summary>
        /// <param name="power">
        /// The <see cref="Power"/> to construct the heading for.
        /// </param>
        /// <param name="resourceDictionary">
        /// A <see cref="ResourceDictionary"/> containing styles.
        /// </param>
        /// <returns>
        /// The heading.
        /// </returns>
        private Block ConstructPowerEntryFlavorText(Power power, ResourceDictionary resourceDictionary)
        {
            if (power == null)
            {
                throw new ArgumentNullException("power");
            } 
            DictionaryHelper.Expect<Style>(resourceDictionary, StyleHelper.FlavorTextStyleName);

            Paragraph result;

            if (power.HasDescription)
            {
                result = new Paragraph(new Italic(new Run(power.Description)));
                result.Style = (Style)resourceDictionary[StyleHelper.FlavorTextStyleName];
            }
            else
            {
                result = null;
            }

            return result;
        }

        /// <summary>
        /// Render out an <see cref="AttackPower"/>.
        /// </summary>
        /// <param name="character">
        /// The <see cref="Character"/> to render.
        /// </param>
        /// <param name="attackPower">
        /// The <see cref="AttackPower"/> to render.
        /// </param>
        /// <param name="resourceDictionary">
        /// A <see cref="ResourceDictionary"/> containing styles.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="attackPower"/> is not usable.
        /// </exception>
        private PowerEntry ConstructAttackPowerEntry(Character character, AttackPower attackPower, ResourceDictionary resourceDictionary)
        {
            if (character == null)
            {
                throw new ArgumentNullException("character");
            }
            if (attackPower == null)
            {
                throw new ArgumentNullException("attackPower");
            }
            if (!attackPower.IsUsable(character))
            {
                throw new ArgumentException("attackPower is not usable", "attackPower");
            }
            DictionaryHelper.Expect<Style>(resourceDictionary, StyleHelper.DetailRowStyleName);

            Paragraph detail;
            Weapon mainHandWeapon;
            Weapon offHandWeapon;
            Weapon currentWeapon;

            detail = new Paragraph();
            detail.Style = (Style)resourceDictionary[StyleHelper.DetailRowStyleName];

            // Write out a line for each attack
            currentWeapon = null;
            foreach (AttackDetails attackDetails in attackPower.Attacks)
            {
                if (attackPower is WeaponAttackPower)
                {
                    mainHandWeapon = character.GetHeldItem<Weapon>(Hand.Main);
                    offHandWeapon = character.GetHeldItem<Weapon>(Hand.Off);

                    // Sanity checks
                    if (!(attackDetails.AttackBonus is WeaponAttackBonus))
                    {
                        throw new InvalidOperationException(string.Format(
                            "Attack power '{0}' with Weapon accessory does not have a WeaponAttackBonus", attackPower.Name));
                    }
                    if (mainHandWeapon == null && offHandWeapon == null)
                    {
                        throw new InvalidOperationException(string.Format(
                            "Attack power '{0}' with Weapon accessory usable when no weapons equipped", attackPower.Name));
                    }
                    if (!attackDetails.HasDamage)
                    {
                        throw new InvalidOperationException(string.Format(
                            "Attack power '{0}' with Weapon accessory has no damage", attackPower.Name));
                    }
                    if (attackDetails.HasDamage && attackDetails.DamageBonus is WeaponDamageBonus
                        && ((WeaponDamageBonus)attackDetails.DamageBonus).Hand == Hand.Main && mainHandWeapon == null)
                    {
                        throw new InvalidOperationException(string.Format(
                            "Attack power '{0}' uses a main hand weapon when no weapon is equipped in the main hand", attackPower.Name));
                    }
                    if (attackDetails.HasDamage && attackDetails.DamageBonus is WeaponDamageBonus
                        && ((WeaponDamageBonus)attackDetails.DamageBonus).Hand == Hand.Off && offHandWeapon == null)
                    {
                        throw new InvalidOperationException(string.Format(
                            "Attack power '{0}' uses an off hand weapon when no weapon is equipped in the off hand", attackPower.Name));
                    }

                    // Set the weapon used by the power
                    currentWeapon = ((WeaponDamageBonus)attackDetails.DamageBonus).Hand == Hand.Main ?
                        mainHandWeapon : offHandWeapon;
                }

                if (attackPower.HasTrigger)
                {
                    detail.Inlines.Add(new Bold(new Run("Trigger: ")));
                    detail.Inlines.Add(new Run(attackPower.Trigger));
                    detail.Inlines.Add(new LineBreak());
                }
                if (attackDetails.HasAttackRoll)
                {
                    detail.Inlines.Add(new Bold(new Run(string.Format("Attack{0}: ",
                        attackPower.HasTrigger ? string.Format(" ({0})", CharacterRendererHelper.GetActionType(attackPower.Action)) : string.Empty))));

                    if (currentWeapon != null)
                    {
                        // Output the weapon name
                        detail.Inlines.Add(new Run("(" + currentWeapon.Name + ") "));

                        // Output the range
                        if (attackPower.AttackTypeAndRange.AttackType == AttackType.Ranged)
                        {
                            detail.Inlines.Add(new Run(string.Format("Ranged {0} ",
                                ((RangedWeapon) currentWeapon).Range.ToString())));
                        }
                        else if (attackPower.AttackTypeAndRange.AttackType == AttackType.Melee)
                        {
                            detail.Inlines.Add(new Run("Melee 1 "));
                        }
                        else
                        {
                            detail.Inlines.Add(new Run(string.Format("{0} ",
                                attackPower.AttackTypeAndRange.ToString().Trim())));
                        }
                    }
                    else
                    {
                        // Output the range for the power
                        detail.Inlines.Add(new Run(string.Format("{0} ",
                            attackPower.AttackTypeAndRange.ToString().Trim())));
                    }

                    detail.Inlines.Add(new Run(string.Format("({0}) ", attackDetails.Target)));
                    detail.Inlines.Add(new Run(string.Format("; {0} vs {1}", attackDetails.AttackBonus.ToString(
                    CharacterRendererHelper.GetFormatString(ScoreDisplayType.Modifier, ShowModifiers)),
                        character[attackDetails.AttackedDefense].Abbreviation)));
                    detail.Inlines.Add(new LineBreak());
                }
                if (attackDetails.HasDamage || !string.IsNullOrEmpty(attackDetails.AdditionalText))
                {
                    detail.Inlines.Add(new Bold(new Run(string.Format("Hit: "))));
                    if (attackDetails.HasDamage)
                    {
                        detail.Inlines.Add(new Run(string.Format("{0}{1}", attackDetails.Damage.Dice,
                            attackDetails.DamageBonus.ToString(
                            CharacterRendererHelper.GetFormatString(ScoreDisplayType.Modifier, ShowModifiers)))));
                    }
                    if (attackDetails.HasDamage && !string.IsNullOrEmpty(attackDetails.AdditionalText))
                    {
                        detail.Inlines.Add(new Run(" "));
                    }
                    if (!string.IsNullOrEmpty(attackDetails.AdditionalText))
                    {
                        detail.Inlines.Add(new Run(attackDetails.AdditionalText));
                    }
                }
                if (attackDetails.HasMissEffect)
                {
                    detail.Inlines.Add(new LineBreak());
                    detail.Inlines.Add(new Bold(new Run("Miss: ")));
                    detail.Inlines.Add(new Run(attackDetails.MissEffect));
                }
                // Gamma World Characters get bonus critical damage at level 2 and 6
                if (attackPower.Criticals.Any())
                {
                    detail.Inlines.Add(new LineBreak());
                    foreach (EffectExpression effectExpression in attackPower.Criticals)
                    {
                        detail.Inlines.Add(new Bold(new Run("Critical: ")));
                        // TODO: Move this into a separate method
                        foreach (EffectSpan effectSpan in new EffectParser().Parse(character, effectExpression))
                        {
                            if (effectSpan.Type == EffectSpanType.Power)
                            {
                                detail.Inlines.Add(new Italic(new Run(effectSpan.Text)));
                            }
                            else
                            {
                                detail.Inlines.Add(new Run(effectSpan.Text));
                            }
                        }
                    }
                }
                detail.Inlines.Add(new LineBreak());
            }

            // Write out the other details
            if (attackPower.HasEffect)
            {
                detail.Inlines.Add(new Bold(new Run(
                    string.Format("Effect{0}: ", attackPower.HasTrigger ? string.Format(" ({0})", CharacterRendererHelper.GetActionType(attackPower.Action)) : string.Empty))));
                detail.Inlines.Add(new Run(attackPower.Effect));
                detail.Inlines.Add(new LineBreak());
            }
            if (attackPower.HasSustainDetails)
            {
                detail.Inlines.Add(new Bold(new Run(string.Format("Sustain {0}: ", 
                    CharacterRendererHelper.GetActionType(attackPower.SustainDetails.Action)))));
                detail.Inlines.Add(new Run(attackPower.SustainDetails.Text));
                detail.Inlines.Add(new LineBreak());
            }

            // Special cases.
            if (attackPower is BasicAttack && character[ScoreType.OpportunityAttackAttackBonus].Total > 0)
            {
                detail.Inlines.Add(new Bold(new Run("Special: ")));
                detail.Inlines.Add(new Run(string.Format("+{0} attack bonus when used for an opportunity attack", 
                    character[ScoreType.OpportunityAttackAttackBonus].Total)));
                detail.Inlines.Add(new LineBreak());
            }

            // Remove the last LineBreak
            if (detail.Inlines.LastInline is LineBreak)
            {
                detail.Inlines.Remove(detail.Inlines.LastInline);
            }

            return new PowerEntry(ConstructPowerEntryHeading(character, attackPower, resourceDictionary),
                ConstructPowerEntryFlavorText(attackPower, resourceDictionary), detail, attackPower);
        }

        /// <summary>
        /// Render out a <see cref="UtilityPower"/>.
        /// </summary>
        /// <param name="GammaWorldCharacter">
        /// The <see cref="Character"/> to render.
        /// </param>
        /// <param name="utilityPower">
        /// The <see cref="UtilityPower"/> to render.
        /// </param>
        /// <param name="resourceDictionary">
        /// A <see cref="ResourceDictionary"/> containing styles.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>
        private PowerEntry ConstructUtilityPowerEntry(Character GammaWorldCharacter, UtilityPower utilityPower, ResourceDictionary resourceDictionary)
        {
            if (GammaWorldCharacter == null)
            {
                throw new ArgumentNullException("GammaWorldCharacter");
            }
            if (utilityPower == null)
            {
                throw new ArgumentNullException("utilityPower");
            }
            if (resourceDictionary == null)
            {
                throw new ArgumentNullException("resourceDictionary");
            }
            DictionaryHelper.Expect<Style>(resourceDictionary, StyleHelper.DetailRowStyleName);

            Paragraph detail;

            detail = new Paragraph();
            detail.Style = (Style)resourceDictionary[StyleHelper.DetailRowStyleName];
            if (utilityPower.HasTrigger)
            {
                detail.Inlines.Add(new Bold(new Run("Trigger: ")));
                detail.Inlines.Add(new Run(utilityPower.Trigger));
                detail.Inlines.Add(new LineBreak());
            }
            if (utilityPower.HasEffect)
            {
                detail.Inlines.Add(new Bold(new Run(string.Format("Effect{0}: ",
                    utilityPower.HasTrigger ? string.Format(" ({0})", CharacterRendererHelper.GetActionType(utilityPower.Action)) : string.Empty))));
                detail.Inlines.Add(new Run(utilityPower.AttackTypeAndRange.ToString().Trim()));
                detail.Inlines.Add(new Run("; "));
                detail.Inlines.Add(new Run(utilityPower.Effect));
                detail.Inlines.Add(new LineBreak());
            }
            if (utilityPower.HasSustainDetails)
            {
                detail.Inlines.Add(new Bold(new Run(string.Format("Sustain {0}: ",
                    CharacterRendererHelper.GetActionType(utilityPower.SustainDetails.Action)))));
                detail.Inlines.Add(new Run(utilityPower.SustainDetails.Text));
                detail.Inlines.Add(new LineBreak());
            }

            // Remove the last LineBreak
            if (detail.Inlines.LastInline is LineBreak)
            {
                detail.Inlines.Remove(detail.Inlines.LastInline);
            }

            return new PowerEntry(ConstructPowerEntryHeading(GammaWorldCharacter, utilityPower, resourceDictionary),
                ConstructPowerEntryFlavorText(utilityPower, resourceDictionary), detail, utilityPower);
        }

        /// <summary>
        /// Render out the specified <see cref="PowerEntry"/>s.
        /// </summary>
        /// <param name="resourceDictionary">
        /// A <see cref="ResourceDictionary"/> containing style information.
        /// </param>
        /// <param name="powerEntries">
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>
        private IEnumerable<Block> RenderPowerEntries(IList<PowerEntry> powerEntries, ResourceDictionary resourceDictionary)
        {
            if (powerEntries == null)
            {
                throw new ArgumentNullException("powerEntries");
            }
            if (powerEntries.Contains(null))
            {
                throw new ArgumentNullException("powerEntries");
            }
            if (resourceDictionary == null)
            {
                throw new ArgumentNullException("resourceDictionary");
            }

            bool renderedHeading;
            string[] titles = 
            {
                "Traits",
                "Standard Actions",
                "Minor Actions",
                "Move Actions",
                "Triggered Actions"
            };
            Predicate<PowerEntry>[] predicates =
            {
                pe => !(pe.ModifierSource is Power),
                pe => pe.ModifierSource is Power && (((Power) pe.ModifierSource).Action == ActionType.Standard),
                pe => pe.ModifierSource is Power && (((Power)pe.ModifierSource).Action == ActionType.Minor),
                pe => pe.ModifierSource is Power && (((Power)pe.ModifierSource).Action == ActionType.Move),
                pe => pe.ModifierSource is Power && 
                    ((((Power)pe.ModifierSource).Action == ActionType.ImmediateInterrupt) || (((Power)pe.ModifierSource).Action == ActionType.ImmediateReaction))
            };
            List<Block> result;

            // Sanity check
            if (titles.Length != predicates.Length)
            {
                throw new InvalidOperationException("titles and predicates length mismatch");
            }

            result = new List<Block>();
            for (int i = 0; i < titles.Length; i++)
            {
                renderedHeading = false;
                foreach (PowerEntry powerEntry in 
                    from pe in powerEntries
                    where predicates[i](pe)
                    orderby GetPriority(pe.ModifierSource), pe.ModifierSource.Name
                    select pe)
                {
                    if (!renderedHeading)
                    {
                        result.Add(RenderPowerSectionHeading(titles[i], resourceDictionary));
                        renderedHeading = true;
                    }
                    result.AddRange(RenderPowerEntry(powerEntry));
                }
            }

            return result;
        }

        /// <summary>
        /// Get the default priority for the given <see cref="ModifierSource"/>.
        /// </summary>
        /// <param name="modifierSource">
        /// The <see cref="ModifierSource"/> to get the priority for. This can be null.
        /// </param>
        /// <returns>
        /// A <see cref="PowerEntryPriority"/> for the given <see cref="ModifierSource"/>.
        /// </returns>
        private static int GetPriority(ModifierSource modifierSource)
        {
            Power power;
            int result;

            result = 0;

            power = modifierSource as Power;
            if (power != null)
            {
                if (power.Frequency == PowerFrequency.AtWill)
                {
                    result = 100;
                }
                else if (power.Frequency == PowerFrequency.Encounter)
                {
                    result = 200;
                }
                else if (power.Frequency == PowerFrequency.Consumable)
                {
                    result = 400;
                }
                else
                {
                    result = 500;
                }
            }
            else
            {
                result = 500;
            }


            return result;
        }

        /// <summary>
        /// Render out the specified <see cref="PowerEntry"/>s.
        /// </summary>
        /// <param name="powerEntry">
        /// The <see cref="PowerEntry"/> to render.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>
        private IEnumerable<Block> RenderPowerEntry(PowerEntry powerEntry)
        {
            if (powerEntry == null)
            {
                throw new ArgumentNullException("powerEntry");
            }

            yield return powerEntry.Heading;
            if (powerEntry.FlavorText != null)
            {
                yield return powerEntry.FlavorText;
            }
            yield return powerEntry.Detail;
        }

        /// <summary>
        /// Render a power section heading.
        /// </summary>
        /// <param name="title">
        /// The heading title.
        /// </param>
        /// <param name="resourceDictionary">
        /// A <see cref="ResourceDictionary"/> containing styles.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>
        private Block RenderPowerSectionHeading(string title, ResourceDictionary resourceDictionary)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException("powerEntry");
            }
            DictionaryHelper.Expect<Style>(resourceDictionary, StyleHelper.HeaderStyleName);

            Paragraph result;

            result = new Paragraph();
            result.Style = (Style)resourceDictionary[StyleHelper.HeaderStyleName];
            result.Inlines.Add(new Bold(new Run(title)));

            return result;
        }

        /// <summary>
        /// Render any racial traits that have a description.
        /// </summary>
        /// <param name="character">
        /// The <see cref="Character"/> to render the racial traits for.
        /// </param>
        /// <param name="resourceDictionary">
        /// A <see cref="ResourceDictionary"/> containing styles.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>
        private IEnumerable<PowerEntry> ConstructTraits(Character character, ResourceDictionary resourceDictionary)
        {
            if (character == null)
            {
                throw new ArgumentNullException("GammaWorldCharacter");
            }
            if (resourceDictionary == null)
            {
                throw new ArgumentNullException("resourceDictionary");
            }
            DictionaryHelper.Expect<Style>(resourceDictionary, StyleHelper.TraitHeaderRowStyleName);
            DictionaryHelper.Expect<Style>(resourceDictionary, StyleHelper.DetailRowStyleName);

            foreach (Trait trait in character.GetTraits())
            {
                yield return new PowerEntry(
                    new Paragraph(new Bold(new Run(trait.Name))) { Style = (Style)resourceDictionary[StyleHelper.TraitHeaderRowStyleName] },
                    null, 
                    new Paragraph(new Run(trait.Description)) { Style = (Style)resourceDictionary[StyleHelper.DetailRowStyleName] }, trait);
            }
        }

        #endregion
    }
}
