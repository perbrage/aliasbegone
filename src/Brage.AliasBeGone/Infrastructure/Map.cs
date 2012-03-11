using System;

namespace Brage.AliasBeGone.Infrastructure
{
    /// <summary>
    ///     Specifies a map between alias and clr Type
    /// </summary>
    internal sealed class Map : IMap
    {
        private readonly String _alias;
        private readonly String _clrType;

        public Map(String alias, String clrType)
        {
            if (String.IsNullOrEmpty(alias))
                throw new ArgumentNullException("alias");

            if (String.IsNullOrEmpty(clrType))
                throw new ArgumentNullException("clrType");

            _alias = alias;
            _clrType = clrType;
        }

        /// <summary>
        ///     Contains the alias to convert from
        /// </summary>
        public String Alias
        {
            get { return _alias; }
        }

        /// <summary>
        ///     Contains the alias to convert too
        /// </summary>
        public String ClrType
        {
            get { return _clrType; }
        }
    }
}
