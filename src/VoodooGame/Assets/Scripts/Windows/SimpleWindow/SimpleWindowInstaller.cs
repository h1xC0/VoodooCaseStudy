using Zenject;

namespace Windows.SimpleWindow
{
    public class SimpleWindowInstaller : Installer<SimpleWindowInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SimpleWindowFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SimpleWindowMapper>().AsSingle().NonLazy();
        }
    }
}