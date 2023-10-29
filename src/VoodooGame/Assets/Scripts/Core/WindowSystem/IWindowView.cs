using System;
using Core.WindowSystem.MVP;

namespace Core.WindowSystem
{
    public interface IWindowView : IView
    {
        void Disable();

        event Action CloseWindowIntentEvent;
        
        event Action ViewDestroyedEvent;
        
        event Action ShowEndEvent;

    }
}