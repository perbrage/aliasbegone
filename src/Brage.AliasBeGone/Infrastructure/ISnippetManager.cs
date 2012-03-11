using System;

namespace Brage.AliasBeGone.Infrastructure
{
    internal interface ISnippetManager
    {
        void InstallSnippets();
        void UninstallSnippets();
        Boolean AreSnippetsInstalled();
    }
}