using System;
using Core.WindowSystem;
using Services.AnimationService;
using UniRx;
using UnityEngine;

namespace Windows.LevelStateWindow
{
    public interface ILevelEndView : IWindowView
    {
        IObservable<Unit> OnNextLevelButtonPressed { get; }
        void Initialize(IAnimationService animationService);
        void AnimateEnter();
        void SetFoodImage(Sprite sprite);
    }
}