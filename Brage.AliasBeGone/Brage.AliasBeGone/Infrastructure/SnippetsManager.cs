using System;
using System.IO;
using System.Linq;

namespace Brage.AliasBeGone.Infrastructure
{
    internal class SnippetsManager : ISnippetsManager
    {
        private const String SNIPPETS_PATH = @"Documents\Visual Studio 2010\Code Snippets\Visual C#\My Code Snippets\";
        private readonly IConfiguration _configuration;
        private readonly IResourceReader _resourceReader;

        public SnippetsManager(IConfiguration configuration, IResourceReader resourceReader)
        {
            _configuration = configuration;
            _resourceReader = resourceReader;
        }

        public void Install()
        {
            foreach (var snippet in _configuration.GetSnippets())
            {
                var snippetContent = _resourceReader.GetResourceContent(snippet);

                if (String.IsNullOrEmpty(snippetContent))
                    continue;

                var targetPath = BuildTargetPath(GetUserPath(), snippet);

                if (!IsSnippetInstalled(targetPath))
                    CreateSnippet(targetPath, snippetContent);
            }
        }

        public void Uninstall()
        {
            foreach (var snippet in _configuration.GetSnippets())
            {
                var targetPath = BuildTargetPath(GetUserPath(), snippet);

                if (IsSnippetInstalled(targetPath))
                    DeleteSnippet(targetPath);
            }
        }

        public Boolean IsInstalled
        {
            get { return _configuration.GetSnippets().All(snippet => IsSnippetInstalled(BuildTargetPath(GetUserPath(), snippet))); }
        }

        private String BuildTargetPath(String userPath, String snippetName)
        {
            return Path.Combine(userPath, SNIPPETS_PATH, snippetName);
        }

        protected virtual void CreateSnippet(String path, String snippetContent)
        {
            using (var file = new StreamWriter(path, false))
                file.Write(snippetContent);
        }

        protected virtual void DeleteSnippet(String path)
        {
            File.Delete(path);
        }

        protected virtual String GetUserPath()
        {
            return Environment.GetEnvironmentVariable("USERPROFILE");
        }

        protected virtual Boolean IsSnippetInstalled(String path)
        {
            return File.Exists(path);
        }
    }
}
