using System;
using System.IO;

namespace Brage.AliasBeGone.Infrastructure
{
    internal class ResourceReader : IResourceReader
    {
        private readonly string _resourceNamespace;

        public ResourceReader(String resourceNamespace)
        {
            _resourceNamespace = resourceNamespace;
        }

        public String GetResourceContent(String resourceName)
        {
            var resourceFullName = BuildResourceFullName(resourceName);
            var assembly = GetType().Assembly;
            var stream = assembly.GetManifestResourceStream(resourceFullName);
            
            if (stream != null)
                using (var reader = new StreamReader(stream))
                    return reader.ReadToEnd();

            return null;
        }

        private String BuildResourceFullName(String resourceName)
        {
            if (resourceName.StartsWith(".") || _resourceNamespace.EndsWith("."))
                return String.Concat(_resourceNamespace, resourceName);

            return String.Join(".", _resourceNamespace, resourceName);
        }
    }
}
