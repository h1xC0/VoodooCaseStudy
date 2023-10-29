using Windows.SimpleWindow;
using Zenject;

namespace Windows.PersistentWindow.Common
{
    public class PersistentWindowInstaller : Installer<PersistentWindowInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PersistentWindowFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PersistentWindowMapper>().AsSingle().NonLazy();
        }
    }
}