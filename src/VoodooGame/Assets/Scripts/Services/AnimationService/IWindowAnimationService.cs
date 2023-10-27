using System;
using Core.WindowSystem;
using Core.WindowSystem.MVP;
using DG.Tweening;

namespace Services.AnimationService
{
    public interface IWindowAnimationService : IDisposable
    {
        void Initialize(IWindowView windowView);
        Sequence Show();
        Sequence Hide();
    }
}