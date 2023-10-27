using Core.WindowSystem.MVP;

namespace Core.WindowSystem
{
    public interface IWindowOpenInfo
    {
        IWindowManipulation WindowManipulation { get; }
        
        IPresenter Presenter { get; set; }
        
        IWindowData Data { get; }
        
        IWindowParameters Parameters { get; set; }
        
        bool EnableBlocker { get; set; }
    }
}