using Core.WindowSystem;
using Core.WindowSystem.Layers;
using Services.LevelConfigurationService;
using Services.PlayerProgression;
using Services.SaveLoad;
using UniRx;

namespace Windows.PersistentWindow.Common
{
    [ViewLayer(ViewLayer.Default)]
    public class PersistentWindowPresenter : WindowPresenter<IPersistentWindowView, IPersistentWindowModel>
    {
        private readonly ILevelConfigurationService _levelConfigurationService;
        private readonly IPlayerProgressionService _playerProgressionService;
        private readonly ISaveLoadService _saveLoadService;

        public PersistentWindowPresenter(
            IPersistentWindowView persistentWindowView,
            IPersistentWindowModel persistentWindowModel,
            ILevelConfigurationService levelConfigurationService,
            IPlayerProgressionService playerProgressionService,
            ISaveLoadService saveLoadService) : base(persistentWindowView, persistentWindowModel)
        {
            _levelConfigurationService = levelConfigurationService;
            _playerProgressionService = playerProgressionService;
            _saveLoadService = saveLoadService;
            
            Initialize();
        }

        public void Initialize()
        {
            var levelConfiguration = _levelConfigurationService.GetLevelConfiguration(_playerProgressionService.CurrentLevel.Value);
            View.SetLevelNumber(_playerProgressionService.CurrentLevel.Value + 1);
            View.SetLevelObjectives(levelConfiguration.LevelRecipe);

            _playerProgressionService.SoftCurrency
                .Subscribe(View.SetMoney)
                .AddTo(CompositeDisposable);
            
            View.SetMoney(_playerProgressionService.SoftCurrency.Value);
        }
    }
}