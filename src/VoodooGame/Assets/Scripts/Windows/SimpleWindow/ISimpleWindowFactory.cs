using Factories;
using UnityEngine;

namespace Windows
{
    public interface ISimpleWindowFactory : IAbstractFactory
    {
        SimpleWindowView CreateSimpleWindowView(Transform parent);
    }
}