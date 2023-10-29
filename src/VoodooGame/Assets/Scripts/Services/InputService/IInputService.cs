using System;

namespace Services.InputService
{
    public interface IInputService : IDisposable
    {
        bool GetClickOnScreen();
    }
}