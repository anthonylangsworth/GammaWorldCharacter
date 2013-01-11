using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GammaWorldCharacter.Powers.Effects;

namespace GammaWorldCharacter.Test.Unit.Powers.Effects
{
    [TestFixture]
    public class TestEffectParserHelper
    {
        [Test]
        public void TestAddConjunctions_NullList()
        {
            Assert.That(() => EffectParserHelper.AddConjunctions(null),
                        Throws.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("effectSpans"));
        }

        [Test]
        public void TestAddConjunctions_EmptyList()
        {
            Assert.That(() => new List<List<EffectSpan>>(new List<EffectSpan>[] {  }).AddConjunctions(),
                        Throws.TypeOf<ArgumentException>().And.Property("ParamName").EqualTo("effectSpans"));
        }

        [Test]
        public void TestAddConjunctions_ListContainingNull()
        {
            Assert.That(() => new List<List<EffectSpan>>(new List<EffectSpan>[] { null }).AddConjunctions(),
                        Throws.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("effectSpans"));
        }

        [TestCaseSource("AddConjuctionsTestSource")]
        public void TestAddConjunctions(List<List<EffectSpan>> effectSpans, string expectedResult)
        {
            IEnumerable<EffectSpan> result = effectSpans.AddConjunctions();
            Assert.That(EffectSpansToString(result), Is.EqualTo(expectedResult));
        }

        public static object[] AddConjuctionsTestSource()
        {
            List<EffectSpan> oneElementAbc = new List<EffectSpan>(new EffectSpan[] { new EffectSpan("abc") });
            List<EffectSpan> oneElementDef = new List<EffectSpan>(new EffectSpan[] { new EffectSpan("def") });
            List<EffectSpan> oneElementGhi = new List<EffectSpan>(new EffectSpan[] { new EffectSpan("ghi") });
            List<EffectSpan> twoElements = new List<EffectSpan>(new EffectSpan[] { new EffectSpan("123"), new EffectSpan("456") });

            return new object[]
            {
                new object[] {new List<List<EffectSpan>>(new[] {oneElementAbc}), "abc"},
                new object[] {new List<List<EffectSpan>>(new[] {oneElementAbc, oneElementDef}), "abc and def"},
                new object[] {new List<List<EffectSpan>>(new[] {oneElementAbc, oneElementDef, twoElements}), "abc, def and 123456"},
                new object[] {new List<List<EffectSpan>>(new[] {oneElementAbc, oneElementDef, twoElements, oneElementGhi}), "abc, def, 123456 and ghi"},
            };
        }


        [Test]
        public void TestAddPeriod_NullList()
        {
            Assert.That(() => EffectParserHelper.AddTrailingPeriod(null),
                        Throws.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("effectSpans"));           
        }

        [Test]
        public void TestAddPeriod()
        {
            IEnumerable<EffectSpan> result = EffectParserHelper.AddTrailingPeriod(new [] {new EffectSpan("a")});
            Assert.That(result, Is.EquivalentTo(new EffectSpan[] {new EffectSpan("a"), new EffectSpan(".")}));
        }

        [Test]
        public void TestCapitalizeFirstLetter_NullList()
        {
            Assert.That(() => EffectParserHelper.CapitalizeFirstLetter(null),
                        Throws.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("effectSpans"));
        }

        [Test]
        public void TestCapitalizeFirstLetter_EmptyList()
        {
            Assert.That(() => new EffectSpan[] { }.CapitalizeFirstLetter(),
                        Throws.TypeOf<ArgumentException>().And.Property("ParamName").EqualTo("effectSpans"));
        }

        [Test]
        public void TestCapitalizeFirstLetter_ListContainingNull()
        {
            Assert.That(() => new EffectSpan[] { null }.CapitalizeFirstLetter(),
                        Throws.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("effectSpans"));
        }

