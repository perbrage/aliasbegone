using System;
using System.Collections.Generic;

namespace Brage.AliasBeGone.Infrastructure
{
    /// <summary>
    ///     Configuration for all alias to search and replace as well as all the different types of patterns
    /// </summary>
    internal class Configuration : IConfiguration
    {
        private IEnumerable<IMap> _mappings;
        private IEnumerable<String> _patterns;
        private IEnumerable<String> _snippets;

        /// <summary>
        ///     Returns alias to CLR mappings that will be searched for
        /// </summary>
        /// <returns>Alias to CLR mappings</returns>
        public IEnumerable<IMap> GetMappings()
        {
            return _mappings ?? (_mappings = new List<IMap>
                                   {
                                        new Map("long", "Int64"),
                                        new Map("int", "Int32"),
                                        new Map("short", "Int16"),
                                        new Map("ulong", "UInt64"),
                                        new Map("uint", "UInt32"),
                                        new Map("ushort", "UInt16"),
                                        new Map("double", "Double"),
                                        new Map("float", "Single"),
                                        new Map("decimal", "Decimal"),
                                        new Map("byte", "Byte"),
                                        new Map("sbyte", "SByte"),
                                        new Map("object", "Object"),
                                        new Map("delegate", "Delegate"),
                                        new Map("string", "String"),
                                        new Map("char", "Char"),
                                        new Map("bool", "Boolean"),
                                   });
        }

        /// <summary>
        ///     Returns all search patterns
        /// </summary>
        /// <returns>Search patterns</returns>
        public IEnumerable<String> GetPatterns()
        {
            return _patterns ?? (_patterns = new List<String>
                                   {
                                       " {0} ", // int x = 0;
                                       "({0})", // var d = (int)x;
                                       " {0}[", // int[] i;
                                       "<{0}>", // var test = new List<int>();
                                       "<{0}[", // var test = new List<int[]>();
                                       "<{0}?", // var test = new List<int?>();
                                       " {0}?", // int? x = 0;
                                       " {0}(", // var x = new int();
                                       " {0})", // if (y is string)
                                       " {0}.", // var i = int.Parse(x);
                                       "({0}." // if (string.IsNullOrEmpty)
                                   });
        }

        /// <summary>
        ///     Returns all snippets to install/uninstall
        /// </summary>
        /// <returns>Snippet names</returns>
        public IEnumerable<String> GetSnippets()
        {
            return _snippets ?? (_snippets = new List<String>
                                    {
                                        "Boolean.snippet",
                                        "Object.snippet",
                                        "String.snippet",
                                        "Delegate.snippet",
                                        "Char.snippet",
                                        "Decimal.snippet",
                                        "Single.snippet",
                                        "Double.snippet",
                                        "Int16.snippet",
                                        "Int32.snippet",
                                        "Int64.snippet",
                                        "UInt16.snippet",
                                        "UInt32.snippet",
                                        "UInt64.snippet",
                                        "Byte.snippet",
                                        "SByte.snippet",
                                    })
            ;
        }

    }
}
