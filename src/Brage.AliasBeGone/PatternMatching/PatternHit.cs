using System;
using Brage.AliasBeGone.Infrastructure;

namespace Brage.AliasBeGone.PatternMatching
{
    /// <summary>
    ///     Specifies a search hit where something is supposed to be replaced
    /// </summary>
    internal sealed class PatternHit
    {
        private readonly Int32 _startPosition;
        private readonly Int32 _charsToReplace;
        private readonly String _replaceWith;

        public PatternHit(Int32 startPosition, IMap map)
        {
            if (map == null)
                throw new ArgumentNullException("map");
            
            _startPosition = startPosition;
            _charsToReplace = map.Alias.Length;
            _replaceWith = map.ClrType;
        }

        /// <summary>
        ///     Start position of text to be replaced
        /// </summary>
        public Int32 StartPosition
        {
            get { return _startPosition; }
        }

        /// <summary>
        ///     Number of characters to replace from StartPosition
        /// </summary>
        public Int32 CharsToReplace
        {
            get { return _charsToReplace; }
        }

        /// <summary>
        ///     The text that should be pasted inbetween instead of current text
        /// </summary>
        public String ReplaceWith
        {
            get { return _replaceWith; }
        }
    }
}
