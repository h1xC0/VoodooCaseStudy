using Gameplay.Core;
using Services.LevelProgressionService;
using UnityEngine;
using Zenject;

namespace Gameplay.StateTrigger
{
    public class FinishTrigger : MonoBehaviour
    {
        private ILevelProgressionService _levelProgressionService;

        [SerializeField] private bool _levelState;
        
        [Inject]
        public void Initialize(ILevelProgressionService levelProgressionService)
        {
            _levelProgressionService = levelProgressionService;
        }

        private void OnCollisionEnter(Collision collision)
        {
            collision.collider.TryGetComponent(out PlayerKnife playerKnife);
            if (playerKnife != null && _levelProgressionService.LevelEnd == false)
            {
                _levelProgressionService.CompleteLevel(_levelState);
            }
        }
    }
}