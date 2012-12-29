using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.ComponentModel;
using System.Windows.Documents;
using GammaWorldCharacterViewer.Renderers;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using System.Windows.Media.Imaging;

namespace GammaWorldCharacterViewer.ViewModels
{
    /// <summary>
    /// The main view model.
    /// </summary>
    public class ViewModel: DependencyObject, INotifyPropertyChanged
    {
        private FlowDocument characterSheet;

        /// <summary>
        /// Create a new <see cref="ViewModel"/>.
        /// </summary>
        public ViewModel()
        {
            Characters = CollectionViewSource.GetDefaultView(new SampleCharacters().Characters);
            Characters.SortDescriptions.Add(new SortDescription("Character.PlayerName", ListSortDirection.Ascending));
            Characters.SortDescriptions.Add(new SortDescription("Character.Name", ListSortDirection.Ascending));
            Characters.SortDescriptions.Add(new SortDescription("Character.Level", ListSortDirection.Ascending));
            Characters.GroupDescriptions.Add(new PropertyGroupDescription("Character.PlayerName"));
            Characters.GroupDescriptions.Add(new PropertyGroupDescription("Character.Name"));
            Characters.CurrentChanged += CurrentCharacterChanged;

            Renderers = CollectionViewSource.GetDefaultView(new RendererDetails[] 
                { 
                    new RendererDetails(new CustomDocumentRenderer(){ ShowModifiers=false }, "Character Sheet", new BitmapImage(new Uri("/Images/CharacterSheet.ico", UriKind.Relative))),
                    new RendererDetails(new FlowDocumentPowerTracker(), "Power Tracker", new BitmapImage(new Uri("/Images/PowerTracker.ico", UriKind.Relative)))
                });
            Renderers.CurrentChanged += CurrentRendererChanged;

            characterSheet = null;
        }

        /// <summary>
        /// Called when the selected character in <see cref="Character"/>s is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CurrentCharacterChanged(object sender, EventArgs e)
        {
            characterSheet = null;
            PropertyChanged(this, new PropertyChangedEventArgs("CharacterSheet"));
        }

        /// <summary>
        /// Called when the selected renderer in <see cref="Renderers"/> is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CurrentRendererChanged(object sender, EventArgs e)
        {
            characterSheet = null;
            PropertyChanged(this, new PropertyChangedEventArgs("CharacterSheet"));
        }

        /// <summary>
        /// The <see cref="FlowDocumentRenderer"/>s supported.
        /// </summary>
        public ICollectionView Renderers
        {
            get;
            private set;
        }

        /// <summary>
        /// The <see cref="DisplayCharacter"/>s supported.
        /// </summary>
        public ICollectionView Characters
        {
            get;
            private set;
        }

        /// <summary>
        /// Fired when a property is changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The character sheet.
        /// </summary>
        public FlowDocument CharacterSheet
        {
            get
            {
                DisplayCharacter sampleCharacter;
                RendererDetails rendererDetails;

                sampleCharacter = Characters.CurrentItem as DisplayCharacter;
                rendererDetails = Renderers.CurrentItem as RendererDetails;

                if (sampleCharacter != null
                    && rendererDetails != null
                    && characterSheet == null)
                {
                    characterSheet = rendererDetails.Renderer.Render(sampleCharacter);
                }

                return characterSheet;
            }
        }
    }
}
