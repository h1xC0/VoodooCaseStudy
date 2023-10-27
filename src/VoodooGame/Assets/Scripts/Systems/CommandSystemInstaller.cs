using Systems.CommandSystem;
using Zenject;

public class CommandSystemInstaller : Installer
{
    public override void InstallBindings()
    {
        Container
            .BindInterfacesTo<CommandBinder>()
            .FromNew()
            .AsSingle()
            .CopyIntoAllSubContainers();

        Container
            .BindInterfacesTo<CommandDispatcher>()
            .FromNew()
            .AsSingle();
    }
}
