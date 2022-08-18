using GammaWorldCharacter.Gear;
using GammaWorldCharacter.Gear.Armor;
using GammaWorldCharacter.Gear.Weapons;
using GammaWorldCharacter.Serialization;
using NUnit.Framework;
using System.Collections.Generic;

namespace GammaWorldCharacter.Test.Unit.Serialization
{
    public class TestItemConverter : TestConverter<ItemConverter, Item>
    {
        public override IEnumerable<TestCaseData> TestSerializationSource()
        {
            yield return new TestCaseData(new RangedWeapon(RangedType.Gun, WeaponHandedness.OneHanded, WeaponWeight.Heavy))
                .Returns("{\"type\":\"Ranged Weapon\",\"rangedType\":\"Gun\",\"weight\":\"Heavy\",\"handedness\":1}");
            yield return new TestCaseData(new RangedWeapon(RangedType.Weapon, WeaponHandedness.OneHanded, WeaponWeight.Heavy))
                .Returns("{\"type\":\"Ranged Weapon\",\"rangedType\":\"Weapon\",\"weight\":\"Heavy\",\"handedness\":1}");
            yield return new TestCaseData(new RangedWeapon(RangedType.Gun, WeaponHandedness.TwoHanded, WeaponWeight.Heavy))
                .Returns("{\"type\":\"Ranged Weapon\",\"rangedType\":\"Gun\",\"weight\":\"Heavy\",\"handedness\":2}");
            yield return new TestCaseData(new RangedWeapon(RangedType.Gun, WeaponHandedness.OneHanded, WeaponWeight.Light))
                .Returns("{\"type\":\"Ranged Weapon\",\"rangedType\":\"Gun\",\"weight\":\"Light\",\"handedness\":1}");
            yield return new TestCaseData(new MeleeWeapon(WeaponHandedness.OneHanded, WeaponWeight.Heavy))
                .Returns("{\"type\":\"Melee Weapon\",\"weight\":\"Heavy\",\"handedness\":1}");
            yield return new TestCaseData(new MeleeWeapon(WeaponHandedness.TwoHanded, WeaponWeight.Heavy))
                .Returns("{\"type\":\"Melee Weapon\",\"weight\":\"Heavy\",\"handedness\":2}");
            yield return new TestCaseData(new MeleeWeapon(WeaponHandedness.OneHanded, WeaponWeight.Light))
                .Returns("{\"type\":\"Melee Weapon\",\"weight\":\"Light\",\"handedness\":1}");
            yield return new TestCaseData(new MeleeWeapon(WeaponHandedness.TwoHanded, WeaponWeight.Heavy))
                .Returns("{\"type\":\"Melee Weapon\",\"weight\":\"Heavy\",\"handedness\":2}");
            yield return new TestCaseData(new HeavyArmor()).Returns("{\"type\":\"Heavy Armor\"}");
            yield return new TestCaseData(new LightArmor()).Returns("{\"type\":\"Light Armor\"}");
            yield return new TestCaseData(new Shield()).Returns("{\"type\":\"Shield\"}");
            yield return new TestCaseData(new Item("Backpack", Slot.None)).Returns("{\"type\":\"Item\",\"name\":\"Backpack\"}");
            yield return new TestCaseData(new Item("Uber Item", Slot.Hands)).Returns("{\"type\":\"Item\",\"name\":\"Uber Item\",\"slot\":\"Hands\"}");
            yield return new TestCaseData(new Item("Uber Item", Slot.Body)).Returns("{\"type\":\"Item\",\"name\":\"Uber Item\",\"slot\":\"Body\"}");
            yield return new TestCaseData(null).Returns("null");
        }

