using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using GammaWorldCharacter;
using GammaWorldCharacter.Origins;
using GammaWorldCharacter.Gear.Weapons;
using GammaWorldCharacter.Gear;
using GammaWorldCharacter.Gear.Armor;
using GammaWorldCharacter.Powers;

namespace GammaWorldCharacter.Samples
{
    /// <summary>
    /// Sample level 1 characters.
    /// </summary>
    public static class Level01Characters
    {
        /// <summary>
        /// A level 1 Android Cockroach.
        /// </summary>
        [Export]
        public static Character Clip
        {
            get
            {
                Character character;

                // Stats rolled randomly on 3d6
                character = new Character(new int[] { 12, 11, 10, 15 }, new Cockroach(), new Android(), ScoreType.Science)
                {
                    Name = "Clip"
                };
                character.SetHeldItem(Hand.Main, new MeleeWeapon(WeaponHandedness.OneHanded, WeaponWeight.Heavy));
                character.SetHeldItem(Hand.Off, new Shield());
                character.Gear.Add(new RangedWeapon(RangedType.Weapon, WeaponHandedness.OneHanded, WeaponWeight.Heavy));
                character.SetEquippedItem(new LightArmor());
                character.Gear.AddRange(ExplorersKit.Contents);
                character.Update();

                return character;
            }
        }

        /// <summary>
        /// A level 1 Doppelganger Electrokinetic.
        /// </summary>
        [Export]
        public static Character Keravnos
        {
            get
            {
                Character character;

                character = new Character(new int[] { 10, 13, 10, 5 }, new Doppelganger(), new Electrokinetic(), ScoreType.Mechanics)
                {
                    Name = "Keravnos"
                };
                character.SetHeldItem(Hand.Main, new MeleeWeapon(WeaponHandedness.TwoHanded, WeaponWeight.Light));
                character.Gear.Add(new RangedWeapon(RangedType.Weapon, WeaponHandedness.TwoHanded, WeaponWeight.Light));
                character.SetEquippedItem(new LightArmor());
                character.Gear.AddRange(ExplorersKit.Contents);
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

                character = new Character(new int[] { 8, 9, 8, 11 }, new Empath(), new Felinoid(), ScoreType.Stealth)
                {
                    Name = "Kitty"
                };
                character.SetHeldItem(Hand.Main, new MeleeWeapon(WeaponHandedness.OneHanded, WeaponWeight.Light));
                character.Gear.Add(new RangedWeapon(RangedType.Weapon, WeaponHandedness.TwoHanded, WeaponWeight.Light));
                character.SetEquippedItem(new LightArmor());
                character.Gear.AddRange(ExplorersKit.Contents);
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

                character = new Character(new int[] { 12, 10, 9, 8 }, new Giant(), new GravityController(), ScoreType.Insight)
                {
                    Name = "Virtus"
                };
                character.SetHeldItem(Hand.Main, new MeleeWeapon(WeaponHandedness.TwoHanded, WeaponWeight.Heavy));
                character.Gear.Add( new RangedWeapon(RangedType.Weapon, WeaponHandedness.TwoHanded, WeaponWeight.Heavy));
                character.SetEquippedItem(new HeavyArmor());
                character.Gear.AddRange(ExplorersKit.Contents);
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

                character = new Character(new int[] { 11, 7, 17, 5, 13 }, new Hawkoid(), new Hypercognitive(), ScoreType.Interaction)
                {
                    Name = "Hermes"
                };
                character.SetHeldItem(Hand.Main, new RangedWeapon(RangedType.Weapon, WeaponHandedness.TwoHanded, WeaponWeight.Light));
                character.Gear.Add(new MeleeWeapon(WeaponHandedness.OneHanded, WeaponWeight.Light));
                character.SetEquippedItem(new LightArmor());
                character.Gear.AddRange(ExplorersKit.Contents);
                character.Update();

                return character;
            }
        }
    }
}
