using Constants;
using Factories;
using Services.ResourceProvider;
using UnityEngine;
using Zenject;

namespace Windows.SimpleWindow
{
    public class SimpleWindowFactory : AbstractFactory, ISimpleWindowFactory
    {
        private readonly IResourceProviderService _resourceProviderService;

        public SimpleWindowFactory(DiContainer diContainer, IResourceProviderService resourceProviderService) : base(diContainer)
        {
            _resourceProviderService = resourceProviderService;
        }

        public SimpleWindowView CreateSimpleWindowView(Transform parent)
        {
            var prefab = _resourceProviderService.LoadResource<SimpleWindowView>(ResourceNames.SimpleWindowView);
            return CreateObject<SimpleWindowView>(prefab.gameObject, parent);
        }

        public SimpleWindowModel CreateSimpleWindowModel()
        {
            return new SimpleWindowModel();
        }
    }
}