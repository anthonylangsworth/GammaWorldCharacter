using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using System.Windows;

namespace GammaWorldCharacterViewer.Renderers
{
    /// <summary>
    /// A helper class for dealing with <see cref="Style"/>s.
    /// </summary>
    /// <seealso cref="Styles"/>
    internal static class StyleHelper
    {
        /// <summary>
        /// Create the <see cref="Style"/>s and add them to the <paramref name="frameworkContentElement"/>'s
        /// ResourceDictionary.
        /// </summary>
        /// <param name="frameworkContentElement">
        /// The <see cref="FlowDocument"/> to add the styles to. This cannot be null.
        /// </param>
        /// <typeparam name="T">
        /// A type derived from <see cref="TextElement"/> the style is applied to.
        /// </typeparam>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="frameworkContentElement"/> cannot be null.
        /// </exception>
        public static void AddStyles<T>(FrameworkContentElement frameworkContentElement)
            where T:TextElement
        {
            if (frameworkContentElement == null)
            {
                throw new ArgumentNullException("frameworkContentElement");
            }

            frameworkContentElement.Resources.Add(HeaderStyleName, Styles.GetHeaderStyle<T>());
            frameworkContentElement.Resources.Add(DetailRowStyleName, Styles.GetDetailStyle<T>());
            frameworkContentElement.Resources.Add(TraitHeaderRowStyleName, Styles.GetTraitHeaderStyle<T>());
            frameworkContentElement.Resources.Add(AtWillHeaderRowStyleName, Styles.GetAtWillHeaderStyle<T>());
            frameworkContentElement.Resources.Add(FlavorTextStyleName, Styles.GetFlavorTextStyle<T>());
            frameworkContentElement.Resources.Add(EncounterHeaderRowStyleName, Styles.EncounterHeader<T>());
            frameworkContentElement.Resources.Add(ConsumableHeaderRowStyleName, Styles.ConsumableHeader<T>());

            // Post Conditions
            DictionaryHelper.Expect<Style>(frameworkContentElement.Resources, HeaderStyleName);
            DictionaryHelper.Expect<Style>(frameworkContentElement.Resources, DetailRowStyleName);
            DictionaryHelper.Expect<Style>(frameworkContentElement.Resources, TraitHeaderRowStyleName);
            DictionaryHelper.Expect<Style>(frameworkContentElement.Resources, AtWillHeaderRowStyleName);
            DictionaryHelper.Expect<Style>(frameworkContentElement.Resources, FlavorTextStyleName);
            DictionaryHelper.Expect<Style>(frameworkContentElement.Resources, EncounterHeaderRowStyleName);
            DictionaryHelper.Expect<Style>(frameworkContentElement.Resources, ConsumableHeaderRowStyleName);
        }

        /// <summary>
        /// The name of the heading style in the FlowDocument's Resources.
        /// </summary>
        public static readonly string HeaderStyleName = "HeaderStyle";

        /// <summary>
        /// The name of the detail row style in the FlowDocument's Resources.
        /// </summary>
        public static readonly string DetailRowStyleName = "DetailRowStyle";

        /// <summary>
        /// The name of the header style for traits in the FlowDocument's Resources.
        /// </summary>
        public static readonly string TraitHeaderRowStyleName = "TraitHeaderRowStyle";

        /// <summary>
        /// The name of the header style for at will powers in the FlowDocument's Resources.
        /// </summary>
        public static readonly string AtWillHeaderRowStyleName = "AtWillHeaderRowStyle";

        /// <summary>
        /// The name of the header style for flavor text in the FlowDocument's Resources.
        /// </summary>
        public static readonly string FlavorTextStyleName = "FlavorTextStyle";

        /// <summary>
        /// The name of the header style for encounter powers in the FlowDocument's Resources.
        /// </summary>
        public static readonly string EncounterHeaderRowStyleName = "EncounterHeaderRowStyle";

        /// <summary>
        /// The name of the header style for at powers from consumables in the FlowDocument's Resources.
        /// </summary>
        public static readonly string ConsumableHeaderRowStyleName = "ConsumableHeaderRowStyle";
    }
}
