using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using GammaWorldCharacter;

namespace GammaWorldCharacterViewer.Renderers
{
    /// <summary>
    /// An entry of a <see cref="PowerEntry"/> on the character renderer character sheet.
    /// Note that "power" may also mean racial trait, class feature and so on.
    /// </summary>
    public class PowerEntry
    {
        /// <summary>
        /// Create a new <see cref="PowerEntry"/>. All arguments to this method are assumed 
        /// to be escaped for HTML already.
        /// </summary>
        /// <param name="heading">
        /// The heading <see cref="Block"/>. This cannot be null.
        /// </param>
        /// <param name="flavorText">
        /// The flavor text <see cref="Block"/>. This may be null.
        /// </param>
        /// <param name="detail">
        /// The detail <see cref="Block"/>.  This cannot be null.
        /// </param>
        /// <param name="modifierSource">
        /// The <see cref="ModifierSource"/> that this <see cref="PowerEntry"/> was created for.
        /// This may be null.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Only <paramref name="modifierSource"/> can be null.
        /// </exception>
        public PowerEntry(Block heading, Block flavorText, Block detail, ModifierSource modifierSource)
        {
            if (heading == null)
            {
                throw new ArgumentNullException("heading");
            }
            if (detail == null)
            {
                throw new ArgumentNullException("detail");
            }

            Heading = heading;
            FlavorText = flavorText;
            Detail = detail;
            ModifierSource = modifierSource;
        }

        /// <summary>
        /// The description or bottom row.
        /// </summary>
        public Block Detail
        {
            get;
            private set;
        }

        /// <summary>
        /// The flavor text
        /// </summary>
        public Block FlavorText
        {
            get;
            private set;
        }

        /// <summary>
        /// The heading (i.e. additional information after the name).
        /// </summary>
        public Block Heading
        {
            get;
            private set;
        }

        /// <summary>
        /// The <see cref="ModifierSource"/> the <see cref="PowerEntry"/> was created for.
        /// </summary>
        public ModifierSource ModifierSource
        {
            get;
            private set;
        }
    }
}
