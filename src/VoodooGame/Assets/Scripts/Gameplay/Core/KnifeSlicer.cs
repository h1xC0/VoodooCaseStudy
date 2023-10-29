using System;
using System.Threading.Tasks;
using Enums;
using Services.LevelProgressionService;
using UnityEngine;

namespace Gameplay.Core
{
    [RequireComponent(typeof(Collider))]
    public class KnifeSlicer : MonoBehaviour
    {
        [SerializeField] private Collider _knifeCutter;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private LayerMask _groundLayerMask;

        public event Action<ISliced> SliceObjectEvent;
        public event Action KnifeReachedGroundEvent;

        private ILevelProgressionService _levelProgressionService;
        public void Initialize(ILevelProgressionService levelProgressionService)
        {
            _levelProgressionService = levelProgressionService;
        }

        private void OnTriggerEnter(Collider other)
        {
            var bounds = _knifeCutter.bounds;
            var colliders = Physics.OverlapBox(bounds.center, bounds.extents, Quaternion.identity, _layerMask);

            foreach (var overlapCollider in colliders)
            {
                overlapCollider.TryGetComponent(out ISliced sliceObject);
                
                if (sliceObject == null) continue;
                
                sliceObject.Slice();
                SliceObjectEvent?.Invoke(sliceObject);
            }

            if (other.gameObject.layer == _groundLayerMask)
            {
                KnifeReachedGroundEvent?.Invoke();
            }
        }

        public async void SetCollider(bool flag, int delay = 0)
        {
            if (delay > 0)
            {
                await Task.Delay(delay);
            }
            _knifeCutter.enabled = flag;
        }
    }
}