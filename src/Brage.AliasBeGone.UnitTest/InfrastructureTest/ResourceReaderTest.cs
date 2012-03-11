using System;
using Brage.AliasBeGone.Infrastructure;
using NUnit.Framework;

namespace Brage.AliasBeGone.UnitTest.InfrastructureTest
{
    [TestFixture]
    public class ResourceReaderTest
    {
        [Test]
        public void GetResourceContent_ResourceThatExists_ReturnsContent()
        {
            var resourceReader = new ResourceReader("Brage.AliasBeGone.Snippets");

            var resourceContent = resourceReader.GetResourceContent("Boolean.snippet");

            Assert.IsFalse(String.IsNullOrEmpty(resourceContent));
        }

        [Test]
        public void GetResourceContent_ResourceDoesntExist_ReturnsNull()
        {
            var resourceReader = new ResourceReader("Brage.AliasBeGone.Snippets");

            var resourceContent = resourceReader.GetResourceContent("DOESNTEXIST.snippet");

            Assert.AreEqual(null, resourceContent);
        }

        [Test]
        public void GetResourceContent_NamespaceEnteredWithSuffixDot_ReturnsContent()
        {
            var resourceReader = new ResourceReader("Brage.AliasBeGone.Snippets.");

            var resourceContent = resourceReader.GetResourceContent("Boolean.snippet");

            Assert.IsFalse(String.IsNullOrEmpty(resourceContent));
        }

        [Test]
        public void GetResourceContent_ResourceNameEnteredWithPrefixDot_ReturnsContent()
        {
            var resourceReader = new ResourceReader("Brage.AliasBeGone.Snippets");

            var resourceContent = resourceReader.GetResourceContent(".Boolean.snippet");

            Assert.IsFalse(String.IsNullOrEmpty(resourceContent));
        }

    }
}
