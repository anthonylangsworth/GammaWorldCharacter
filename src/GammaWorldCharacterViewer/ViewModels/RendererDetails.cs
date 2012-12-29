using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using GammaWorldCharacterViewer.Renderers;

namespace GammaWorldCharacterViewer.ViewModels
{
    /// <summary>
    /// A class containing information about how to display details of the renderer.
    /// </summary>
    public class RendererDetails
    {
        /// <summary>
        /// Create a new <see cref="RendererDetails"/>.
        /// </summary>
        /// <param name="renderer">
        /// The <see cref="FlowDocumentRenderer"/> to use to render the character.
        /// </param>
        /// <param name="name">
        /// A human readable name of the renderer.
        /// </param>
        /// <param name="image">
        /// The image to display.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>
        public RendererDetails(FlowDocumentRenderer renderer, string name, BitmapImage image)
        {
            if (renderer == null)
            {
                throw new ArgumentNullException("renderer");
            }
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            if (image == null)
            {
                throw new ArgumentNullException("image");
            }

            Renderer = renderer;
            Name = name;
            Image = image;
        }

        /// <summary>
        /// The <see cref="FlowDocumentRenderer"/> to use to render the character.
        /// </summary>
        public FlowDocumentRenderer Renderer
        {
            get;
            private set;
        }

        /// <summary>
        /// A human readable name of the renderer.
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// The image to display.
        /// </summary>
        public BitmapImage Image
        {
            get;
            private set;
        }
    }
}
