using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brage.AliasBeGone.Infrastructure;

namespace Brage.AliasBeGone.UnitTest.PatternMatchingTest.Support
{
    internal sealed class IntWithGenericPatternConfiguration : IConfiguration
    {
        public IEnumerable<IMap> GetMappings()
        {
            return new List<IMap>
                       {
                           new Map("int", "Int32")
                       };
        }

        public IEnumerable<String> GetPatterns()
        {
            return new List<String>
                       {
                            "<{0}>"
                       };
        }
    }
}
