using Codebase.GameplayComponents.CameraFollower;
using Constants;
using Gameplay;
using Gameplay.CameraFollower;
using Gameplay.Core;
using Services.ResourceProvider;
using Zenject;

namespace Factories
{
    public class GameFactory : AbstractFactory, IGameFactory 
    {
        private readonly IResourceProviderService _resourceProviderService;

        public GameFactory(DiContainer diContainer, IResourceProviderService resourceProviderService) : base(diContainer)
        {
            _resourceProviderService = resourceProviderService;
        }

        public IWeapon CreatePlayerKnife(SpawnPoint spawnPoint)
        {
            var prefab = _resourceProviderService.LoadResource<PlayerKnife>(ResourceNames.PlayerKnife);
            var createdObject = CreateObject<PlayerKnife>(prefab.gameObject, spawnPoint.transform);
            spawnPoint.SetSpawnPoint(createdObject.transform);
            
            return createdObject;
        }

        public IVirtualCameraFollower CreatePlayerCamera(SpawnPoint spawnPoint)
        {
            var prefab = _resourceProviderService.LoadResource<VirtualCameraFollower>(ResourceNames.PlayerCamera);
            var createdObject = CreateObject<VirtualCameraFollower>(prefab.gameObject, spawnPoint.transform);
            return createdObject;
        }
    }
}