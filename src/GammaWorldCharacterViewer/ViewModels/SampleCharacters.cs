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
            SourceCharacters = null;
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
                    CompositionBatch compositionBatch;
                    using(CompositionContainer container = new CompositionContainer(
                        new AssemblyCatalog("GammaWorldCharacterGenerator.Samples.dll")))
                    {
                        compositionBatch = new CompositionBatch();
                        compositionBatch.AddPart(this);

                        container.Compose(compositionBatch);

                        characters = new List<DisplayCharacter>(SourceCharacters.Select(x => new DisplayCharacter(x))).AsReadOnly();
                    }
                }

                return characters;
            }
        }

        /// <summary>
        /// Characters loaded via MEF in <see cref="Characters"/>.
        /// </summary>
        [ImportMany]
        private IEnumerable<Character> SourceCharacters { get; set; }
    }
}
