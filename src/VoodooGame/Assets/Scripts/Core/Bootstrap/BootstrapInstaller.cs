using Commands;
using Constants;
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

namespace Bootstrap
{
    public class BootstrapInstaller : MonoInstaller
    {

        [SerializeField] private SceneTransitionService _sceneTransitionService;
        public override void InstallBindings()
        {
            BindServices();
            BindCommands(Container.Resolve<ICommandBinder>());
        }

        public override void Start()
        {
            Container
                .Resolve<ICommandDispatcher>()
                .Dispatch<LoadGameplaySignal>(new SceneNamePayload(SceneNames.Gameplay));
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
                .BindInterfacesTo<CommandBinder>()
                .FromNew()
                .AsSingle()
                .CopyIntoAllSubContainers();

            Container
                .BindInterfacesTo<CommandDispatcher>()
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

        private void BindCommands(ICommandBinder commandBinder) =>
            commandBinder.Bind<LoadGameplaySignal>()
                .To<LoadSceneCommand>();
    }
}