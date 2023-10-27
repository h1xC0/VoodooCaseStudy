using Constants;
using Core.WindowSystem.Settings;
using Services.ResourceProvider;
using Zenject;

namespace Core.WindowSystem
{
    public class WindowInstaller : Installer
    {
        public override void InstallBindings()
        {
            var resourceProvider = Container.Resolve<IResourceProviderService>();
            
            var settings = resourceProvider.LoadResource<WindowSystemSettings>(ResourceNames.WindowSystemSettings);
            Container.BindInterfacesAndSelfTo<WindowSystemSettings>()
                .FromInstance(settings);
            Container.BindInterfacesTo<WindowManagerFactory>().AsSingle().NonLazy();
            Container.BindInterfacesTo<WindowManager>().AsSingle().NonLazy();

        }
    }
}
