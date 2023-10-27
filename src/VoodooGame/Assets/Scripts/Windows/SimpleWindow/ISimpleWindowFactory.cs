using Factories;
using UnityEngine;

namespace Windows.SimpleWindow
{
    public interface ISimpleWindowFactory : IAbstractFactory
    {
        SimpleWindowView CreateSimpleWindowView(Transform parent);
        SimpleWindowModel CreateSimpleWindowModel();
    }
}