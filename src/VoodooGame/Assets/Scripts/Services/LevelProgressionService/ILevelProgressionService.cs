using System;
using Core.Gameplay.Levels;
using Enums;
using UniRx;

namespace Services.LevelProgressionService
{
    public interface ILevelProgressionService : IDisposable
    {
        bool LevelEnd { get; }
        // bool LevelState { get; }
        void SetLevelEnded(bool flag);
        void CompleteLevel(bool levelComplete);

        void ScoreIngredient(IngredientType ingredientType);
        bool CheckAllCollectedIngredients();
    }
}