using Brage.AliasBeGone.Infrastructure;
using NUnit.Framework;

namespace Brage.AliasBeGone.UnitTest.InfrastructureTest
{
    [TestFixture]
    public class ConfigurationTest
    {
        [Test]
        public void GetMappings_CalledASecondTime_ReturnsSameInstance()
        {
            var configuration = new Configuration();

            var firstCall = configuration.GetMappings();
            var secondCall = configuration.GetMappings();

            Assert.IsTrue(ReferenceEquals(firstCall, secondCall));

        }

        [Test]
        public void GetPatterns_CalledASecondTime_ReturnsSameInstance()
        {
            var configuration = new Configuration();

            var firstCall = configuration.GetPatterns();
            var secondCall = configuration.GetPatterns();

            Assert.IsTrue(ReferenceEquals(firstCall, secondCall));
        }

    }
}
