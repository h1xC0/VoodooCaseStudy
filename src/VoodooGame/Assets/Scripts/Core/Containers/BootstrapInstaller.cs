using Windows.LevelStateWindow;
using Windows.LevelStateWindow.Common;
using Windows.PersistentWindow.Common;
using Windows.SimpleWindow;
using Constants;
using Core.WindowSystem;
using Payloads;
using Services.AnimationService;
using Services.EventBus;
using Services.InputService;
using Services.LevelConfigurationService;
using Services.LevelProgressionService;
using Services.PlayerProgression;
using Services.ResourceProvider;
using Services.SaveLoad;
using Services.Transitions;
using Signals;
using Signals.SceneLoading;
using Systems.CommandSystem;
using UnityEngine;
using Zenject;

namespace Core.Containers
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private SceneTransitionService _sceneTransitionService;
        
        public override void InstallBindings()
        {
            BindServices();
            BindInstallers();
            BindCommands(Container.Resolve<ICommandBinder>());
        }

        public override void Start()
        {
            var index = Container.Resolve<IPlayerProgressionService>().CurrentLevel.Value;
            var levelToLoad = new SceneInfo($"Level {index + 1}", "LevelToLoad");

            Container
                .Resolve<ICommandDispatcher>()
                .Dispatch<LoadSceneSignal>(new SceneNamePayload(levelToLoad, null));
        }

        private void BindInstallers()
        {
            Container.Install<CommandSystemInstaller>();
            Container.Install<WindowInstaller>();
            
            SimpleWindowInstaller.Install(Container);
            PersistentWindowInstaller.Install(Container);
        }

        private void BindServices()
        {
            Container
                .BindInterfacesTo<ResourceProviderService>()
                .FromNew()
                .AsSingle();

            Container
                .BindInterfacesTo<EventBusService>()
                .FromNew()
                .AsSingle();

            Container
                .BindInterfacesTo<SceneTransitionService>()
                .FromInstance(_sceneTransitionService)
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesTo<SaveLoadService>()
                .FromNew()
                .AsSingle();
            
            Container
                .BindInterfacesTo<LevelConfigurationService>()
                .FromNew()
                .AsSingle();

            Container
                .BindInterfacesTo<AnimationService>()
                .FromNew()
                .AsSingle();
            
            Container
                .BindInterfacesTo<PlayerProgressionService>()
                .FromNew()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInterfacesTo<LevelProgressionService>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindCommands(ICommandBinder commandBinder)
        {
            commandBinder.Bind<LoadSceneSignal>()
                .To<LoadSceneCommand>();

            commandBinder.Bind<UnloadSceneSignal>()
                .To<UnloadSceneCommand>();
        }
    }
}