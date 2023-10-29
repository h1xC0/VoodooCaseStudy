using Services.ResourceProvider;
using UnityEngine;

namespace Codebase.GameplayComponents.CameraFollower
{
    public interface IVirtualCameraFollower : IResource
    {
        void SetCameraTarget(Transform target);
    }
}