using Windows.LevelStateWindow.Common;
using Core.WindowSystem;
using Payloads;
using Services.LevelProgressionService;
using Systems.CommandSystem;

namespace Signals
{
    public class LevelEndCommand : Command
    {
        private readonly ILevelProgressionService _levelProgressionService;
        private readonly IWindowManager _windowManager;


        public LevelEndCommand(IWindowManager windowManager, ILevelProgressionService levelProgressionService)
        {
            _levelProgressionService = levelProgressionService;
            _windowManager = windowManager;
        }
        
        protected override void Execute(ICommandPayload payload)
        {
            Retain();

            var gameState = payload as LevelEndStatePayload;
            _levelProgressionService.SetLevelEnded(gameState.LevelEnded);

            _windowManager.Open<LevelStateWindowPresenter>().SetupView(gameState.LevelState, _windowManager.MainCanvas.transform);
            
            Release();
        }

    }
}