using Windows.PersistentWindow.Common;
using Core.WindowSystem;
using UnityEngine;
using Zenject;

namespace Core
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [Inject]
        public void Initialize(IWindowManager windowManager)
        {
            windowManager.Open<PersistentWindowPresenter>();
        }
    }
}