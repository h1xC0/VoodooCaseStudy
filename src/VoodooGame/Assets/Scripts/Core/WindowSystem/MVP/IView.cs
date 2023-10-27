using System;

namespace Core.WindowSystem.MVP
{
    public interface IView : IDisposable
    {
        void Initialize();
        void SetupLayer(IModel model);
    }
}