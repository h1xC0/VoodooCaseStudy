using Windows.SimpleWindow;
using Constants;
using Factories;
using Services.ResourceProvider;
using UnityEngine;
using Zenject;

namespace Windows.PersistentWindow.Common
{
    public class PersistentWindowFactory : AbstractFactory, IPersistentWindowFactory
    {
        private readonly IResourceProviderService _resourceProviderService;

        public PersistentWindowFactory(DiContainer diContainer, IResourceProviderService resourceProviderService) : base(diContainer)
        {
            _resourceProviderService = resourceProviderService;
        }

        public PersistentWindowView CreatePersistentWindowView(Transform parent)
        {
            var prefab = _resourceProviderService.LoadResource<PersistentWindowView>(ResourceNames.PersistentWindowView);
            return CreateObject<PersistentWindowView>(prefab.gameObject, parent);
        }

        public PersistentWindowModel CreatePersistentWindowModel()
        {
            return new PersistentWindowModel();
        }
    }
}