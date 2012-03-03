using System;
using Brage.AliasBeGone.Infrastructure;
using Brage.AliasBeGone.PatternMatching;
using NUnit.Framework;

namespace Brage.AliasBeGone.UnitTest.PatternMatchingTest
{
    [TestFixture]
    public class PatternHitTest
    {
        [Test]
        public void StartPosition_UsingGetter_ReturnsValueSpecifiedAtConstruction()
        {
            const int expectedValue = 10;
            var map = new Map("int", "Int32");
            var patternHit = new PatternHit(expectedValue, map);
            Assert.AreEqual(expectedValue, patternHit.StartPosition);
        }

        [Test]
        public void CharsToReplace_ConstructedWithAMapContainingA3CharacterAlias_ReturnsTheNumber3()
        {
            var map = new Map("int", "Int32");
            var patternHit = new PatternHit(10, map);
            Assert.AreEqual(3, patternHit.CharsToReplace);
        }

        [Test]
        public void ReplaceWith_ConstructedWithAMapContainingTheCLRTypeString_ReturnsValueString()
        {
            var map = new Map("string", "String");
            var patternHit = new PatternHit(10, map);
            Assert.AreEqual("String", patternHit.ReplaceWith);
        }

        [Test]
        public void Constructor_MapParameterIsNull_ThrowArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new PatternHit(10, null));

            Assert.AreEqual("map", exception.ParamName);
        }

    }
}
