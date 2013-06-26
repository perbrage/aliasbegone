using System;
using System.Collections.Generic;
using System.Linq;
using Brage.AliasBeGone.Infrastructure;

namespace Brage.AliasBeGone.PatternMatching
{
    /// <summary>
    ///     Finds Maps in a given text according to a certain pattern which results in zero or more PatternHits
    /// </summary>
    internal sealed class PatternMatcher : IPatternMatcher
    {
        private readonly IConfiguration _configuration;

        public PatternMatcher(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        ///     Searches for pattern hits according configuration
        /// </summary>
        /// <param name="textToSearch">Text to search in</param>
        /// <returns>Zero or more PatternHits</returns>
        public IEnumerable<PatternHit> Search(String textToSearch)
        {
            if (String.IsNullOrEmpty(textToSearch))
                return new List<PatternHit>();

			var indexMap = CommentedCodeIndexMap.CreateMap(textToSearch);

            return _configuration.GetMappings()
                                 .SelectMany(map => _configuration.GetPatterns()
                                 .SelectMany(pattern => Search(textToSearch, map, pattern, indexMap)));
        }

		private IEnumerable<PatternHit> Search(String textToSearch, IMap map, String pattern, CommentedCodeIndexMap indexMap)
        {
            var mapPattern = String.Format(pattern, map.Alias);
            var i = 0;

			while ((i = textToSearch.IndexOf(mapPattern, i)) != -1)
			{
				++i;
				if(!indexMap.IsIndexCommentedCode(i - 1))
					yield return new PatternHit(i, map);
			}
        }
    }
}