        public override IEnumerable<TestCaseData> TestDeserializationSource()
        {
            yield return new TestCaseData("{\"type\":\"Ranged Weapon\",\"rangedType\":\"Gun\",\"weight\":\"Heavy\",\"handedness\":1}")
                .Returns(new RangedWeapon(RangedType.Gun, WeaponHandedness.OneHanded, WeaponWeight.Heavy));
            yield return new TestCaseData("{\"type\":\"Ranged Weapon\",\"rangedType\":\"Weapon\",\"weight\":\"Heavy\",\"handedness\":1}")
                .Returns(new RangedWeapon(RangedType.Weapon, WeaponHandedness.OneHanded, WeaponWeight.Heavy));
            yield return new TestCaseData("{\"type\":\"Ranged Weapon\",\"rangedType\":\"Gun\",\"weight\":\"Heavy\",\"handedness\":2}")
                .Returns(new RangedWeapon(RangedType.Gun, WeaponHandedness.TwoHanded, WeaponWeight.Heavy));
            yield return new TestCaseData("{\"type\":\"Ranged Weapon\",\"rangedType\":\"Gun\",\"weight\":\"Light\",\"handedness\":1}")
                .Returns(new RangedWeapon(RangedType.Gun, WeaponHandedness.OneHanded, WeaponWeight.Light));
            //yield return new TestCaseData("{\"type\":\"Ranged Weapon\",\"rangedType\":\"Foo\",\"weight\":\"Heavy\",\"handedness\":1}")
            //    .Throws(typeof(InvalidSerializationException));
            //yield return new TestCaseData("{\"type\":\"Ranged Weapon\",\"rangedType\":\"Gun\",\"weight\":\"Foo\",\"handedness\":1}")
            //    .Throws(typeof(InvalidSerializationException));
            //yield return new TestCaseData("{\"type\":\"Ranged Weapon\",\"rangedType\":\"Gun\",\"weight\":\"Heavy\",\"handedness\":\"one\"}")
            //    .Throws(typeof(InvalidSerializationException));
            //yield return new TestCaseData("{\"type\":\"Ranged Weapon\",\"rangedType\":\"Gun\",\"weight\":\"Heavy\",\"handedness\":3}")
            //    .Throws(typeof(InvalidSerializationException));
            //yield return new TestCaseData("{\"type\":\"Ranged Weapon\",\"weight\":\"Heavy\",\"handedness\":1}")
            //    .Throws(typeof(InvalidSerializationException));
            //yield return new TestCaseData("{\"type\":\"Ranged Weapon\",\"rangedType\":\"Gun\",\"handedness\":1}")
            //    .Throws(typeof(InvalidSerializationException));
            //yield return new TestCaseData("{\"type\":\"Ranged Weapon\",\"rangedType\":\"Gun\",\"weight\":\"Heavy\"}")
            //    .Throws(typeof(InvalidSerializationException));

            yield return new TestCaseData("{\"type\":\"Melee Weapon\",\"weight\":\"Heavy\",\"handedness\":1}")
                .Returns(new MeleeWeapon(WeaponHandedness.OneHanded, WeaponWeight.Heavy));
            yield return new TestCaseData("{\"type\":\"Melee Weapon\",\"weight\":\"Heavy\",\"handedness\":2}")
                .Returns(new MeleeWeapon(WeaponHandedness.TwoHanded, WeaponWeight.Heavy));
            yield return new TestCaseData("{\"type\":\"Melee Weapon\",\"weight\":\"Light\",\"handedness\":1}")
                .Returns(new MeleeWeapon(WeaponHandedness.OneHanded, WeaponWeight.Light));
            yield return new TestCaseData("{\"type\":\"Melee Weapon\",\"weight\":\"Heavy\",\"handedness\":2}")
                .Returns(new MeleeWeapon(WeaponHandedness.TwoHanded, WeaponWeight.Heavy));
            //yield return new TestCaseData("{\"type\":\"Melee Weapon\",\"weight\":\"Foo\",\"handedness\":2}")
            //    .Throws(typeof(InvalidSerializationException));
            //yield return new TestCaseData("{\"type\":\"Melee Weapon\",\"weight\":\"Heavy\",\"handedness\":\"one\"}")
            //    .Throws(typeof(InvalidSerializationException));
            //yield return new TestCaseData("{\"type\":\"Melee Weapon\",\"weight\":\"Heavy\",\"handedness\":3}")
            //    .Throws(typeof(InvalidSerializationException));
            //yield return new TestCaseData("{\"type\":\"Melee Weapon\",\"handedness\":1}")
            //    .Throws(typeof(InvalidSerializationException));
            //yield return new TestCaseData("{\"type\":\"Melee Weapon\",\"weight\":\"Heavy\"}")
            //    .Throws(typeof(InvalidSerializationException));

            yield return new TestCaseData("{\"type\":\"Heavy Armor\"}").Returns(new HeavyArmor());
            yield return new TestCaseData("{\"type\":\"Light Armor\"}").Returns(new LightArmor());
            yield return new TestCaseData("{\"type\":\"Shield\"}").Returns(new Shield());
            yield return new TestCaseData("{\"type\":\"Item\",\"name\":\"Uber Item\",\"slot\":\"Hands\"}").Returns(new Item("Uber Item", Slot.Hands));
            yield return new TestCaseData("{\"type\":\"Item\",\"name\":\"Uber Item\",\"slot\":\"Body\"}").Returns(new Item("Uber Item", Slot.Body));
            yield return new TestCaseData("{\"type\":\"Item\",\"name\":\"Backpack\"}").Returns(new Item("Backpack", Slot.None));
            //yield return new TestCaseData("{\"type\":\"Item\"}").Throws(typeof(InvalidSerializationException));
            //yield return new TestCaseData("{\"type\":\"foo\",\"name\":\"Backpack\"}").Throws(typeof(InvalidSerializationException));
            //yield return new TestCaseData("{\"type\":\"Item\",\"name\":\"Uber Item\",\"slot\":\"Neck\"}").Throws(typeof(InvalidSerializationException));
            //yield return new TestCaseData("{ }").Throws(typeof(InvalidSerializationException));
            //yield return new TestCaseData("1").Throws(typeof(InvalidSerializationException));
            yield return new TestCaseData("null").Returns(null);
        }
    }
}
