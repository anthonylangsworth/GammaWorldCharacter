using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using GammaWorldCharacter.Powers;
using GammaWorldCharacter.Origins;

namespace GammaWorldCharacterViewer.Renderers
{
    /// <summary>
    /// Commonly used styles for <see cref="FlowDocumentRenderer"/>s.
    /// </summary>
    /// <seealso cref="StyleHelper"/>
    internal static class Styles
    {
        private const string fontName = "Arial";
        private const double fontSize = 10.0 * 96.0 / 72.0; // 10pt

        /// <summary>
        /// A header <see cref="Block"/>. This is used for the top row; 
        /// showing the character name, class, level and so on; and the header for
        /// each type of <see cref="Power"/>.
        /// </summary>
        /// <typeparam name="T">
        /// A type derived from <see cref="TextElement"/> the style is applied to.
        /// </typeparam>
        internal static Style GetHeaderStyle<T>()
            where T : TextElement
        {
            Style headerStyle;
            headerStyle = new Style(typeof(Block));
            headerStyle.Setters.Add(new Setter(Table.ForegroundProperty, Brushes.White));
            headerStyle.Setters.Add(new Setter(Table.BackgroundProperty, new SolidColorBrush(Color.FromArgb(0xff, 0x10, 0x40, 0x10))));
            headerStyle.Setters.Add(new Setter(Table.FontFamilyProperty, new FontFamily(fontName)));
            headerStyle.Setters.Add(new Setter(Table.FontSizeProperty, fontSize));
            headerStyle.Setters.Add(new Setter(Table.PaddingProperty, new Thickness(2.0 * 96.0 / 72.0))); // 2pt
            headerStyle.Setters.Add(new Setter(Table.MarginProperty, new Thickness(0)));
            headerStyle.Setters.Add(new Setter(Table.TextAlignmentProperty, TextAlignment.Left));
            headerStyle.Seal();

            return headerStyle;
        }

        /// <summary>
        /// A detail <see cref="Block"/>. This is used for the statitics and equipment sections, along with the details
        /// of each power.
        /// </summary>
        /// <typeparam name="T">
        /// A type derived from <see cref="TextElement"/> the style is applied to.
        /// </typeparam>
        internal static Style GetDetailStyle<T>()
            where T : TextElement
        {
            const double twoPoint = 2.0 * 96.0 / 72.0;
            const double indent = twoPoint; // 0.2 * 96.0; // 0.2 inches

            Style detailRowStyle;
            detailRowStyle = new Style(typeof(T));
            detailRowStyle.Setters.Add(new Setter(Table.ForegroundProperty, Brushes.Black));
            detailRowStyle.Setters.Add(new Setter(Table.BackgroundProperty, Brushes.White));
            detailRowStyle.Setters.Add(new Setter(Table.FontFamilyProperty, new FontFamily(fontName)));
            detailRowStyle.Setters.Add(new Setter(Table.FontSizeProperty, fontSize));
            detailRowStyle.Setters.Add(new Setter(Table.PaddingProperty, new Thickness(indent, twoPoint, twoPoint, twoPoint)));
            detailRowStyle.Setters.Add(new Setter(Table.MarginProperty, new Thickness(0)));
            detailRowStyle.Setters.Add(new Setter(Table.TextAlignmentProperty, TextAlignment.Left));
            detailRowStyle.Seal();
            return detailRowStyle;
        }

        /// <summary>
        /// The header <see cref="Block"/> used for non-powers, such as <see cref="Feat"/>s, <see cref="RacialTrait"/>s,
        /// <see cref="ClassFeature"/>s or the like.
        /// </summary>
        /// <typeparam name="T">
        /// A type derived from <see cref="TextElement"/> the style is applied to.
        /// </typeparam>
        internal static Style GetTraitHeaderStyle<T>()
            where T : TextElement
        {
            Style traitHeaderRowStyle;
            traitHeaderRowStyle = new Style(typeof(T));
            traitHeaderRowStyle.Setters.Add(new Setter(Table.ForegroundProperty, Brushes.Black));
            traitHeaderRowStyle.Setters.Add(new Setter(Table.BackgroundProperty, new SolidColorBrush(Color.FromRgb(0xe0, 0xe0, 0xe0))));
            traitHeaderRowStyle.Setters.Add(new Setter(Table.FontFamilyProperty, new FontFamily(fontName)));
            traitHeaderRowStyle.Setters.Add(new Setter(Table.FontSizeProperty, fontSize));
            traitHeaderRowStyle.Setters.Add(new Setter(Table.PaddingProperty, new Thickness(2.0 * 96.0 / 72.0))); // 2pt
            traitHeaderRowStyle.Setters.Add(new Setter(Table.MarginProperty, new Thickness(0)));
            traitHeaderRowStyle.Setters.Add(new Setter(Table.TextAlignmentProperty, TextAlignment.Left));
            traitHeaderRowStyle.Seal();
            return traitHeaderRowStyle;
        }

