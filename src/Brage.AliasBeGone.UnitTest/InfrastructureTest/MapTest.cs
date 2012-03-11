using System;
using Brage.AliasBeGone.Infrastructure;
using NUnit.Framework;

namespace Brage.AliasBeGone.UnitTest.InfrastructureTest
{
    [TestFixture]
    public class MapTest
    {
        [Test]
        public void Constructor_AliasIsNullOrEmpty_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new Map(null, "Int32"));

            Assert.AreEqual("alias", exception.ParamName);
        }

        [Test]
        public void Constructor_ClrTypeIsNullOrEmpty_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new Map("int", null));

            Assert.AreEqual("clrType", exception.ParamName);
        }

        [Test]
        public void Alias_UsingGetter_ReturnsValueSpecifiedAtConstruction()
        {
            const String expectedValue = "int";
            var map = new Map(expectedValue, "Int32");
            Assert.AreEqual(expectedValue, map.Alias);
        }

        [Test]
        public void ClrType_UsingGetter_ReturnsValueSpecifiedAtConstruction()
        {
            const String expectedValue = "Int32";
            var map = new Map("int", expectedValue);
            Assert.AreEqual(expectedValue, map.ClrType);
        }

    }
}
