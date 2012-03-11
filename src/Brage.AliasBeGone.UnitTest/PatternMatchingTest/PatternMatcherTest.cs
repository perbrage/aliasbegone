using System;
using System.Collections.Generic;
using System.Linq;
using Brage.AliasBeGone.Infrastructure;
using Brage.AliasBeGone.PatternMatching;
using FakeItEasy;
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
            //Arrange
            var configuration = A.Fake<IConfiguration>();
            A.CallTo(() => configuration.GetPatterns()).Returns(new List<String>
                                                                    {
                                                                        "<{0}>"
                                                                    });
            A.CallTo(() => configuration.GetMappings()).Returns(new List<Map>
                                                                    {
                                                                        new Map("int", "Int32")
                                                                    });
            var patternMatcher = new PatternMatcher(configuration);

            //Act
            var patternHits = patternMatcher.Search("int x = 0;");

            //Assert
            Assert.AreEqual(0, patternHits.Count());
        }

        [Test]
        public void Search_UsingIntWithGenericPatternConfigurationAndSearchTextContainsGenericInts_ReturnsOnePatternHits()
        {
            //Arrange
            var configuration = A.Fake<IConfiguration>();
            A.CallTo(() => configuration.GetPatterns()).Returns(new List<String>
                                                                    {
                                                                        "<{0}>"
                                                                    });
            A.CallTo(() => configuration.GetMappings()).Returns(new List<Map>
                                                                    {
                                                                        new Map("int", "Int32")
                                                                    });
            var patternMatcher = new PatternMatcher(configuration);

            //Act
            var patternHits = patternMatcher.Search("var stats = new List<int>();");

            //Assert
            Assert.AreEqual(1, patternHits.Count());
        }

        [Test]
        public void Search_UsingIntAndStringWithGenericPatternConfigurationAndSearchTextContainsPatches_ReturnsTwoPatternHits()
        {
            //Arrange
            var configuration = A.Fake<IConfiguration>();
            A.CallTo(() => configuration.GetPatterns()).Returns(new List<String>
                                                                    {
                                                                        "<{0}>"
                                                                    });
            A.CallTo(() => configuration.GetMappings()).Returns(new List<Map>
                                                                    {
                                                                        new Map("int", "Int32"),
                                                                        new Map("string", "String")
                                                                    });
            var patternMatcher = new PatternMatcher(configuration);

            //Act
            var patternHits = patternMatcher.Search("var stats = new List<int>(); var messages = new List<string>();");

            //Assert
            Assert.AreEqual(2, patternHits.Count());
        }
    }
}
