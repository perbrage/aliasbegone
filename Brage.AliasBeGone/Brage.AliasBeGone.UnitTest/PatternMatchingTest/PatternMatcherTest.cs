using System;
using System.Linq;
using Brage.AliasBeGone.Infrastructure;
using Brage.AliasBeGone.PatternMatching;
using Brage.AliasBeGone.UnitTest.PatternMatchingTest.Support;
using NUnit.Framework;

namespace Brage.AliasBeGone.UnitTest.PatternMatchingTest
{
    [TestFixture]
    public class PatternMatcherTest
    {
        [Test]
        public void Search_TextToSearchParameterIsNull_ReturnsZeroPatternHits()
        {
            var configuration = new Configuration();
            var patternMatcher = new PatternMatcher(configuration);

            var patternHits = patternMatcher.Search(null);

            Assert.AreEqual(0, patternHits.Count());
        }

        [Test]
        public void Search_TextToSearchParameterIsEmptyString_ReturnsZeroPatternHits()
        {
            var configuration = new Configuration();
            var patternMatcher = new PatternMatcher(configuration);

            var patternHits = patternMatcher.Search(String.Empty);

            Assert.AreEqual(0, patternHits.Count());
        }

        [Test]
        public void Search_UsingIntWithGenericPatternConfigurationAndNotGenericIntsUsed_ReturnsZeroPatternHits()
        {
            var configuration = new IntWithGenericPatternConfiguration();
            var patternMatcher = new PatternMatcher(configuration);

            var patternHits = patternMatcher.Search("int x = 0;");

            Assert.AreEqual(0, patternHits.Count());
        }

        [Test]
        public void Search_UsingIntWithGenericPatternConfigurationAndSearchTextContainsGenericInts_ReturnsOnePatternHits()
        {
            var configuration = new IntWithGenericPatternConfiguration();
            var patternMatcher = new PatternMatcher(configuration);

            var patternHits = patternMatcher.Search("var stats = new List<int>();");

            Assert.AreEqual(1, patternHits.Count());
        }

        [Test]
        public void Search_UsingIntAndStringWithGenericPatternConfigurationAndSearchTextContainsPatches_ReturnsTwoPatternHits()
        {
            var configuration = new IntAndStringWithGenericPatternConfiguration();
            var patternMatcher = new PatternMatcher(configuration);

            var patternHits = patternMatcher.Search("var stats = new List<int>(); var messages = new List<string>();");

            Assert.AreEqual(2, patternHits.Count());
        }
    }
}
