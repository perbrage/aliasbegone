using System;
using System.Collections.Generic;

namespace Brage.AliasBeGone.Infrastructure
{
    public interface IConfiguration
    {
        /// <summary>
        ///     Returns alias to CLR mappings that will be searched for
        /// </summary>
        /// <returns>Alias to CLR mappings</returns>
        IEnumerable<IMap> GetMappings();

        /// <summary>
        ///     Returns all search patterns
        /// </summary>
        /// <returns>Search patterns</returns>
        IEnumerable<String> GetPatterns();

        /// <summary>
        ///     Returns all snippets to install/uninstall
        /// </summary>
        /// <returns>Snippet names</returns>
        IEnumerable<String> GetSnippets();
    }
}