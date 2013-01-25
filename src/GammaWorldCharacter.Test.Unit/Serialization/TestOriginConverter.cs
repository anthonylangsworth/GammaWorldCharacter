using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GammaWorldCharacter.Origins;
using GammaWorldCharacter.Serialization;
using GammaWorldCharacter.Test.Unit.Origins;
using NUnit.Framework;

namespace GammaWorldCharacter.Test.Unit.Serialization
{
    [TestFixture]
    public class TestOriginConverter: TestConverter<OriginConverter, Origin>
    {
        public override IEnumerable<TestCaseData> TestSerializationSource()
        {
            yield return new TestCaseData(new Android()).Returns(QuoteString(OriginConverter.AndroidOriginName));
            yield return new TestCaseData(new Cockroach()).Returns(QuoteString(OriginConverter.CockroachOriginName));
            yield return new TestCaseData(new Doppelganger()).Returns(QuoteString(OriginConverter.DoppelgangerOriginName));
            yield return new TestCaseData(new Electrokinetic()).Returns(QuoteString(OriginConverter.ElectrokineticOriginName));
            yield return new TestCaseData(new Empath()).Returns(QuoteString(OriginConverter.EmpathOriginName));
            yield return new TestCaseData(new Felinoid()).Returns(QuoteString(OriginConverter.FelinoidOriginName));
            yield return new TestCaseData(new Giant()).Returns(QuoteString(OriginConverter.GiantOriginName));
            yield return new TestCaseData(new GravityController()).Returns(QuoteString(OriginConverter.GravityControllerOriginName));
            yield return new TestCaseData(new Hawkoid()).Returns(QuoteString(OriginConverter.HawkoidOriginName));
            yield return new TestCaseData(new Hypercognitive()).Returns(QuoteString(OriginConverter.HypercognitiveOriginName));
            yield return new TestCaseData(new NullOrigin()).Throws(typeof(InvalidSerializationException));
            yield return new TestCaseData(null).Returns("null");
        }

        public override IEnumerable<TestCaseData> TestDeserializationSource()
        {
            yield return new TestCaseData(QuoteString(OriginConverter.AndroidOriginName)).Returns(new Android());
            yield return new TestCaseData(QuoteString(OriginConverter.CockroachOriginName)).Returns(new Cockroach());
            yield return new TestCaseData(QuoteString(OriginConverter.DoppelgangerOriginName)).Returns(new Doppelganger());
            yield return new TestCaseData(QuoteString(OriginConverter.ElectrokineticOriginName)).Returns(new Electrokinetic());
            yield return new TestCaseData(QuoteString(OriginConverter.EmpathOriginName)).Returns(new Empath());
            yield return new TestCaseData(QuoteString(OriginConverter.FelinoidOriginName)).Returns(new Felinoid());
            yield return new TestCaseData(QuoteString(OriginConverter.GiantOriginName)).Returns(new Giant());
            yield return new TestCaseData(QuoteString(OriginConverter.GravityControllerOriginName)).Returns(new GravityController());
            yield return new TestCaseData(QuoteString(OriginConverter.HawkoidOriginName)).Returns(new Hawkoid());
            yield return new TestCaseData(QuoteString(OriginConverter.HypercognitiveOriginName)).Returns(new Hypercognitive());
            yield return new TestCaseData("null").Returns(null);
            yield return new TestCaseData("\"foo\"").Throws(typeof(InvalidSerializationException));
            yield return new TestCaseData("{'foo': 'bar'}").Throws(typeof(InvalidSerializationException));
        }

        private static string QuoteString(string str)
        {
            return "\"" + str + "\"";
        }
    }
}
