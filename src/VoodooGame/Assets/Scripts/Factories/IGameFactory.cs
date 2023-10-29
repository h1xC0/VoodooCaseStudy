using Codebase.GameplayComponents.CameraFollower;
using Gameplay;
using Gameplay.Core;

namespace Factories
{
    public interface IGameFactory
    {
        IWeapon CreatePlayerKnife(SpawnPoint spawnPoint);
        IVirtualCameraFollower CreatePlayerCamera(SpawnPoint spawnPoint);
    }
}