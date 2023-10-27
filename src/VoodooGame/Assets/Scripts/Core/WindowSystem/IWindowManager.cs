using System;
using Core.WindowSystem.Layers;
using Core.WindowSystem.MVP;

namespace Core.WindowSystem
{
    public interface IWindowManager
    {
        event Action<IPresenter> WindowOpenedEvent;
        event Action<IPresenter> WindowClosedEvent;
        event Action<IPresenter> ActiveWindowChangedEvent;
        
        TPresenter Open<TPresenter>(IWindowParameters parameters = null, bool enableBlocker = true)
            where TPresenter : class, IPresenter, new();
        
        void SetCanvasConfig(WindowSystemConfig config);
        void Close(IPresenter windowController, bool executeImmediately = false);
        IWindowOpenInfo GetActiveWindow(Layer layer);
        IWindowOpenInfo GetActiveWindow(string layerName);
        
        bool IsOpened<TPresenter>() where TPresenter : class, IPresenter, new();
        
        Layer GetLayer(string layerName);
    }
}