        /// <summary>
        /// The header <see cref="Block"/> used for an at-will power.
        /// </summary>
        /// <typeparam name="T">
        /// A type derived from <see cref="TextElement"/> the style is applied to.
        /// </typeparam>
        internal static Style GetAtWillHeaderStyle<T>()
            where T : TextElement
        {
            Style atWillHeaderRowStyle;
            atWillHeaderRowStyle = new Style(typeof(T));
            atWillHeaderRowStyle.Setters.Add(new Setter(Table.ForegroundProperty, Brushes.White));
            atWillHeaderRowStyle.Setters.Add(new Setter(Table.BackgroundProperty, new SolidColorBrush(Color.FromRgb(0x00, 0x90, 0x00))));
            atWillHeaderRowStyle.Setters.Add(new Setter(Table.FontFamilyProperty, new FontFamily(fontName)));
            atWillHeaderRowStyle.Setters.Add(new Setter(Table.FontSizeProperty, fontSize));
            atWillHeaderRowStyle.Setters.Add(new Setter(Table.PaddingProperty, new Thickness(2.0 * 96.0 / 72.0))); // 2pt
            atWillHeaderRowStyle.Setters.Add(new Setter(Table.MarginProperty, new Thickness(0)));
            atWillHeaderRowStyle.Setters.Add(new Setter(Table.TextAlignmentProperty, TextAlignment.Left));
            atWillHeaderRowStyle.Seal();
            return atWillHeaderRowStyle;
        }

        /// <summary>
        /// The <see cref="Block"/> used for power flavor text.
        /// </summary>
        /// <typeparam name="T">
        /// A type derived from <see cref="TextElement"/> the style is applied to.
        /// </typeparam>
        internal static Style GetFlavorTextStyle<T>()
            where T:TextElement
        {
            Style dailyHeaderRowStyle;
            dailyHeaderRowStyle = new Style(typeof(T));
            dailyHeaderRowStyle.Setters.Add(new Setter(Table.ForegroundProperty, Brushes.Black));
            dailyHeaderRowStyle.Setters.Add(new Setter(Table.BackgroundProperty, new SolidColorBrush(Colors.Wheat)));
            dailyHeaderRowStyle.Setters.Add(new Setter(Table.FontStyleProperty, FontStyles.Italic));
            dailyHeaderRowStyle.Setters.Add(new Setter(Table.FontFamilyProperty, new FontFamily(fontName)));
            dailyHeaderRowStyle.Setters.Add(new Setter(Table.FontSizeProperty, fontSize));
            dailyHeaderRowStyle.Setters.Add(new Setter(Table.PaddingProperty, new Thickness(2.0 * 96.0 / 72.0))); //2pt
            dailyHeaderRowStyle.Setters.Add(new Setter(Table.MarginProperty, new Thickness(0)));
            dailyHeaderRowStyle.Setters.Add(new Setter(Table.TextAlignmentProperty, TextAlignment.Left));
            dailyHeaderRowStyle.Seal();
            return dailyHeaderRowStyle;
        }

        /// <summary>
        /// The header <see cref="Block"/> used for an encounter power.
        /// </summary>
        /// <typeparam name="T">
        /// A type derived from <see cref="TextElement"/> the style is applied to.
        /// </typeparam>
        internal static Style EncounterHeader<T>()
            where T : TextElement
        {
            Style encounterHeaderRowStyle;
            encounterHeaderRowStyle = new Style(typeof(T));
            encounterHeaderRowStyle.Setters.Add(new Setter(Table.ForegroundProperty, Brushes.White));
            encounterHeaderRowStyle.Setters.Add(new Setter(Table.BackgroundProperty, new SolidColorBrush(Color.FromRgb(0xA0, 0x00, 0x00))));
            encounterHeaderRowStyle.Setters.Add(new Setter(Table.FontFamilyProperty, new FontFamily(fontName)));
            encounterHeaderRowStyle.Setters.Add(new Setter(Table.FontSizeProperty, fontSize));
            encounterHeaderRowStyle.Setters.Add(new Setter(Table.PaddingProperty, new Thickness(2.0 * 96.0 / 72.0))); // 2pt
            encounterHeaderRowStyle.Setters.Add(new Setter(Table.MarginProperty, new Thickness(0)));
            encounterHeaderRowStyle.Setters.Add(new Setter(Table.TextAlignmentProperty, TextAlignment.Left));
            encounterHeaderRowStyle.Seal();
            return encounterHeaderRowStyle;
        }

        /// <summary>
        /// The header <see cref="Block"/> used for using a consumable, e.g. drinking a potion.
        /// </summary>
        /// <typeparam name="T">
        /// A type derived from <see cref="TextElement"/> the style is applied to.
        /// </typeparam>
        internal static Style ConsumableHeader<T>()
            where T : TextElement
        {
            Style consumableHeaderRowStyle;
            consumableHeaderRowStyle = new Style(typeof(T));
            consumableHeaderRowStyle.Setters.Add(new Setter(Table.ForegroundProperty, Brushes.White));
            consumableHeaderRowStyle.Setters.Add(new Setter(Table.BackgroundProperty, new SolidColorBrush(Color.FromRgb(0xd0, 0xa0, 0x40))));
            consumableHeaderRowStyle.Setters.Add(new Setter(Table.FontFamilyProperty, new FontFamily(fontName)));
            consumableHeaderRowStyle.Setters.Add(new Setter(Table.FontSizeProperty, fontSize));
            consumableHeaderRowStyle.Setters.Add(new Setter(Table.PaddingProperty, new Thickness(2.0 * 96.0 / 72.0))); // 2pt
            consumableHeaderRowStyle.Setters.Add(new Setter(Table.MarginProperty, new Thickness(0)));
            consumableHeaderRowStyle.Setters.Add(new Setter(Table.TextAlignmentProperty, TextAlignment.Left));
            consumableHeaderRowStyle.Seal();
            return consumableHeaderRowStyle;
        }

    }
}
