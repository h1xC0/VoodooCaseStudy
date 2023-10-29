using Windows.LevelStateWindow.Common;
using Constants;
using Core.WindowSystem;
using Core.WindowSystem.Layers;
using Payloads;
using Services.AnimationService;
using Services.PlayerProgression;
using Signals;
using Systems.CommandSystem;
using UniRx;

namespace Windows.LevelStateWindow.LevelStates
{
    [ViewLayer(ViewLayer.Screen)]
    public class FailWindowPresenter : WindowPresenter<ILevelEndView, ILevelStateModel>
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IPlayerProgressionService _playerProgressionService;
        
        public FailWindowPresenter(
            ILevelEndView levelEndView,
            ILevelStateModel levelStateModel,
            IPlayerProgressionService playerProgressionService,
            ICommandDispatcher commandDispatcher,
            IAnimationService animationService) : base(levelEndView, levelStateModel)
        {
            View = levelEndView;
            _commandDispatcher = commandDispatcher;
            _playerProgressionService = playerProgressionService;
            
            View.Initialize(animationService);

            View.OnNextLevelButtonPressed
                .Subscribe(RestartLevel)
                .AddTo(CompositeDisposable);

            View.AnimateEnter();
        }
        
        private void RestartLevel(Unit args)
        {           
            var levelToUnload = new SceneInfo($"Level {_playerProgressionService.CurrentLevel.Value + 1}",
                "LevelToUnload");
            _commandDispatcher.Dispatch<LoadNextLevelSignal>(new SceneNamePayload(levelToUnload, levelToUnload));
        }
    }
}