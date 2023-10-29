using Cinemachine;
using Codebase.GameplayComponents.CameraFollower;
using UnityEngine;
using Zenject;

namespace Gameplay.CameraFollower
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class VirtualCameraFollower : MonoBehaviour, IVirtualCameraFollower
    {
        private CinemachineVirtualCamera _virtualCamera;

        [Inject]
        public void Initialize()
        {
            _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        }

        public void SetCameraTarget(Transform target)
        {
            _virtualCamera.Follow = target;
        }
    }
}
