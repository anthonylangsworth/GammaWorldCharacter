using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;

namespace GammaWorldCharacterViewer.Renderers
{
    /// <summary>
    /// Base class for do
    /// </summary>
    public abstract class FlowDocumentRenderer
    {
        /// <summary>
        /// True if modifiers are expanded and included in the output, false if only totals are shown.
        /// </summary>
        public bool ShowModifiers
        {
            get;
            set;
        }

        /// <summary>
        /// Render out the <see cref="DisplayCharacter"/> to the given <see cref="FlowDocument"/>.
        /// </summary>
        /// <param name="displayCharacter">
        /// The <see cref="DisplayCharacter"/> to render.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>
        public abstract FlowDocument Render(DisplayCharacter displayCharacter);
    }
}
