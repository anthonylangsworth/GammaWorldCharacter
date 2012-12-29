using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Gear;
using GammaWorldCharacter.Gear.Armor;
using GammaWorldCharacter.Gear.Weapons;
using GammaWorldCharacter.Levels;
using GammaWorldCharacter.Origins;

namespace GammaWorldCharacter.Samples
{
    public static class Level02Characters
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

                character = Level01Characters.Clip;
                character.AddLevels(new Level02(OriginChoice.Primary));
                character.Update();

                return character;
            }
        }

        /// <summary>
        /// A level 2 Doppelganger Electrokinetic.
        /// </summary>
        [Export]
        public static Character Keravnos
        {
            get
            {
                Character character;

                character = Level01Characters.Keravnos;
                character.AddLevels(new Level02(OriginChoice.Primary));
                character.Update();

                return character;
            }
        }

        /// <summary>
        /// A level 1 Empath Felinoid.
        /// </summary>
        [Export]
        public static Character Kitty
        {
            get
            {
                Character character;

                character = Level01Characters.Kitty;
                character.AddLevels(new Level02(OriginChoice.Primary));
                character.Update();

                return character;
            }
        }

        /// <summary>
        /// A level 1 Giant Gravity Controller.
        /// </summary>
        [Export]
        public static Character Virtus
        {
            get
            {
                Character character;

                character = Level01Characters.Virtus;
                character.AddLevels(new Level02(OriginChoice.Primary));
                character.Update();

                return character;
            }
        }

        /// <summary>
        /// A level 1 Hawkoid Hypercognitive.
        /// </summary>
        [Export]
        public static Character Hermes
        {
            get
            {
                Character character;

                character = Level01Characters.Hermes;
                character.AddLevels(new Level02(OriginChoice.Primary));
                character.Update();

                return character;
            }
        }

    }
}
