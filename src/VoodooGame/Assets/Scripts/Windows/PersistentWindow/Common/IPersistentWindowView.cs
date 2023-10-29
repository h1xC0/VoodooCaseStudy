using System;
using Core.Gameplay.Levels;
using Core.WindowSystem;
using UniRx;

namespace Windows.PersistentWindow.Common
{
    public interface IPersistentWindowView : IWindowView
    {
        IObservable<Unit> HideViewObservable { get; }
        void SetMoney(int value);
        void SetLevelNumber(int value);
        void SetLevelObjectives(FoodRecipe foodRecipe);

    }
}