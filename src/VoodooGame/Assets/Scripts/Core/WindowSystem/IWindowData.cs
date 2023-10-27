using Core.WindowSystem.Layers;
using Core.WindowSystem.MVP;

namespace Core.WindowSystem
{
    public interface IWindowData
    {
        int Id { get; set; }
        Layer Layer { get; set; }
    }
}