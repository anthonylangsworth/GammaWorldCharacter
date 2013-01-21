using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Levels;
using GammaWorldCharacter.Collections;
using GammaWorldCharacter.Gear;
using GammaWorldCharacter.Gear.Armor;
using GammaWorldCharacter.Gear.Weapons;
using GammaWorldCharacter.Powers;
using GammaWorldCharacter.Scores;
using GammaWorldCharacter.Origins;
using GammaWorldCharacter.Traits;

namespace GammaWorldCharacter
{
    /// <summary>
    /// A Gamma World character.
    /// </summary>
    public class Character: IEquatable<Character>
    {
        private AbilityScores abilityScores;
        private InternalModifierSource internalModifierSource;
        private List<Item> equipment;
        private Dictionary<Slot, Item> equippedItems;
        private List<Level> levels;
        private Dictionary<Hand, Item> heldItems;
        private string name;
        private string playerName;
        private List<Power> powers;
        private Dictionary<ScoreType, Score> scores;
        private List<Trait> traits;

        /// <summary>
        /// Create a new <see cref="Character"/>.
        /// </summary>
        /// <param name="abilityScores">
        /// The character's ability scores, e.g. Strength and so on.
        /// </param>
        /// <param name="primaryOrigin">
        /// The character's primary <see cref="Origin"/>.
        /// </param>
        /// <param name="secondaryOrigin">
        /// The character's secondary <see cref="Origin"/>.
        /// </param>
        /// <param name="trainedSkill">
        /// The initial trained skill.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No parameter can be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name=" trainedSkill"/> is not a skill.
        /// </exception>
        public Character(IEnumerable<int> abilityScores, Origin primaryOrigin, Origin secondaryOrigin, ScoreType trainedSkill)
        {
            if (abilityScores == null)
            {
                throw new ArgumentNullException("abilityScores");
            }
            if (primaryOrigin == null)
            {
                throw new ArgumentNullException("primaryOrigin");
            }
            if (secondaryOrigin == null)
            {
                throw new ArgumentNullException("secondaryOrigin");
            }
            if (!ScoreTypeHelper.IsSkill(trainedSkill))
            {
                throw new ArgumentException("Not a skill", "trainedSkill");
            }

            this.PrimaryOrigin = primaryOrigin;
            this.SecondaryOrigin = secondaryOrigin;
            this.TrainedSkill = trainedSkill;

            this.abilityScores = new AbilityScores(primaryOrigin, secondaryOrigin, abilityScores);
            this.internalModifierSource = new InternalModifierSource(trainedSkill, primaryOrigin.PowerSource);
            this.equipment = new List<Item>();
            this.levels = new List<Level>();
            this.playerName = "Unnamed";
            this.name = "Unnamed";

            // Create score for each ScoreType
            scores = CreateScores();

            // Create the equipment slots
            CreateEquippedItems();

            // Intentionally leave these uninitialized.
            powers = null;

            Update();
        }

        /// <summary>
        /// The character's primary <see cref="Origin"/>.
        /// </summary>
        public Origin PrimaryOrigin
        {
            get;
            private set;
        }

        /// <summary>
        /// The character's secondary <see cref="Origin"/>.
        /// </summary>
        public Origin SecondaryOrigin
        {
            get;
            private set;
        }

        /// <summary>
        /// The skill the character is initially trained in.
        /// </summary>
        public ScoreType TrainedSkill
        {
            get;
            private set;
        }

        /// <summary>
        /// Add the specified levels.
        /// </summary>
        /// <param name="newLevels">
        /// The levels to add.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// newLevels cannot be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// At least one level must be specified and levels must be contiguous, starting at the
        /// character's next level.
        /// </exception>
        public void AddLevels(params Level[] newLevels)
        {
            if (newLevels == null)
            {
                throw new ArgumentNullException("newLevels");
            }
            if (newLevels.Length == 0)
            {
                throw new ArgumentException("Need at least one level", "newLevels");
            }

            List<Level> workingLevels;
            int expectedLevel;

            // Ensure the provided levels are contiguous, start at the next level for the character
            // and are for this character's class.
            workingLevels = new List<Level>();
            workingLevels.AddRange(newLevels);
            workingLevels.Sort((x, y) => (x.Number - y.Number));
            for (int i = 0; i < workingLevels.Count; i++)
            {
                expectedLevel = Level + 1;

                if (workingLevels[i].Number != expectedLevel)
                {
                    throw new ArgumentException(string.Format("Level {0} out of order. Expected level {1}.",
                        workingLevels[i].Number, expectedLevel), "newLevels");
                }

                // Add the level
                levels.Add(workingLevels[i]);

                // Update to ensure requirements of abilities at each level are checked.
                Update();
            }
        }

