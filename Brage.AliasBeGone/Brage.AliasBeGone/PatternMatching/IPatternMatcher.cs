using System;
using System.Collections.Generic;

namespace Brage.AliasBeGone.PatternMatching
{
    /// <summary>
    ///     Finds Maps in a given text according to a certain pattern which results in zero or more PatternHits
    /// </summary>
    internal interface IPatternMatcher
    {
        /// <summary>
        ///     Searches for pattern hits according configuration
        /// </summary>
        /// <param name="textToSearch">Text to search in</param>
        /// <returns>Zero or more PatternHits</returns>
        IEnumerable<PatternHit> Search(String textToSearch);
    }
}