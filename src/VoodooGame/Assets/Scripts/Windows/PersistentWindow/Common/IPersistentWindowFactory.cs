using Factories;
using UnityEngine;

namespace Windows.PersistentWindow.Common
{
    public interface IPersistentWindowFactory : IAbstractFactory
    {
        PersistentWindowView CreatePersistentWindowView(Transform parent);
        PersistentWindowModel CreatePersistentWindowModel();
    }
}