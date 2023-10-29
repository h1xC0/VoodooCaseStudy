using Windows.LevelStateWindow;
using Windows.LevelStateWindow.Common;
using Windows.PersistentWindow.Common;
using Windows.SimpleWindow;
using Constants;
using Core.WindowSystem;
using Factories;
using Gameplay;
using Payloads;
using Services.AnimationService;
using Services.InputService;
using Services.LevelProgressionService;
using Services.PlayerProgression;
using Signals;
using Signals.SceneLoading;
using Systems.CommandSystem;
using UnityEngine;
using Zenject;
using Input = UnityEngine.Input;

namespace Core.Containers
{
    public class GameplayInstaller : MonoInstaller
    {
        private ICommandDispatcher _commandDispatcher;
        private ICommandBinder _commandBinder;

        [SerializeField] private SpawnPoint _spawnPoint;
        [SerializeField] private GameplayEntryPoint _gameplayEntryPoint;
        
        public override void InstallBindings()
        {
            _commandDispatcher = Container.Resolve<ICommandDispatcher>();
            _commandBinder = Container.Resolve<ICommandBinder>();
            
            BindServices();
            BindCommands();
            BindInstallers();
        }

        public void OnEnable()
        {
            _commandDispatcher.Dispatch<SetupGameplaySignal>(new SetupGameplayPayload(_spawnPoint));
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                _commandDispatcher.Dispatch<LoadNextLevelSignal>(new SceneNamePayload(SceneNames.Gameplay, SceneNames.Gameplay));
            }
        }

        private void BindServices()
        {
            Container
                .BindInterfacesTo<InputService>()
                .FromNew()
                .AsSingle();
            
            Container
                .BindInterfacesTo<GameFactory>()
                .FromNew()
                .AsSingle();
        }

        private void BindCommands()
        {
            if (_commandDispatcher.HasListener(typeof(SetupGameplaySignal))) return;

            _commandBinder
                .Bind<SetupGameplaySignal>()
                .To<SetupGameplayCommand>();

            _commandBinder
                .Bind<LoadNextLevelSignal>()
                .To<UnloadSceneCommand>()
                .To<DisposeLevelStateCommand>()
                .To<LoadSceneCommand>();

            _commandBinder
                .Bind<LevelEndSignal>()
                .To<LevelEndCommand>();
        }

        private void BindInstallers()
        {
            LevelStateInstaller.Install(Container);
        }
    }
}
