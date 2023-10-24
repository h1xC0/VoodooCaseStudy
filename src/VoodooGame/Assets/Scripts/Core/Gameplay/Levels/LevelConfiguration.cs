using Services.ResourceProvider;
using UnityEngine;

namespace Core.Gameplay.Levels
{
    [CreateAssetMenu(fileName = "Level", menuName = "Constants/Level")]
    public class LevelConfiguration : ScriptableObject, IResource
    {
        public int LevelNumber = 1;
        public int Difficulty = 1;
    }
}