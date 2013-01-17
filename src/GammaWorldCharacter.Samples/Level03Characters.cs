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
        /// A level 3 Android Cockroach.
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

        /// <summary>
        /// A level 3 Android Cockroach (using other origin utility power instead).
        /// </summary>
        [Export]
        public static Character ClipAlternate
        {
            get
            {
                Character character;

                character = Level02Characters.Clip;
                character.Name = character.Name + " (Alternate)";
                character.AddLevels(new Level03(OriginChoice.Primary));
                character.Update();

                return character;
            }
        }

        /// <summary>
        /// A level 3 Doppelganger Electrokinetic.
        /// </summary>
        [Export]
        public static Character Keravnos
        {
            get
            {
                Character character;

                character = Level02Characters.Keravnos;
                character.AddLevels(new Level03(OriginChoice.Secondary));
                character.Update();

                return character;
            }
        }

        /// <summary>
        /// A level 3 Doppelganger Electrokinetic (using other origin utility power instead).
        /// </summary>
        [Export]
        public static Character KeravnosAlternate
        {
            get
            {
                Character character;

                character = Level02Characters.Keravnos;
                character.Name = character.Name + " (Alternate)";
                character.AddLevels(new Level03(OriginChoice.Primary));
                character.Update();

                return character;
            }
        }

        /// <summary>
        /// A level 3 Empath Felinoid.
        /// </summary>
        [Export]
        public static Character Kitty
        {
            get
            {
                Character character;

                character = Level02Characters.Kitty;
                character.AddLevels(new Level03(OriginChoice.Primary));
                character.Update();

                return character;
            }
        }

        /// <summary>
        /// A level 3 Empath Felinoid (using other origin utility power instead).
        /// </summary>
        [Export]
        public static Character KittyAlternate
        {
            get
            {
                Character character;

                character = Level02Characters.Kitty;
                character.Name = character.Name + " (Alternate)";
                character.AddLevels(new Level03(OriginChoice.Secondary));
                character.Update();

                return character;
            }
        }

        /// <summary>
        /// A level 3 Giant Gravity Controller.
        /// </summary>
        [Export]
        public static Character Virtus
        {
            get
            {
                Character character;

                character = Level02Characters.Virtus;
                character.AddLevels(new Level03(OriginChoice.Primary));
                character.Update();

                return character;
            }
        }

        /// <summary>
        /// A level 3 Giant Gravity Controller (using the other origin utility power instead).
        /// </summary>
        [Export]
        public static Character VirtusAlternate
        {
            get
            {
                Character character;

                character = Level02Characters.Virtus;
                character.Name = character.Name + " (Alternate)";
                character.AddLevels(new Level03(OriginChoice.Secondary));
                character.Update();

                return character;
            }
        }

        /// <summary>
        /// A level 3 Hawkoid Hypercognitive.
        /// </summary>
        [Export]
        public static Character Hermes
        {
            get
            {
                Character character;

                character = Level02Characters.Hermes;
                character.AddLevels(new Level03(OriginChoice.Primary));
                character.Update();

                return character;
            }
        }

        /// <summary>
        /// A level 3 Hawkoid Hypercognitive (using the other origin utility power instead).
        /// </summary>
        [Export]
        public static Character HermesAlternate
        {
            get
            {
                Character character;

                character = Level02Characters.Hermes;
                character.Name = character.Name + " (Alternate)";
                character.AddLevels(new Level03(OriginChoice.Secondary));
                character.Update();

                return character;
            }
        }

    }
}
