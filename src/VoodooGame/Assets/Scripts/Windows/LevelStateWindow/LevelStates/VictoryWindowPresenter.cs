using Windows.LevelStateWindow.Common;
using Constants;
using Core.WindowSystem;
using Core.WindowSystem.Layers;
using Payloads;
using Services.AnimationService;
using Services.LevelConfigurationService;
using Services.LevelProgressionService;
using Services.PlayerProgression;
using Signals;
using Systems.CommandSystem;
using UniRx;

namespace Windows.LevelStateWindow.LevelStates
{
    [ViewLayer(ViewLayer.Screen)]
    public class VictoryWindowPresenter : WindowPresenter<ILevelEndView, ILevelStateModel>
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IPlayerProgressionService _playerProgressionService;

        public VictoryWindowPresenter(
            ILevelEndView levelEndView,
            ILevelStateModel levelStateModel,
            ICommandDispatcher commandDispatcher,
            IPlayerProgressionService playerProgressionService,
            ILevelProgressionService levelProgressionService,
            ILevelConfigurationService levelConfigurationService,
            IAnimationService animationService) : base(levelEndView, levelStateModel)
        {
            View = levelEndView;
            _commandDispatcher = commandDispatcher;
            _playerProgressionService = playerProgressionService;

            View.Initialize(animationService);

            View.OnNextLevelButtonPressed
                .Subscribe(LoadNextLevel)
                .AddTo(CompositeDisposable);

            View.AnimateEnter();
            var levelConfiguration =
                levelConfigurationService.GetLevelConfiguration(_playerProgressionService.CurrentLevel.Value);
            View.SetFoodImage(levelConfiguration.LevelRecipe.Icon);
        }
        
        private void LoadNextLevel(Unit args)
        {        
            var levelToUnload = new SceneInfo($"Level {_playerProgressionService.CurrentLevel.Value + 1}",
                "LevelToUnload");
            
            _playerProgressionService.IncreaseLevelIndex();

            var levelToLoad = new SceneInfo($"Level {_playerProgressionService.CurrentLevel.Value + 1}", "LevelToLoad");
            _commandDispatcher.Dispatch<LoadNextLevelSignal>(new SceneNamePayload(levelToLoad, levelToUnload));
        }
    }
}