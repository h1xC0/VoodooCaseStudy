using System;
using UniRx;

namespace Services.PlayerProgression
{
    public interface IPlayerProgressionService : IDisposable
    {
        IReadOnlyReactiveProperty<int> SoftCurrency { get; }
        IReadOnlyReactiveProperty<int> CurrentLevel { get; }
        IReadOnlyReactiveCollection<string> GiftSlots { get; }
        void AddResources(int amount);
        bool SpendResources(int amount);
        void IncreaseLevelIndex();
        void BuyGiftSlots(string id);
    }
}