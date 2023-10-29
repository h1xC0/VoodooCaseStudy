using Windows.LevelStateWindow.LevelStates;
using Constants;
using Core.WindowSystem;
using Core.WindowSystem.Layers;
using Core.WindowSystem.MVP;
using Services.AnimationService;
using Services.LevelConfigurationService;
using Services.LevelProgressionService;
using Services.PlayerProgression;
using Services.ResourceProvider;
using Systems.CommandSystem;
using UnityEngine;

namespace Windows.LevelStateWindow.Common
{
    [ViewLayer(ViewLayer.Screen)]
    public class LevelStateWindowPresenter : WindowPresenter<ILevelStateView, ILevelStateModel>
    {
        private readonly IResourceProviderService _resourceProviderService;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IPlayerProgressionService _playerProgressionService;
        private readonly ILevelProgressionService _levelProgressionService;
        private readonly ILevelConfigurationService _levelConfigurationService;
        private readonly IAnimationService _animationService;

        public LevelStateWindowPresenter(
            ILevelStateView view,
            ILevelStateModel model,
            IResourceProviderService resourceProviderService,
            ICommandDispatcher commandDispatcher,
            IPlayerProgressionService playerProgressionService,
            ILevelProgressionService levelProgressionService,
            ILevelConfigurationService levelConfigurationService,
            IAnimationService animationService) : base(view, model)
        {
            _resourceProviderService = resourceProviderService;
            _commandDispatcher = commandDispatcher;
            _playerProgressionService = playerProgressionService;
            _levelProgressionService = levelProgressionService;
            _levelConfigurationService = levelConfigurationService;
            _animationService = animationService;
        }
        
        public void SetupView(bool endLevelGameState, Transform parent)
        {
            var winView = _resourceProviderService.LoadResource<LevelEndView>(ResourceNames.VictoryWindowView);
            var loseView = _resourceProviderService.LoadResource<LevelEndView>(ResourceNames.FailWindowView);

            var endLevelPanelView = endLevelGameState ? View.GetVictoryView(winView) : View.GetFailView(loseView);
            
            if (endLevelGameState)
            {
                AddDisposable(new VictoryWindowPresenter(endLevelPanelView,
                    Model,
                    _commandDispatcher,
                    _playerProgressionService,
                    _levelProgressionService,
                    _levelConfigurationService,
                    _animationService));
            }
            else
            {
                AddDisposable(new FailWindowPresenter(endLevelPanelView,
                    Model,
                    _playerProgressionService,
                    _commandDispatcher,
                    _animationService));
            }
        }
    }
}