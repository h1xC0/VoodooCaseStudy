using Zenject;

namespace Windows.LevelStateWindow.Common
{
    public class LevelStateInstaller : Installer<LevelStateInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LevelStateFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<LevelStateMapper>().AsSingle().NonLazy();
        }
    }
}