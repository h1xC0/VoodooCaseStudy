using Core.WindowSystem;
using Payloads;
using Services.LevelProgressionService;
using Systems.CommandSystem;

namespace Signals
{
    public class DisposeLevelStateCommand : Command
    {
        private readonly IWindowManager _windowManager;
        private readonly ILevelProgressionService _levelProgressionService;

        public DisposeLevelStateCommand(IWindowManager windowManager, ILevelProgressionService levelProgressionService)
        {
            _windowManager = windowManager;
            _levelProgressionService = levelProgressionService;
        }
        
        protected override void Execute(ICommandPayload payload)
        {
            Retain();

            _windowManager.ClearCanvas();
            _levelProgressionService.Dispose();
            
            Release();
        }
    }
}