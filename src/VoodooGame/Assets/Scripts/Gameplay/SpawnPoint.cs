using UnityEngine;

namespace Gameplay
{
    public class SpawnPoint : MonoBehaviour
    {
        public void SetSpawnPoint(Transform other)
        {
            other.position = transform.position;
        }
    }
}