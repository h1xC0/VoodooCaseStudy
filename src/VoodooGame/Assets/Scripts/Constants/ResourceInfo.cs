using System;

namespace Constants
{
    public class ResourceInfo
    {
        public Type Type;
        public string Path;
        
        public ResourceInfo(Type type, string path)
        {
            Type = type;
            Path = path;
        }
    }
}