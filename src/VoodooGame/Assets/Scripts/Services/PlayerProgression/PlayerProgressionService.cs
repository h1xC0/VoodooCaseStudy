using System.Linq;
using Constants;
using Services.LevelConfigurationService;
using Services.SaveLoad;
using UniRx;

namespace Services.PlayerProgression
{
    public class PlayerProgressionService : IPlayerProgressionService
    {
        public IReadOnlyReactiveProperty<int> SoftCurrency => _resourcesCount;
        public IReadOnlyReactiveProperty<int> CurrentLevel => _lastLevelIndex;
        public IReadOnlyReactiveCollection<string> GiftSlots => _giftSlots;

        private readonly ReactiveProperty<int> _resourcesCount;
        private readonly ReactiveProperty<int> _lastLevelIndex;
        private readonly ReactiveCollection<string> _giftSlots;

        private readonly CompositeDisposable _compositeDisposable;
        private readonly ISaveLoadService _saveLoadService;
        private readonly ILevelConfigurationService _levelConfigurationService;

        public PlayerProgressionService(ISaveLoadService saveLoadService,
            ILevelConfigurationService levelConfigurationService)
        {

            _saveLoadService = saveLoadService;
            _levelConfigurationService = levelConfigurationService;

            var playerState = _saveLoadService.Load();

            _compositeDisposable = new CompositeDisposable();
            _resourcesCount = new ReactiveProperty<int>(playerState.ResourcesCount);
            _lastLevelIndex = new ReactiveProperty<int>(playerState.LastLevelIndex);
            _giftSlots = new ReactiveCollection<string>(playerState.GiftSlots);

            SoftCurrency
                .Subscribe(_ => SaveProgress())
                .AddTo(_compositeDisposable);

            CurrentLevel
                .Subscribe(_ => SaveProgress())
                .AddTo(_compositeDisposable);

            GiftSlots.ObserveAdd()
                .Subscribe(_ => SaveProgress())
                .AddTo(_compositeDisposable);
        }

        public void AddResources(int amount)
        {
            if (amount < 0) return;
            _resourcesCount.Value += amount;
        }

        public bool SpendResources(int amount)
        {
            if (_resourcesCount.Value - amount < 0) return false;

            _resourcesCount.Value -= amount;
            return true;
        }

        public void IncreaseLevelIndex()
        {
            _lastLevelIndex.Value += 1;

            if (_lastLevelIndex.Value > _levelConfigurationService.TotalLevels)
            {
                _lastLevelIndex.Value = 1;
            }
        }

        public void BuyGiftSlots(string id)
        {
            if (_giftSlots.Contains(id)) return;
            _giftSlots.Add(id);
        }

        private void SaveProgress()
        {
            var currentProgressionModel = new PlayerProgressionModel(_resourcesCount.Value, _lastLevelIndex.Value,
                _giftSlots.ToArray());

            _saveLoadService.Save(currentProgressionModel);
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
    }
}