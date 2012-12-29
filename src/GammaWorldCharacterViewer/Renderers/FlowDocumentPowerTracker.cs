using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using System.Windows;
using GammaWorldCharacter.Gear;
using GammaWorldCharacter.Powers;
using System.Windows.Media;
using GammaWorldCharacter;

namespace GammaWorldCharacterViewer.Renderers
{
    /// <summary>
    /// A document that allows the player to check off powers as they are used.
    /// </summary>
    public class FlowDocumentPowerTracker: FlowDocumentRenderer
    {
        /// <summary>
        /// Number of encounters to add columns for.
        /// </summary>
        private const int NumEncounters = 6;

        /// <summary>
        /// Number of encounters to add columns for.
        /// </summary>
        private const int NameColumnSpan = NumEncounters * 2;

        /// <summary>
        /// Create a new <see cref="FlowDocumentPowerTracker"/>.
        /// </summary>
        public FlowDocumentPowerTracker()
        {
            // Do nothing
        }

        /// <summary>
        /// Render.
        /// </summary>
        /// <param name="displayCharacter">
        /// The <see cref="DisplayCharacter"/> to render.
        /// </param>
        /// <returns>
        /// A <see cref="FlowDocument"/> containing the character.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>
        public override FlowDocument Render(DisplayCharacter displayCharacter)
        {
            if (displayCharacter == null)
            {
                throw new ArgumentNullException("displayCharacter");
            }

            FlowDocument result;
            Table table;
            TableRowGroup tableRowGroup;

            result = new FlowDocument();
            StyleHelper.AddStyles<TextElement>(result);

            result.Blocks.Add(RenderHeader(displayCharacter.Character, result.Resources));

            table = SetupTable(result.Resources);

            // Table
            tableRowGroup = table.RowGroups.Last();
            table.RowGroups.Add(RenderEncounterPowers(displayCharacter, result.Resources));
            table.RowGroups.Add(RenderHitPoints(displayCharacter, result.Resources));

            result.Blocks.Add(table);

            return result;
        }