        /// <summary>
        /// Are the <see cref="Character"/>s equal? (Ignores equipment)
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Character other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return scores.OrderBy(x => x.Key).SequenceEqual(other.scores.OrderBy(x => x.Key))
                && levels.OrderBy(x => x.Number).SequenceEqual(other.levels.OrderBy(x => x.Number))
                && string.Equals(name, other.name)
                && PrimaryOrigin.Equals(other.PrimaryOrigin)
                && SecondaryOrigin.Equals(other.SecondaryOrigin)
                && TrainedSkill == other.TrainedSkill;
        }

        /// <summary>
        /// Object equality.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Character) obj);
        }

        /// <summary>
        /// Hash code.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (abilityScores != null ? abilityScores.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (levels != null ? levels.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (name != null ? name.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (PrimaryOrigin != null ? PrimaryOrigin.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (SecondaryOrigin != null ? SecondaryOrigin.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (int) TrainedSkill;
                return hashCode;
            }
        }

        /// <summary>
        /// The non-equipped items the character is carrying.
        /// </summary>
        /// <seealso cref="GetHeldItem{T}"/>
        /// <seealso cref="SetEquippedItem"/>
        /// <seealso cref="SetHeldItem"/>
        public List<Item> Gear
        {
            get
            {
                return equipment;
            }
        }

        /// <summary>
        /// Get the <see cref="Item"/> equipped at the given slot.
        /// </summary>
        /// <param name="slot">
        /// The slot to get the item for.
        /// </param>
        /// <typeparam name="T">
        /// Cast the returned item to this type.
        /// </typeparam>
        /// <returns>
        /// The equipped item or null, if no item is equipped or the cast fails.
        /// </returns>
        /// <seealse cref="GetHeldItem{T}"/>
        /// <seealse cref="Gear"/>
        /// <seealso cref="SetEquippedItem"/>
        /// <seealso cref="SetHeldItem"/>
        public T GetEquippedItem<T>(Slot slot)
            where T : Item
        {
            return equippedItems[slot] as T;
        }

        /// <summary>
        /// The item held in the given hand or null, if no item is held.
        /// </summary>
        /// <remarks>
        /// To equip a versatile weapon in two hands, equip nothing in the
        /// off hand. Two-handed items should be equipped in the main hand.
        /// </remarks>
        /// <param name="hand">
        /// The hand to get the item from.
        /// </param>
        /// <typeparam name="T">
        /// Cast the returned item to the given type.
        /// </typeparam>
        /// <returns>
        /// The <see cref="Item"/> in that hand or null, if nothing is in that hand
        /// or the cast fails.
        /// </returns>
        /// <seealso cref="SetHeldItem"/>
        public T GetHeldItem<T>(Hand hand)
            where T : Item
        {
            return heldItems[hand] as T;
        }

        /// <summary>
        /// Traits
        /// </summary>
        public IEnumerable<Trait> GetTraits()
        {
            if (traits == null)
            {
                traits = new List<Trait>();

                foreach (ModifierSource modifierSource in ModifierSources.OfType<ITraitSource>())
                {
                    traits.AddRange(((ITraitSource)modifierSource).Traits);
                }
            }

            return traits;
        }

        /// <summary>
        /// Powers.
        /// </summary>
        public IEnumerable<Power> GetPowers()
        {
            if (powers == null)
            {
                powers = new List<Power>();

                foreach (ModifierSource modifierSource in ModifierSources.OfType<IPowerSource>())
                {
                    powers.AddRange(((IPowerSource)modifierSource).Powers);
                }
            }

            return powers;
        }

        /// <summary>
        /// The powers that are usuable by this Character at this time.
        /// </summary>
        public IEnumerable<Power> GetUsablePowers()
        {
            return GetPowers().Where(x => x.IsUsable(this));
        }

        /// <summary>
        /// Is this character trained in the given skill?
        /// </summary>
        /// <param name="skill">
        /// The skill  to check,
        /// </param>
        /// <returns>
        /// True if the character is trained in the given skill, false otherwise.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="skill"/> is not a skill.
        /// </exception>
        public bool IsTrainedInSkill(ScoreType skill)
        {
            if (!ScoreTypeHelper.IsSkill(skill))
            {
                throw new ArgumentException("Not a skill", "skill");
            }

            bool result;

            result = false;
            foreach (Modifier modifier in this[skill].AppliedModifiers)
            {
                if (modifier.ModifierValue == 4 && !modifier.Conditional
                    && (modifier.Source is Origin || modifier.Source is InternalModifierSource))
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Is the character wearing heavy armor?
        /// </summary>
        /// <returns>
        /// True if the character is wearing heavy armor, false otherwise.
        /// </returns>
        public bool IsWearingHeavyArmor()
        {
            Armor armorWorn;

            armorWorn = GetEquippedItem<Armor>(Slot.Body);

            return armorWorn != null && armorWorn is HeavyArmor;
        }

        /// <summary>
        /// The character's level.
        /// </summary>
        public int Level
        {
            get
            {
                return levels.Count + 1;
            }
        }

        /// <summary>
        /// The levels taken by the character.
        /// </summary>
        public IList<Level> Levels
        {
            get
            {
                return levels.AsReadOnly();
            }
        }

        /// <summary>
        /// Fired when each <see cref="ModifierSource"/> has just been updated. Useful
        /// for debugging.
        /// </summary>
        public event EventHandler<ModifierSourceUpdateEventArgs> ModifierSourceUpdated;

        /// <summary>
        /// Fired when each <see cref="ModifierSource"/> is about to be updated. Useful
        /// for debugging.
        /// </summary>
        public event EventHandler<ModifierSourceUpdateEventArgs> ModifierSourceUpdating;

        /// <summary>
        /// The character's name.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// The assigned value cannot be null, empty or whitespace.
        /// </exception>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException();
                }

                name = value;
            }
        }

        /// <summary>
        /// The player's name.
        /// </summary>
        public string PlayerName
        {
            get
            {
                return playerName;
            }
            set
            {
                playerName = value;
            }
        }

        /// <summary>
        /// Equip the given item.
        /// </summary>
        /// <returns>
        /// The item previously equipped in the slot.
        /// </returns>
        /// <param name="item">
        /// The item to equip. Pass null to unequip the item from the slot.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Items with no slot should be equipped using the <see cref="Gear"/> property. Weapons should be equipped
        /// using the <see cref="SetHeldItem"/> method.
        /// </exception>
        /// <seealso cref="Gear"/>
        /// <seealso cref="GetHeldItem{T}"/>
        /// <seealso cref="SetHeldItem"/>
        public Item SetEquippedItem(Item item)
        {
            if (item.Slot == Slot.None)
            {
                // TODO: Throw a more specific exception
                throw new ArgumentException("Items that lack a slot should be added using the Equipment property", "item");
            }
            if (item.Slot == Slot.Weapon)
            {
                // TODO: Throw a more specific exception
                throw new ArgumentException("To equip a weapon, call the MainHandItem or OffHandItem properties", "item");
            }

            Item previousItem = equippedItems[item.Slot];
            equippedItems[item.Slot] = item;
            return previousItem;
        }

        /// <summary>
        /// Equip an item in a hand.
        /// </summary>
        /// <param name="hand">
        /// The <see cref="Hand"/> to hold the item in.
        /// </param>
        /// <param name="item">
        /// The <see cref="Item"/> to hold or null, to remove any held item.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown when:
        /// <list type="bullet">
        /// <item>
        ///     <description>A weapon that is not off-hand cannot be equipped in the off-hand.</description>
        /// </item>
        /// <item>
        ///     <description>An off-hand weapon cannot be equipped if a two-handed weapon is equipped in the main hand.</description>
        /// </item>
        /// <item>
        ///     <description>A two-handed weapon cannot be equipped in the off-hand or if an item is held in the off-hand.</description>
        /// </item>
        /// </list>
        /// </exception>
        /// <seealso cref="GetEquippedItem{T}"/>
        /// <seealso cref="GetHeldItem{T}"/>
        /// <seealso cref="Gear"/>
        /// <seealso cref="SetEquippedItem"/>
        public Item SetHeldItem(Hand hand, Item item)
        {
            Weapon weapon;
            Item previousItem;

            //bool invalidWeaponConfiguration;
            //string exceptionMessage;

            if (item != null)
            {
                weapon = item as Weapon;

                if (hand == Hand.Main)
                {
                    if (weapon != null && weapon.Handedness == WeaponHandedness.TwoHanded
                        && heldItems[Hand.Off] != null)
                    {
                        throw new ArgumentException("Cannot equip two-handed weapon when off-hand holds a weapon");
                    }
                }
                else if (hand == Hand.Off)
                {
                    if (weapon != null)
                    {
                        //invalidWeaponConfiguration = false;
                        //exceptionMessage = string.Empty;
                        //if ((weapon.Properties & WeaponProperties.OffHand) != WeaponProperties.OffHand)
                        //{
                        //    invalidWeaponConfiguration = true;
                        //    exceptionMessage = "Weapon must be off-hand";
                        //}
                        //if (weapon.Handedness == WeaponHandedness.TwoHanded)
                        //{
                        //    invalidWeaponConfiguration = true;
                        //    exceptionMessage = "Cannot equip a two-handed weapon in the off-hand";
                        //}
                        //if (heldItems[Hand.Main] != null && heldItems[Hand.Main] is Weapon
                        //    && ((Weapon)heldItems[Hand.Main]).Handedness == WeaponHandedness.TwoHanded)
                        //{
                        //    invalidWeaponConfiguration = true;
                        //    exceptionMessage = "Two-handed weapon equipped";
                        //}
                        //if (GetEquippedItem<Shield>(Slot.Arms) != null)
                        //{
                        //    invalidWeaponConfiguration = true;
                        //    exceptionMessage = "Cannot equip off-hand weapon when shield is equipped";
                        //}
                    }
                }
                else
                {
                    throw new ArgumentException("Unknown hand", "hand");
                }
            }

            previousItem = heldItems[hand];
            heldItems[hand] = item;
            return previousItem;
        }

        /// <summary>
        /// Human-readable representation.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder stringBuilder;

            stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("{0} Level {1} {2} {3}:\n",
                Name, Level, PrimaryOrigin.Name, SecondaryOrigin.Name);
            stringBuilder.AppendFormat("Str {0}  Con {1}  Dex {2}  Int {3}  Wis {4}  Cha {5}\n",
                Strength.Total, Constitution.Total, Dexterity.Total, Intelligence.Total, Wisdom.Total, Charisma.Total);
            stringBuilder.AppendFormat("HP {0}  AC {1}  Fort {2}  Ref {3}  Will {4}\n",
                HitPoints.Total, ArmorClass.Total, Fortitude.Total, Reflex.Total, Will.Total);

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Update the character with recent changes.
        /// </summary>
        /// <remarks>
        /// This operation can be time consuming.
        /// </remarks>
        public void Update()
        {
            List<ModifierSource> modifierSources;
            GraphNode<ModifierSource> root;

            // Construct a list of ModifierSources.
            modifierSources = new List<ModifierSource>();
            modifierSources.AddRange(ModifierSources);

            // Invalidate these in case new ones have been added, e.g. adding a level.
            powers = null;

            // NOTE: Do not assume the source of a modifier is the ModifierSource.

            root = ModifierSourceGraphNodeHelper.ConstructDependencyGraph(this, modifierSources);

            // Remove existing modifiers
            RemoveModifiers();

            foreach (GraphNode<ModifierSource> modifierSourceGraphNode in root)
            {
                if (!(modifierSourceGraphNode.Data is NullModifierSource))
                {
                    RaiseModifierSourceUpdatingEvent(
                        new ModifierSourceUpdateEventArgs(modifierSourceGraphNode.Data));

                    modifierSourceGraphNode.Data.Update(this);
                    AddModifiers(modifierSourceGraphNode.Data);

                    RaiseModifierSourceUpdatedEvent(
                        new ModifierSourceUpdateEventArgs(modifierSourceGraphNode.Data));
                }
            }
        }

        /// <summary>
        /// Access a Character's scores.
        /// </summary>
        /// <param name="scoreType">
        /// The <see cref="ScoreType"/> of the score to access.
        /// </param>
        /// <returns>
        /// The <see cref="Score"/>.
        /// </returns>
        public Score this[ScoreType scoreType]
        {
            get
            {
                return scores[scoreType];
            }
        }

        /// <summary>
        /// The character's hit points.
        /// </summary>
        public Score HitPoints
        {
            get
            {
                return scores[ScoreType.HitPoints];
            }
        }

        /// <summary>
        /// The characters' bloodied hit point total.
        /// </summary>
        public Score Bloodied
        {
            get
            {
                return scores[ScoreType.Bloodied];
            }
        }

        /// <summary>
        /// The character's Armor Class (AC).
        /// </summary>
        public Score ArmorClass
        {
            get
            {
                return scores[ScoreType.ArmorClass];
            }
        }

        /// <summary>
        /// The character's Fortitude defense.
        /// </summary>
        public Score Fortitude
        {
            get
            {
                return scores[ScoreType.Fortitude];
            }
        }

        /// <summary>
        /// The character's Reflex defense.
        /// </summary>
        public Score Reflex
        {
            get
            {
                return scores[ScoreType.Reflex];
            }
        }

        /// <summary>
        /// The character's Will defense.
        /// </summary>
        public Score Will
        {
            get
            {
                return scores[ScoreType.Will];
            }
        }

        /// <summary>
        /// The number of squares the character can move in a move action.
        /// </summary>
        public Score Speed
        {
            get
            {
                return scores[ScoreType.Speed];
            }
        }

        /// <summary>
        /// The bonus to the character's initiative roll.
        /// </summary>
        public Score Initiative
        {
            get
            {
                return scores[ScoreType.Initiative];
            }
        }

        /// <summary>
        /// The character's Strength ability score.
        /// </summary>
        public Score Strength
        {
            get
            {
                return scores[ScoreType.Strength];
            }
        }

        /// <summary>
        /// The character's Constitution ability score.
        /// </summary>
        public Score Constitution
        {
            get
            {
                return scores[ScoreType.Constitution];
            }
        }

        /// <summary>
        /// The character's Dexterity ability score.
        /// </summary>
        public Score Dexterity
        {
            get
            {
                return scores[ScoreType.Dexterity];
            }
        }

        /// <summary>
        /// The character's Intelligence ability score.
        /// </summary>
        public Score Intelligence
        {
            get
            {
                return scores[ScoreType.Intelligence];
            }
        }

        /// <summary>
        /// The character's Wisdom ability score.
        /// </summary>
        public Score Wisdom
        {
            get
            {
                return scores[ScoreType.Wisdom];
            }
        }

        /// <summary>
        /// The character's Charisma ability score.
        /// </summary>
        public Score Charisma
        {
            get
            {
                return scores[ScoreType.Charisma];
            }
        }

        /// <summary>
        /// The character's Strength check bonus.
        /// </summary>
        public Score StrengthCheck
        {
            get
            {
                return scores[ScoreType.StrengthCheck];
            }
        }

        /// <summary>
        /// The character's Constitution check bonus.
        /// </summary>
        public Score ConstitutionCheck
        {
            get
            {
                return scores[ScoreType.ConstitutionCheck];
            }
        }

        /// <summary>
        /// The character's Dexterity check bonus.
        /// </summary>
        public Score DexterityCheck
        {
            get
            {
                return scores[ScoreType.DexterityCheck];
            }
        }

        /// <summary>
        /// The character's Intelligence check bonus.
        /// </summary>
        public Score IntelligenceCheck
        {
            get
            {
                return scores[ScoreType.IntelligenceCheck];
            }
        }

        /// <summary>
        /// The character's Wisdom check bonus.
        /// </summary>
        public Score WisdomCheck
        {
            get
            {
                return scores[ScoreType.WisdomCheck];
            }
        }

        /// <summary>
        /// The character's Charisma check bonus.
        /// </summary>
        public Score CharismaCheck
        {
            get
            {
                return scores[ScoreType.CharismaCheck];
            }
        }

        /// <summary>
        /// The Perception skill bonus.
        /// </summary>
        public Score Perception
        {
            get
            {
                return scores[ScoreType.Perception];
            }
        }

        /// <summary>
        /// Add the <see cref="Modifier"/>s provided by a <see cref="ModifierSource"/>
        /// to the scores for this character.
        /// </summary>
        /// <param name="modifierSource">
        /// The <see cref="ModifierSource"/> where the modifiers originate.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// modifierSource cannot be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// modifierSources contains one or more modifiers for the levels score.
        /// </exception>
        protected void AddModifiers(ModifierSource modifierSource)
        {
            if (modifierSource == null)
            {
                throw new ArgumentNullException("modifierSource");
            }

            foreach (Modifier modifier in modifierSource.Modifiers)
            {
                if (modifier.ModifiedScore.GetType().Equals(typeof(LevelScore))
                    && !modifier.Source.GetType().Equals(typeof(InternalModifierSource)))
                {
                    throw new ArgumentException("Cannot modify 'Level' score");
                }

                modifier.ModifiedScore.AddModifier(modifier);
            }
        }

        /// <summary>
        /// Create the equipped data structures.
        /// </summary>
        protected void CreateEquippedItems()
        {
            if (equippedItems != null)
            {
                throw new InvalidOperationException("Already created");
            }

            equippedItems = new Dictionary<Slot, Item>();
            foreach (int slot in Enum.GetValues(typeof(Slot)))
            {
                equippedItems.Add((Slot)slot, null);
            }

            heldItems = new Dictionary<Hand, Item>();
            heldItems.Add(Hand.Main, null);
            heldItems.Add(Hand.Off, null);
        }

        /// <summary>
        /// Create the scores this character uses.
        /// </summary>
        /// <returns>
        /// The scores used by this character.
        /// </returns>
        protected Dictionary<ScoreType, Score> CreateScores()
        {
            Dictionary<ScoreType, Score> result;
            int[] scoreTypes;

            result = new Dictionary<ScoreType, Score>();

            // Add specialized scores
            result.Add(ScoreType.Strength, new AbilityScore("Strength", "Str", 
                ScoreType.StrengthModifier));
            result.Add(ScoreType.Constitution, new AbilityScore("Constitution", "Con", 
                ScoreType.ConstitutionModifier));
            result.Add(ScoreType.Dexterity, new AbilityScore("Dexterity", "Dex", 
                ScoreType.DexterityModifier));
            result.Add(ScoreType.Intelligence, new AbilityScore("Intelligence", "Int", 
                ScoreType.IntelligenceModifier));
            result.Add(ScoreType.Wisdom, new AbilityScore("Wisdom", "Wis", 
                ScoreType.WisdomModifier));
            result.Add(ScoreType.Charisma, new AbilityScore("Charisma", "Cha", 
                ScoreType.CharismaModifier));
            result.Add(ScoreType.HitPoints, new HitPoints());
            result.Add(ScoreType.Bloodied, new Bloodied());
            result.Add(ScoreType.ArmorClass, new ArmorClass());
            result.Add(ScoreType.Fortitude, new Defense("Fortitude", "Fort",
                new ScoreType[] { ScoreType.Strength, ScoreType.Constitution }));
            result.Add(ScoreType.Reflex, new Defense("Reflex", "Ref",
                new ScoreType[] { ScoreType.Dexterity, ScoreType.Intelligence }));
            result.Add(ScoreType.Will, new Defense("Will", "Will",
                new ScoreType[] { ScoreType.Wisdom, ScoreType.Charisma }));
            result.Add(ScoreType.Acrobatics, new Skill("Acrobatics", ScoreType.Dexterity));
            result.Add(ScoreType.Athletics, new Skill("Athletics", ScoreType.Strength));
            result.Add(ScoreType.Conspiracy, new Skill("Conspiracy", ScoreType.Intelligence));
            result.Add(ScoreType.Insight, new Skill("Insight", ScoreType.Wisdom));
            result.Add(ScoreType.Interaction, new Skill("Interaction", ScoreType.Charisma));
            result.Add(ScoreType.Mechanics, new Skill("Mechanics", ScoreType.Intelligence));
            result.Add(ScoreType.Nature, new Skill("Nature", ScoreType.Wisdom));
            result.Add(ScoreType.Perception, new Skill("Perception", ScoreType.Wisdom));
            result.Add(ScoreType.Science, new Skill("Science", ScoreType.Intelligence));
            result.Add(ScoreType.Stealth, new Skill("Stealth", ScoreType.Dexterity));
            result.Add(ScoreType.Initiative, new Initiative());
            result.Add(ScoreType.Level, new LevelScore());
            result.Add(ScoreType.Speed, new Speed());
            result.Add(ScoreType.StrengthCheck, new AbilityCheck(ScoreType.Strength));
            result.Add(ScoreType.ConstitutionCheck, new AbilityCheck(ScoreType.Constitution));
            result.Add(ScoreType.DexterityCheck, new AbilityCheck(ScoreType.Dexterity));
            result.Add(ScoreType.IntelligenceCheck, new AbilityCheck(ScoreType.Intelligence));
            result.Add(ScoreType.WisdomCheck, new AbilityCheck(ScoreType.Wisdom));
            result.Add(ScoreType.CharismaCheck, new AbilityCheck(ScoreType.Charisma));

            // Add any remaining score types
            scoreTypes = (int[])Enum.GetValues(typeof(ScoreType));
            foreach (int scoreType in scoreTypes)
            {
                if (!result.ContainsKey((ScoreType)scoreType))
                {
                    result.Add((ScoreType)scoreType, new Score(Enum.GetName(typeof(ScoreType), scoreType),
                        Enum.GetName(typeof(ScoreType), scoreType)));
                }
            }

            return result;
        }

        /// <summary>
        /// The <see cref="ModifierSource"/>s from <see cref="Item"/>s.
        /// </summary>
        protected ICollection<ModifierSource> ItemModifierSources
        {
            get
            {
                List<ModifierSource> result;
                List<Item> items;

                // Assemble a list of all equipment
                // TODO: Move this to a property?
                items = new List<Item>();
                foreach (Item item in equippedItems.Values)
                {
                    if (item != null)
                    {
                        items.Add(item);
                    }
                }
                foreach (Item item in heldItems.Values)
                {
                    if (item != null)
                    {
                        items.Add(item);
                    }
                }
                foreach (Item item in equipment)
                {
                    if (item.Slot == Slot.None)
                    {
                        items.Add(item);
                    }
                }

                // Add each item
                result = new List<ModifierSource>();
                foreach (Item item in items)
                {
                    result.Add(item);
                }

                return result;
            }
        }

        /// <summary>
        /// The character's modifier sources. These are used for updating the character's scores
        /// and as a source of feats, powers and the like.
        /// </summary>
        protected internal ICollection<ModifierSource> ModifierSources
        {
            get
            {
                List<ModifierSource> result;

                result = new List<ModifierSource>();

                result.AddRange(ModifierSourcesOnly);
                result.AddRange(SubModifierSources);

                return result.AsReadOnly();
            }
        }

        /// <summary>
        /// <see cref="ModifierSource"/>s without and sub modifier sources.
        /// </summary>
        protected ICollection<ModifierSource> ModifierSourcesOnly
        {
            get
            {
                List<ModifierSource> result;

                result = new List<ModifierSource>();

                result.Add(internalModifierSource);
                foreach (ModifierSource score in scores.Values)
                {
                    result.Add(score);
                }
                result.Add(abilityScores);
                result.Add(PrimaryOrigin);
                result.Add(SecondaryOrigin);
                result.AddRange(Levels);
                result.AddRange(GetUsablePowers());
                result.AddRange(ItemModifierSources);
                result.AddRange(GetTraits());

                return result.AsReadOnly();
            }
        }

        /// <summary>
        /// The <see cref="ModifierSource"/>s from <see cref="ModifierSource"/>s. This includes the attack and damage bonuses
        /// for attack powers, for example.
        /// </summary>
        protected ICollection<ModifierSource> SubModifierSources
        {
            get
            {
                List<ModifierSource> result;

                result = new List<ModifierSource>();
                foreach (ModifierSource modifierSource in ModifierSourcesOnly)
                {
                    foreach (ModifierSource subModifierSource in modifierSource.ModifierSources)
                    {
                        result.Add(subModifierSource);
                    }
                }

                return result.AsReadOnly();
            }
        }

        /// <summary>
        /// Raise the <see cref="ModifierSourceUpdated"/> event.
        /// </summary>
        /// <param name="args">
        /// The <see cref="ModifierSourceUpdateEventArgs"/> to =include in the raised event.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="args"/> cannot be null.
        /// </exception>
        /// <seealso cref="ModifierSourceUpdated"/>
        protected void RaiseModifierSourceUpdatedEvent(ModifierSourceUpdateEventArgs args)
        {
            if (args == null)
            {
                throw new ArgumentNullException("args");
            }

            if (ModifierSourceUpdated != null)
            {
                ModifierSourceUpdated(this, args);
            }
        }

        /// <summary>
        /// Raise the <see cref="ModifierSourceUpdating"/> event.
        /// </summary>
        /// <param name="args">
        /// The <see cref="ModifierSourceUpdateEventArgs"/> to =include in the raised event.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="args"/> cannot be null.
        /// </exception>
        /// <seealso cref="ModifierSourceUpdating"/>
        protected void RaiseModifierSourceUpdatingEvent(ModifierSourceUpdateEventArgs args)
        {
            if (args == null)
            {
                throw new ArgumentNullException("args");
            }

            if (ModifierSourceUpdating != null)
            {
                ModifierSourceUpdating(this, args);
            }
        }

        /// <summary>
        /// Remove any <see cref="Modifier"/> to the scores for this character.
        /// </summary>
        protected void RemoveModifiers()
        {
            foreach (Score score in scores.Values)
            {
                score.ClearModifiers();
            }
            foreach (ModifierSource modifierSource in SubModifierSources)
            {
                if (modifierSource is Score)
                {
                    ((Score)modifierSource).ClearModifiers();
                }
            }
        }

        /// <summary>
        /// Remove powers from <paramref name="powers"/> that match <paramref name="predicate"/>.
        /// </summary>
        /// <param name="powers">
        /// A list of powers to remove a powerfrom.
        /// </param>
        /// <param name="predicate">
        /// The predicate to use to remove a power.
        /// </param>
        /// <param name="isReplacedPowerValid">
        /// An optional function to check whether the replaced power is valid for the source. It should
        /// return true if the replaecd power is valid or false if not.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Only one power can be retrained.
        /// </exception>
        private void RemovePower(List<Power> powers, Predicate<Power> predicate, Func<Power, bool> isReplacedPowerValid)
        {
            if (powers == null)
            {
                throw new ArgumentNullException("powers");
            }
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            List<Power> matchingPowers;
            List<Power> unretrainablePowers;

            // Ensure it matches only one feat
            matchingPowers = powers.FindAll(predicate);
            if (matchingPowers.Count == 0)
            {
                throw new ArgumentException("Cannot retrain a power the character does not have",
                    "predicate");
            }
            else if (matchingPowers.Count > 1)
            {
                throw new ArgumentException("Cannot retrain multiple powers", "predicate");
            }
            else if (!isReplacedPowerValid(matchingPowers[0]))
            {
                throw new ArgumentException(string.Format("Power '{0}' cannot be retrained at this level", matchingPowers[0].Name));
            }

            // Ensure the power provided by a class, race or paragon path is not retrainable.
            // Exclude the Class because it contains powers the character chose at first level.
            unretrainablePowers = new List<Power>();
            if (unretrainablePowers.FindAll(predicate).Count > 0)
            {
                throw new ArgumentException("Cannot retrain a power provided by a class, race or paragon path", "predicate");
            }

            powers.RemoveAll(predicate);
        }
    }
}