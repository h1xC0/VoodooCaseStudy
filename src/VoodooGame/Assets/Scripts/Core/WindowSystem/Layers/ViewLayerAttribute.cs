using System;

namespace Core.WindowSystem.Layers
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ViewLayerAttribute : Attribute
    {
        public string Name { get; }

        public ViewLayerAttribute(string name)
        {
            Name = name;
        }
    }
}