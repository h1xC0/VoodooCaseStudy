using System;
using Core.WindowSystem.Layers;

namespace Core.WindowSystem.MVP
{
    public interface IPresenter : IDisposable
    {
        Layer Layer { get; set; }

        ViewState State { get; }

        bool IsOpen { get; }
    }
}