        /// <summary>
        /// Render out the header containing the name, level, class, race and so on.
        /// </summary>
        /// <param name="character">
        /// The <see cref="Character"/> to render.
        /// </param>
        /// <param name="resourceDictionary">
        /// A <see cref="ResourceDictionary"/> containing styles.
        /// </param>
        /// <returns>
        /// No argument can be null.
        /// </returns>
        private Block RenderHeader(Character character, ResourceDictionary resourceDictionary)
        {
            if (character == null)
            {
                throw new ArgumentNullException("character");
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
            Paragraph paragraph; // Reused

            leftCell = new TableCell();
            paragraph = new Paragraph();
            paragraph.Inlines.Add(new Bold(new Run(character.Name)));
            leftCell.Blocks.Add(paragraph);

            rightCell = new TableCell();
            paragraph = new Paragraph();
            paragraph.Inlines.Add(new Run(
                string.Format("Level {0} {1} {2}", character.Level,
                    character.PrimaryOrigin.Name, character.SecondaryOrigin.Name)));
            rightCell.TextAlignment = TextAlignment.Right;
            rightCell.Blocks.Add(paragraph);

            tableRow = new TableRow();
            tableRow.Cells.Add(leftCell);
            tableRow.Cells.Add(rightCell);

            table = new Table();
            table.Style = (Style)resourceDictionary[StyleHelper.HeaderStyleName];
            table.Columns.Add(new TableColumn());
            table.Columns.Add(new TableColumn());
            tableRowGroup = new TableRowGroup();
            tableRowGroup.Rows.Add(tableRow);
            table.RowGroups.Add(tableRowGroup);

            return table;
        }

        /// <summary>
        /// Setup the main <see cref="Table"/> and the headings.
        /// </summary>
        /// <param name="resourceDictionary"></param>
        /// <returns></returns>
        private Table SetupTable(ResourceDictionary resourceDictionary)
        {
            if (resourceDictionary == null)
            {
                throw new ArgumentNullException("resourceDictionary");
            }
            DictionaryHelper.Expect<Style>(resourceDictionary, StyleHelper.DetailRowStyleName);

            Table table;
            TableRow tableRow; // reused
            TableRowGroup tableRowGroup;

            table = new Table();
            table.Style = (Style)resourceDictionary[StyleHelper.DetailRowStyleName];
            for (int i = 0; i < (NumEncounters * 3); i++)
            {
                table.Columns.Add(new TableColumn());
            }

            tableRowGroup = new TableRowGroup();

            tableRow = new TableRow();
            tableRow.Cells.Add(new TableCell(new Paragraph(new Bold(new Run("Power")))) { ColumnSpan = NameColumnSpan, TextAlignment = TextAlignment.Center });
            tableRow.Cells.Add(new TableCell(new Paragraph(new Bold(new Run("Encounter")))) { ColumnSpan = NumEncounters, TextAlignment = TextAlignment.Center });
            tableRowGroup.Rows.Add(tableRow);

            tableRow = new TableRow();
            tableRow.Cells.Add(new TableCell(new Paragraph(new Bold(new Run(string.Empty)))) { ColumnSpan = NameColumnSpan });
            for(int i = 1; i <= NumEncounters; i++)
            {
                tableRow.Cells.Add(new TableCell(new Paragraph(new Bold(new Run(i.ToString())))) { TextAlignment = TextAlignment.Center } );
            }
            tableRowGroup.Rows.Add(tableRow);

            table.RowGroups.Add(tableRowGroup);
            return table;
        }

        /// <summary>
        /// Construct the heading for a power.
        /// </summary>
        /// <param name="power">
        /// The <see cref="Power"/> to consturct the 
        /// </param>
        /// <returns>
        /// The header as a string encoded for HTML.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>
        public string GetPowerHeading(Power power)
        {
            if (power == null)
            {
                throw new ArgumentNullException("power");
            }

            StringBuilder result;
            List<string> additionalKeyWords;

            result = new StringBuilder();
            result.AppendFormat(string.Format("{0} ", power.Name));

            additionalKeyWords = new List<string>();
            additionalKeyWords.Add(CharacterRendererHelper.GetActionType(power.Action));
            if (power is AttackPower)
            {
                additionalKeyWords.Add("attack");
            }
            else if (power.PowerSource != PowerSource.Item && power is UtilityPower)
            {
                additionalKeyWords.Add("utility");
            }
            result.AppendFormat("({0})", string.Join("; ", additionalKeyWords.ToArray()));

            return result.ToString();
        }

        /// <summary>
        /// Render out multiple boxes in a table so they don't wrap after each box.
        /// </summary>
        /// <param name="count">
        /// The number of boxes to render.
        /// </param>
        /// <param name="style">
        /// The <see cref="Style"/> to use to render the boxes. This may be null, indicating
        /// use the parent element's style.
        /// </param>
        /// <exception cref="ArgumentException">
        /// <paramref name="count"/> cannot be negative.
        /// </exception>
        private Block RenderBoxes(int count, Style style)
        {
            if (count < 0)
            {
                throw new ArgumentException("count cannot be negative", "count");
            }

            Paragraph result;

            result = new Paragraph();
            result.Style = style;
            if(count > 0)
            {
                for (int i = 1; i <= count; i++)
                {
                    result.Inlines.Add(new Run(Encoding.Unicode.GetString(new byte[] { 0xA1, 0x25 }))); // Small square

                    // Write out a new row every 10 boxes and a space after every 5.
                    if (i != count)
                    {
                        if (i % 10 == 0)
                        {
                            result.Inlines.Add(new LineBreak());
                        }
                        else if (i % 5 == 0 && i != count)
                        {
                            result.Inlines.Add(new Run(" "));
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Render out encounter powers.
        /// </summary>
        /// <param name="displayCharacter">
        /// The character to write out the encounter powers for.
        /// </param>
        /// <param name="resourceDictionary">
        /// A <see cref="ResourceDictionary"/> containing styles.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>
        private TableRowGroup RenderEncounterPowers(DisplayCharacter displayCharacter, ResourceDictionary resourceDictionary)
        {
            if (displayCharacter == null)
            {
                throw new ArgumentNullException("displayCharacter");
            }
            DictionaryHelper.Expect<Style>(resourceDictionary, StyleHelper.EncounterHeaderRowStyleName);

            List<Power> powers;
            TableRowGroup tableRowGroup;
            Style style;

            powers = new List<Power>();
            powers.AddRange(displayCharacter.Character.GetUsablePowers().Where(x => x.Frequency == PowerFrequency.Encounter));
            powers.Sort((x, y) => x.Name.CompareTo(y.Name));

            style = (Style)resourceDictionary[StyleHelper.EncounterHeaderRowStyleName];

            tableRowGroup = new TableRowGroup();
            foreach (Power power in powers)
            {
                TableRow tableRow;

                tableRow = new TableRow();
                tableRow.Cells.Add(
                    new TableCell(new Paragraph(new Run(GetPowerHeading(power))) { Style = style }) { ColumnSpan = NameColumnSpan });
                for (int i = 0; i < NumEncounters; i++)
                {
                    tableRow.Cells.Add(new TableCell(RenderBoxes(1, style)) { TextAlignment = TextAlignment.Center });
                }

                tableRowGroup.Rows.Add(tableRow);
            }

            return tableRowGroup;
        }

        /// <summary>
        /// Render out the section containing hit points and healing surges.
        /// </summary>
        /// <param name="displayCharacter">
        /// The <see cref="DisplayCharacter"/> to display.
        /// </param>
        /// <param name="resourceDictionary">
        /// A <see cref="ResourceDictionary"/> containing styles.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>
        private TableRowGroup RenderHitPoints(DisplayCharacter displayCharacter, ResourceDictionary resourceDictionary)
        {
            if (displayCharacter == null)
            {
                throw new ArgumentNullException("displayCharacter");
            }
            DictionaryHelper.Expect<Style>(resourceDictionary, StyleHelper.TraitHeaderRowStyleName);

            TableRowGroup tableRowGroup;
            TableRow tableRow; // Reused
            TableCell tableCell; // Reused
            Style style;

            style = (Style)resourceDictionary[StyleHelper.TraitHeaderRowStyleName];

            tableRowGroup = new TableRowGroup();
            tableRowGroup.Style = style;
            tableRow = new TableRow();

            // Add the Hit Points title
            tableCell = new TableCell() { ColumnSpan = NameColumnSpan + NumEncounters };
            tableCell.Blocks.Add(new Paragraph(new Run(string.Format("Hit Points: {0}",
                displayCharacter.Character[ScoreType.HitPoints].ToString("CS")))));
            tableRow = new TableRow();
            tableRow.Cells.Add(tableCell);
            tableRowGroup.Rows.Add(tableRow);

            // Add the Hit Points box
            tableCell = new TableCell() { ColumnSpan = NameColumnSpan + NumEncounters };

            Table hitPointsTable;
            TableRowGroup hitPointsTableRowGroup;
            TableRow hitPointsTableRow;
            TableCell hitPointsTableCell;
            Paragraph hitPointParagraph;

            hitPointParagraph = new Paragraph();
            hitPointParagraph.Inlines.Add(new Run(displayCharacter.Character.HitPoints.Total.ToString()));
            for (int i = 0; i < 4; i++)
            {
                hitPointParagraph.Inlines.Add(new LineBreak());
            }

            hitPointsTableCell = new TableCell();
            hitPointsTableCell.Blocks.Add(hitPointParagraph);

            hitPointsTableRow = new TableRow();
            hitPointsTableRow.Cells.Add(hitPointsTableCell);

            hitPointsTableRowGroup = new TableRowGroup();
            hitPointsTableRowGroup.Rows.Add(hitPointsTableRow);

            hitPointsTable = new Table();
            hitPointsTable.BorderBrush = Brushes.Black;
            hitPointsTable.BorderThickness = new Thickness(72 / 96.0); // 1pt
            hitPointsTable.Background = Brushes.White;
            hitPointsTable.Margin = new Thickness(0);
            hitPointsTable.Padding = new Thickness(0);
            hitPointsTable.RowGroups.Add(hitPointsTableRowGroup);

            tableCell.Blocks.Add(hitPointsTable);

            tableRow = new TableRow();
            tableRow.Cells.Add(tableCell);
            tableRowGroup.Rows.Add(tableRow);

            return tableRowGroup;
        }
    }
}
