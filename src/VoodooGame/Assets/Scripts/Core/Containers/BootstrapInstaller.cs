using Commands;
using Constants;
using Core.WindowSystem;
using Services.EventBus;
using Services.LevelConfigurationService;
using Services.ResourceProvider;
using Services.SaveLoad;
using Services.Transitions;
using Signals;
using Systems.CommandSystem;
using Systems.CommandSystem.Payloads;
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
            Container
                .Resolve<ICommandDispatcher>()
                .Dispatch<LoadSceneSignal>(new SceneNamePayload(SceneNames.Gameplay));
        }

        private void BindInstallers()
        {
            Container.Install<CommandSystemInstaller>();
            Container.Install<WindowInstaller>();
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