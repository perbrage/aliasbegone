using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brage.AliasBeGone.Infrastructure;

namespace Brage.AliasBeGone.UnitTest.InfrastructureTest.Support
{
    internal class SnippetsManagerTestHarness : SnippetsManager
    {
        public SnippetsManagerTestHarness(IConfiguration configuration, IResourceReader resourceReader)
            : base(configuration, resourceReader)
        {
        }

        protected override string GetVisualStudioPath()
        {
            return "C:\\Users\\Brage\\Documents\\Visual Studio 2010\\";
        }

        protected override bool IsSnippetInstalled(String path)
        {
            if (path == "C:\\Users\\Brage\\Documents\\Visual Studio 2010\\Code Snippets\\Visual C#\\My Code Snippets\\Boolean.snippet" ||
                path == "C:\\Users\\Brage\\Documents\\Visual Studio 2010\\Code Snippets\\Visual C#\\My Code Snippets\\String.snippet")
                return true;
            
            return false;
        }

        protected override void CreateSnippet(String path, String snippetContent)
        {
            CreateSnippetCalled = true;
        }

        protected override void DeleteSnippet(String path)
        {
            DeleteSnippetCalled = true;
        }

        public Boolean CreateSnippetCalled { get; private set; }
        public Boolean DeleteSnippetCalled { get; private set; }
    }
}
