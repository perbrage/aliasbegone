using System;

namespace Brage.AliasBeGone.Infrastructure
{
    internal interface ISnippetsManager
    {
        void Install();
        void Uninstall();
        Boolean IsInstalled { get; }
    }
}