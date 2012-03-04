using System;

namespace Brage.AliasBeGone.Infrastructure
{
    public interface IResourceReader
    {
        String GetResourceContent(String resourceName);
    }
}