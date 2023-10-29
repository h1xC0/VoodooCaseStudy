using Windows.PersistentWindow.Common;
using Core.WindowSystem;
using Factories;
using Payloads;
using Systems.CommandSystem;

namespace Signals
{
    public class SetupGameplayCommand : Command
    {
        private readonly IGameFactory _gameFactory;
        private readonly IWindowManager _windowManager;

        public SetupGameplayCommand(IGameFactory gameFactory, IWindowManager windowManager)
        {
            _gameFactory = gameFactory;
            _windowManager = windowManager;
        }
        protected override void Execute(ICommandPayload payload)
        {
            var gameplayPayload = payload as SetupGameplayPayload;
            
            var playerKnife = _gameFactory.CreatePlayerKnife(gameplayPayload.SpawnPoint);
            var cameraFollower = _gameFactory.CreatePlayerCamera(gameplayPayload.SpawnPoint);
            
            cameraFollower.SetCameraTarget(playerKnife.Transform);
        }
    }
}