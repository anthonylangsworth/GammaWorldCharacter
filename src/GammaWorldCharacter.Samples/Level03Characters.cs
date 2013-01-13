using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Levels;

namespace GammaWorldCharacter.Samples
{
    public static class Level03Characters
    {
        /// <summary>
        /// A level 2 Android Cockroach.
        /// </summary>
        [Export]
        public static Character Clip
        {
            get
            {
                Character character;

                character = Level02Characters.Clip;
                character.AddLevels(new Level03(OriginChoice.Secondary));
                character.Update();

                return character;
            }
        }
    }
}
