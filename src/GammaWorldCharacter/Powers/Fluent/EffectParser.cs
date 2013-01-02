using GammaWorldCharacter.Powers.Fluent.EffectComponents;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers.Fluent
{
    /// <summary>
    /// Take an <see cref="EffectExpression"/> and into <see cref="EffectSpan"/>s. 
    /// These mostly contain next but also high light power names or similar 
    /// aspects that may need to be formatted differently.
    /// </summary>
    /// <remarks>
    /// For example, the DoppelGaner critical status "Deal 1d10 extra damage 
    /// and you can use double trouble as a free action" (GW38). In the a 
    /// character sheet, "double trouble" should be italicized because it is
    /// a power name.
    /// <para />
    /// TODO: Show modifiers
    /// </remarks>
    public class EffectParser
    {
        /// <summary>
        /// 
        /// </summary>
        protected internal Dictionary<Type, Func<EffectComponent, IEnumerable<EffectSpan>>> effectToSpan;

        /// <summary>
        /// Create a new <see cref="EffectParser"/>.
        /// </summary>
        public EffectParser()
        {
            effectToSpan = new Dictionary<Type, Func<EffectComponent, IEnumerable<EffectSpan>>>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="character"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression"/> cannot be null.
        /// </exception>
        public IEnumerable<EffectSpan> Parse(Character character, EffectExpression expression)
        {
            if (character == null)
            {
                throw new ArgumentNullException("character");
            }
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            List<EffectSpan> result;

            result = new List<EffectSpan>();
            foreach (EffectComponent component in expression.Components)
            {
                ParseEffectComponent(character, component, result.Add);
            }

            // TODO: Add conjunctions like And
            // TODO: Capitalize first level, add spaces and add a full stop (a.k.a. period) at the end.
            // TODO: Merge adjacent text spans (type EffectSpanType.None)

            return result;
        }

        /// <summary>
        /// Parse an <see cref="EffectComponent"/>.
        /// </summary>
        /// <param name="character"></param>
        /// <param name="component"></param>
        /// <param name="addSpan"></param>
        /// <exception cref="ArgumentNullException">
        /// Neither <paramref name="character"/> nor <paramref name="component"/> can be null.
        /// </exception>
        private void ParseEffectComponent(Character character, EffectComponent component, Action<EffectSpan> addSpan)
        {
            if (character == null)
            {
                throw new ArgumentNullException("character");
            }
            if (component == null)
            {
                throw new ArgumentNullException("component");
            }
            if (addSpan == null)
            {
                throw new ArgumentNullException("addSpan");
            }

            ParseTarget(component.Target, addSpan);
            ParseComponent(character, component, addSpan);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="character"></param>
        /// <param name="component"></param>
        /// <param name="addSpan"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// Neither <paramref name="character"/> nor <paramref name="component"/> can be null.
        /// </exception>
        private void ParseComponent(Character character, EffectComponent component, Action<EffectSpan> addSpan)
        {
            if (character == null)
            {
                throw new ArgumentNullException("character");
            }
            if (component == null)
            {
                throw new ArgumentNullException("component");
            }
            if (addSpan == null)
            {
                throw new ArgumentNullException("addSpan");
            }

            // TODO: Move these into separate classes or functions

            if (component is DiceDamageEffect)
            {
                addSpan(new EffectSpan(string.Format("the attack deals {0} damage",
                    ((DiceDamageEffect) component).Dice)));
            }
            else if (component is PushEffect)
            {
                addSpan(new EffectSpan(string.Format("you push the target {0} squares",
                    ((PushEffect) component).Squares)));
            }
            else if (component is TemporaryHitPointsEffect)
            {
                addSpan(new EffectSpan(string.Format("regains {0} hit points",
                    ((TemporaryHitPointsEffect) component).TemporaryHitPoints
                        .GetValue(character))));
            }
            else
            {
                throw new ArgumentException(string.Format("Unknown effect component type '{0}'",
                    component.GetType()), "component");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="addSpan"></param>
        /// <returns></returns>
        private void ParseTarget(Target target, Action<EffectSpan> addSpan)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }
            if (addSpan == null)
            {
                throw new ArgumentNullException("addSpan");
            }

            switch (target.TargetType)
            {
                case TargetType.Ally:
                    addSpan(new EffectSpan(string.Format(
                        "one ally within {0} squares of {1}",
                        target.Where.Squares, target.Where.Of == Of.Target ? "the target" : "you")));
                    break;
                case TargetType.Creature:
                    addSpan(new EffectSpan("one creature"));
                    break;
                case TargetType.Enemy:
                    addSpan(new EffectSpan("one enemy"));
                    break;
                case TargetType.SameTarget:
                    // Do nothing
                    break;
                case TargetType.You:
                    addSpan(new EffectSpan("you"));
                    break;
                case TargetType.YouOrAlly:
                    addSpan(new EffectSpan(string.Format(
                        "you or one ally within {0} squares of {1}",
                        target.Where.Squares, target.Where.Of == Of.Target ? "the target" : "you")));
                    break;
                default:
                    throw new ArgumentException(
                        string.Format("Unknown or missing target '{0}'", target.TargetType), "target");
            }
        }
    }

    /// <summary>
    /// Part of a textual effect description.
    /// </summary>
    public class EffectSpan
    {
        /// <summary>
        /// Create a new <see cref="EffectSpan"/>.
        /// </summary>
        /// <param name="text">
        /// The text to display. This cannot be null, empty or whitespace.
        /// </param>
        /// <param name="effectSpanType">
        /// The <see cref="EffectSpanType"/> of the text shown. By default, 
        /// it is EffectSpanType.None.
        /// </param>
        public EffectSpan(string text, EffectSpanType effectSpanType = EffectSpanType.None)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentNullException("text");
            }

            this.Text = text;
            this.Type = effectSpanType;
        }

        /// <summary>
        /// The span's description or text.
        /// </summary>
        public string Text
        {
            get;
            private set;
        }

        /// <summary>
        /// What the text describes.
        /// </summary>
        public EffectSpanType Type
        {
            get;
            private set;
        }
    }

    /// <summary>
    /// The different types of <see cref="EffectSpan"/>s.
    /// </summary>
    public enum EffectSpanType
    {
        /// <summary>
        /// No special meaning. Just text.
        /// </summary>
        None,
        /// <summary>
        /// Power name.
        /// </summary>
        Power
    }
}
