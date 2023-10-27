using System;
using Core.WindowSystem.Layers;
using UniRx;

namespace Core.WindowSystem.MVP
{
    public interface IModel : IDisposable
    {
        ReactiveProperty<ViewState> State { get; }
        Layer Layer { get; set; }
    }
}