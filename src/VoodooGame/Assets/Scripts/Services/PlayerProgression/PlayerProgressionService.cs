using System.Linq;
using Constants;
using Services.LevelConfigurationService;
using Services.SaveLoad;
using UniRx;

namespace Services.PlayerProgression
{
    public class PlayerProgressionService : IPlayerProgressionService
    {
        public IReadOnlyReactiveProperty<int> SoftCurrency => _MoneyCount;
        public IReadOnlyReactiveProperty<int> CurrentLevel => _lastLevelIndex;

        private readonly ReactiveProperty<int> _MoneyCount;
        private readonly ReactiveProperty<int> _lastLevelIndex;

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
            _MoneyCount = new ReactiveProperty<int>(playerState.MoneyCount);
            _lastLevelIndex = new ReactiveProperty<int>(playerState.LastLevelIndex);

            SoftCurrency
                .Subscribe(_ => SaveProgress())
                .AddTo(_compositeDisposable);

            CurrentLevel
                .Subscribe(_ => SaveProgress())
                .AddTo(_compositeDisposable);
        }

        public void AddResources(int amount)
        {
            if (amount < 0) return;
            _MoneyCount.Value += amount;
        }

        public bool SpendResources(int amount)
        {
            if (_MoneyCount.Value - amount < 0) return false;

            _MoneyCount.Value -= amount;
            return true;
        }

        public void IncreaseLevelIndex()
        {
            _lastLevelIndex.Value += 1;

            if (_lastLevelIndex.Value >= _levelConfigurationService.TotalLevels)
            {
                _lastLevelIndex.Value = 0;
            }
        }

        private void SaveProgress()
        {
            var currentProgressionModel = new PlayerProgressionModel(_MoneyCount.Value, _lastLevelIndex.Value);

            _saveLoadService.Save(currentProgressionModel);
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
    }
}