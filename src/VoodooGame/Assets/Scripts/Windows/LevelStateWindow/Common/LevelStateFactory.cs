using Constants;
using Factories;
using Services.LevelProgressionService;
using Services.PlayerProgression;
using Services.ResourceProvider;
using UnityEngine;
using Zenject;

namespace Windows.LevelStateWindow.Common
{
    public class LevelStateFactory : AbstractFactory, ILevelStateWindowFactory
    {
        private readonly IResourceProviderService _resourceProviderService;

        public LevelStateFactory(
            DiContainer diContainer,
            IResourceProviderService resourceProviderService) : base(diContainer)
        {
            _resourceProviderService = resourceProviderService;
        }

        public LevelStateWindowView CreateLevelStateWindowView(Transform parent)
        {
            var prefab = _resourceProviderService
                .LoadResource<LevelStateWindowView>(ResourceNames.LevelStateWindowView);
            
            return CreateObject<LevelStateWindowView>(prefab.gameObject, parent);
        }
        public LevelStateWindowModel CreateLevelStateWindowModel()
        {
            return new LevelStateWindowModel();
        }
    }
}