        [TestCaseSource("CapitalizeFirstLetterTestSource")]
        public void TestCapitalizeFirstLetter(IEnumerable<EffectSpan> effectSpans, string expectedResult)
        {
            IEnumerable<EffectSpan> result = effectSpans.CapitalizeFirstLetter();
            Assert.That(EffectSpansToString(result), Is.EqualTo(expectedResult));
        }

        public static object[] CapitalizeFirstLetterTestSource()
        {
            return new object[]
            {
                new object[] {new [] { new EffectSpan("abc") }, "Abc" },
                new object[] {new [] { new EffectSpan(" abc") }, "Abc" },
                new object[] {new [] { new EffectSpan("abc"), new EffectSpan("def") }, "Abcdef" },
                new object[] {new [] { new EffectSpan("123") }, "123" }
            };
        }

        [Test]
        public void TestMergeAdjacentTextSpans_NullList()
        {
            Assert.That(() => EffectParserHelper.MergeAdjacentTextSpans(null),
                        Throws.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("effectSpans"));
        }

        [Test]
        public void TestMergeAdjacentTextSpans_EmptyList()
        {
            Assert.That(() => new EffectSpan[] { }.MergeAdjacentTextSpans(),
                        Throws.TypeOf<ArgumentException>().And.Property("ParamName").EqualTo("effectSpans"));
        }

        [Test]
        public void TestMergeAdjacentTextSpans_ListContainingNull()
        {
            Assert.That(() => new EffectSpan[] { null }.MergeAdjacentTextSpans(),
                        Throws.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("effectSpans"));
        }

        [TestCaseSource("MergeAdjacentTextSpansTestSource")]
        public void TestMergeAdjacentTextSpans(IEnumerable<EffectSpan> effectSpans, IEnumerable<EffectSpan> expectedResult)
        {
            IEnumerable<EffectSpan> result = effectSpans.MergeAdjacentTextSpans();
            Assert.That(result, Is.EquivalentTo(expectedResult));
        }

        public static object[] MergeAdjacentTextSpansTestSource()
        {
            return new object[]
            {
                new object[]
                {
                    new [] { new EffectSpan("abc") }, 
                    new [] { new EffectSpan("abc") }
                },
                new object[]
                {
                    new [] { new EffectSpan("abc"), new EffectSpan("def") }, 
                    new [] { new EffectSpan("abcdef") }
                },
                new object[]
                {
                    new [] { new EffectSpan("abc"), new EffectSpan("def"), new EffectSpan("ghi") }, 
                    new [] { new EffectSpan("abcdefghi") }
                },
                new object[]
                {
                    new [] { new EffectSpan("abc"), new EffectSpan("def", EffectSpanType.Power), new EffectSpan("ghi") }, 
                    new [] { new EffectSpan("abc"), new EffectSpan("def", EffectSpanType.Power), new EffectSpan("ghi") } 
                },
                new object[]
                {
                    new [] { new EffectSpan("abc"), new EffectSpan("def"), new EffectSpan("ghi", EffectSpanType.Power) }, 
                    new [] { new EffectSpan("abcdef"), new EffectSpan("ghi", EffectSpanType.Power) } 
                },
                new object[]
                {
                    new [] { new EffectSpan("abc", EffectSpanType.Power), new EffectSpan("def"), new EffectSpan("ghi") }, 
                    new [] { new EffectSpan("abc", EffectSpanType.Power), new EffectSpan("defghi") } 
                }
            };
        }

        /// <summary>
        /// Convert a <see cref="IEnumerable{EffectSpan}"/> to a string.
        /// </summary>
        /// <param name="effectSpans"></param>
        /// <returns></returns>
        private string EffectSpansToString(IEnumerable<EffectSpan> effectSpans)
        {
            if (effectSpans == null)
            {
                throw new ArgumentNullException("effectSpans");
            }

            StringBuilder stringBuilder = new StringBuilder();
            foreach (EffectSpan effectSpan in effectSpans)
            {
                stringBuilder.Append(effectSpan.Text);
            }

            return stringBuilder.ToString();
        }
    }
}
