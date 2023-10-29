using Core.WindowSystem.Layers;
using Core.WindowSystem.MVP;

namespace Core.WindowSystem
{
    public class WindowOpenInfo : IWindowOpenInfo
    {
        private IPresenter _window;
        
        public IWindowManipulation WindowManipulation { get; private set; }
        
        public IWindowData Data { get; private set; }

        public IPresenter Presenter
        {
            get => _window;
            set
            {
                _window = value;
                WindowManipulation = (IWindowManipulation) _window;
            }
        }
        
        public IWindowParameters Parameters { get; set; }

        public bool EnableBlocker { get; set; }

        public WindowOpenInfo(Layer layer)
        {
            Data = WindowData.Create(GetHashCode(), layer);
        }
    }
}