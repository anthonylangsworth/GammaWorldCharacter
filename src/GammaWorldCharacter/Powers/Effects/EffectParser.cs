﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Powers.Effects.EffectComponents;

namespace GammaWorldCharacter.Powers.Effects
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

            List<List<EffectSpan>> effectSpans;
            List<EffectSpan> phrase;
            bool firstComponent;

            firstComponent = true;
            effectSpans = new List<List<EffectSpan>>();
            foreach (EffectComponent component in expression.Components)
            {
                phrase = new List<EffectSpan>();
                ParseEffectComponent(character, component, firstComponent, x => 
                {
                    if (phrase.Any())
                    {
                        phrase.AddRange(new EffectSpan[] {new EffectSpan(" "), x});
                    }
                    else
                    {
                        phrase.Add(x);
                    }
                } );
                effectSpans.Add(phrase);
                firstComponent = false;
            }

            // Clean up before returning.
            return effectSpans.AddConjunctions().AddTrailingPeriod().CapitalizeFirstLetter().MergeAdjacentTextSpans();
        }

        /// <summary>
        /// Parse an <see cref="EffectComponent"/>.
        /// </summary>
        /// <param name="character"></param>
        /// <param name="component"></param>
        /// <param name="firstComponent"></param>
        /// <param name="addSpan"></param>
        /// <exception cref="ArgumentNullException">
        /// Neither <paramref name="character"/> nor <paramref name="component"/> can be null.
        /// </exception>
        private void ParseEffectComponent(Character character, EffectComponent component, bool firstComponent, Action<EffectSpan> addSpan)
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

            ParseTarget(component.Target, firstComponent, addSpan);
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

            foreach (EffectSpan effectSpan in component.Parse(character))
            {
                addSpan(effectSpan);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="firstTarget"></param>
        /// <param name="addSpan"></param>
        /// <returns></returns>
        private void ParseTarget(Target target, bool firstTarget, Action<EffectSpan> addSpan)
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
                    if (target.Where == Where.Unspecified)
                    {
                        addSpan(new EffectSpan("one creature"));
                    }
                    else
                    {
                        addSpan(new EffectSpan(string.Format("one creature within {0} squares of {1}", 
                            target.Where.Squares, OfHelper.ToString(target.Where.Of))));
                    }
                    break;
                case TargetType.Enemy:
                    addSpan(new EffectSpan("one enemy"));
                    break;
                case TargetType.TheTarget:
                    if (firstTarget)
                    {
                        addSpan(new EffectSpan("the target"));
                    }
                    // else do nothing
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
}
