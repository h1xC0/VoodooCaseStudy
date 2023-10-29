using Factories;
using UnityEngine;

namespace Windows.LevelStateWindow.Common
{
    public interface ILevelStateWindowFactory : IAbstractFactory
    {
        LevelStateWindowView CreateLevelStateWindowView(Transform parent);
        LevelStateWindowModel CreateLevelStateWindowModel();
    }
}