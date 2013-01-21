using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using GammaWorldCharacter;
using GammaWorldCharacter.Origins;
using GammaWorldCharacter.Gear.Weapons;
using GammaWorldCharacter.Gear;
using GammaWorldCharacter.Gear.Armor;
using GammaWorldCharacter.Powers;
using GammaWorldCharacter.Powers.Origins;
using GammaWorldCharacter.Samples;
using GammaWorldCharacterViewer.Renderers;

namespace GammaWorldCharacterViewer.ViewModels
{
    /// <summary>
    /// Sample Gamma World characters.
    /// </summary>
    public class SampleCharacters
    {
        private IEnumerable<DisplayCharacter> characters;

        /// <summary>
        /// Create a new <see cref="SampleCharacters"/>.
        /// </summary>
        public SampleCharacters()
        {
            // Do nothing
        }

        /// <summary>
        /// Characters to display (loaded via MEF from the GammaWorldCharacterGenerator.Samples.dll assembly).
        /// </summary>
        public IEnumerable<DisplayCharacter> Characters
        {
            get
            {
                if (characters == null)
                {
                    using(CompositionContainer container = new CompositionContainer(
                        new AssemblyCatalog("GammaWorldCharacter.Samples.dll")))
                    {
                        characters = new List<DisplayCharacter>(
                            container.GetExportedValues<Character>().Select(x => new DisplayCharacter(x))).AsReadOnly();
                    }
                }

                return characters;
            }
        }
    }
}
