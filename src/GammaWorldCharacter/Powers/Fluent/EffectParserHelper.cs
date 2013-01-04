using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GammaWorldCharacter.Powers.Fluent
{
    /// <summary>
    /// Helper methods for <see cref="EffectParser"/>.
    /// </summary>
    public static class EffectParserHelper
    {
        /// <summary>
        /// Add conjunctions, such as commas "," and "ands" to 
        /// concatenate the given phrases into a sentence.
        /// </summary>
        /// <param name="effectSpans">
        /// The lists of lists of <see cref="EffectSpan"/> to
        /// add conjunctions to. This cannot be null and no element
        /// within hte list can be null.
        /// </param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="effectSpans"/> cannot be null and no 
        /// element within it can be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="effectSpans"/> cannot be empty.
        /// </exception>
        public static IList<EffectSpan> AddConjunctions(this IList<List<EffectSpan>> effectSpans)
        {
            if (effectSpans == null)
            {
                throw new ArgumentNullException("effectSpans");
            }
            if (!effectSpans.Any())
            {
                throw new ArgumentException("Cannot be empty", "effectSpans");
            }
            if (effectSpans.Any(x => x == null))
            {
                throw new ArgumentNullException("effectSpans");
            }

            List<EffectSpan> result;

            result = new List<EffectSpan>();
            for (int i = 0; i < effectSpans.Count(); i++)
            {
                result.AddRange(effectSpans[i]);

                if (effectSpans.Any())
                {
                    if (i < effectSpans.Count() - 2)
                    {
                        result.Add(new EffectSpan(" and "));
                    }
                    else
                    {
                        result.Add(new EffectSpan(", "));
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Add a trailing period ("full stop" for those outside the US) or do nothing for
        /// an empty list.
        /// </summary>
        /// <param name="effectSpans">
        /// The <see cref="IEnumerable{EffectSpan}"/> to add a new <see cref="EffectSpan"/>
        /// to containing a period.  This cannot be null, empty or contain
        /// null.
        /// </param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="effectSpans"/> cannot be null.
        /// </exception>
        public static IEnumerable<EffectSpan> AddPeriod(this IEnumerable<EffectSpan> effectSpans)
        {
            if (effectSpans == null)
            {
                throw new ArgumentNullException("effectSpans");
            }

            return  effectSpans.Concat(new[] { new EffectSpan(".") });
        }

        /// <summary>
        /// Capitalize the first letter of the first <see cref="EffectSpan"/>.
        /// </summary>
        /// <param name="effectSpans">
        /// The <see cref="IEnumerable{EffectSpan}"/> to add make the first
        /// letter of a captial letter. This cannot be null, empty or contain
        /// null.
        /// </param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="effectSpans"/> cannot be null and no element can be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="effectSpans"/> cannot be empty.
        /// </exception>
        public static IEnumerable<EffectSpan> CapitalizeFirstLetter(this IEnumerable<EffectSpan> effectSpans)
        {
            if (effectSpans == null)
            {
                throw new ArgumentNullException("effectSpans");
            }
            if (!effectSpans.Any())
            {
                throw new ArgumentException("Cannot be empty", "effectSpans");
            }
            if (effectSpans.Any(x => x == null))
            {
                throw new ArgumentNullException("effectSpans");
            }

            string text;

            text = effectSpans.First().Text.TrimStart();
            if (text.Length > 0)
            {
                text = text.Substring(0, 1).ToUpper() + text.Substring(1);
            }

            return new []{ new EffectSpan(text) }.Concat(effectSpans.Skip(1));
        }

        /// <summary>
        /// Merge adjacent <see cref="EffectSpan"/>s of <see cref="EffectSpanType.None"/>.
        /// Not required, this is an optimiation that makes handling
        /// such a list easier.
        /// </summary>
        /// <param name="effectSpans">
        /// The <see cref="IEnumerable{EffectSpan}"/> to coalesce. This 
        /// cannot be null, empty or contain null.
        /// </param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="effectSpans"/> cannot be null and no element can be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="effectSpans"/> cannot be empty.
        /// </exception>
        public static IEnumerable<EffectSpan> MergeAdjacentTextSpans(this IEnumerable<EffectSpan> effectSpans)
        {
            if (effectSpans == null)
            {
                throw new ArgumentNullException("effectSpans");
            }
            if (!effectSpans.Any())
            {
                throw new ArgumentException("Cannot be empty", "effectSpans");
            }
            if (effectSpans.Any(x => x == null))
            {
                throw new ArgumentNullException("effectSpans");
            }

            StringBuilder stringBuilder;
            List<EffectSpan> result;

            stringBuilder = null;
            result = new List<EffectSpan>();
            foreach (EffectSpan effectSpan in effectSpans)
            {
                if (effectSpan.Type == EffectSpanType.None)
                {
                    if (stringBuilder == null)
                    {
                        stringBuilder = new StringBuilder();
                    }
                    stringBuilder.Append(effectSpan.Text);
                }
                else
                {
                    if (stringBuilder != null)
                    {
                        result.Add(new EffectSpan(stringBuilder.ToString()));
                        stringBuilder = null;
                    }

                    result.Add(effectSpan);
                }
            }

            // Add any trailing effect spans
            if (stringBuilder != null)
            {
                result.Add(new EffectSpan(stringBuilder.ToString()));
            }

            return result;
        }
    }
}
