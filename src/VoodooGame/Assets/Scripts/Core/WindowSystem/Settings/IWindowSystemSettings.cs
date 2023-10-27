using System.Collections.Generic;
using Core.WindowSystem.Layers;
using Services.ResourceProvider;

namespace GP.Framework.WindowSystem
{
    public interface IWindowSystemSettings : IResource
    {
        List<ViewLayerSettings> LayersSettings { get; }
    }
}