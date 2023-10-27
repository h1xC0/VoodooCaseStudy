using System;
using UnityEngine;

namespace Gameplay.Core
{
    [RequireComponent(typeof(Collider))]
    public class KnifeSlicer : MonoBehaviour
    {
        [SerializeField] private Collider _knifeCutter;
        [SerializeField] private LayerMask _layerMask;

        private void OnCollisionEnter(Collision collision)
        {
            var bounds = _knifeCutter.bounds;
            var colliders = Physics.OverlapBox(bounds.center, bounds.extents, Quaternion.identity, _layerMask);

            foreach (var collider1 in colliders)
            {
                collider1.TryGetComponent(out ISliced sliceObject);
                if (sliceObject != null)
                {
                    sliceObject.Slice();
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            var bounds = _knifeCutter.bounds;
            var colliders = Physics.OverlapBox(bounds.center, bounds.extents, Quaternion.identity, _layerMask);

            foreach (var collider1 in colliders)
            {
                collider1.TryGetComponent(out ISliced sliceObject);
                if (sliceObject != null)
                {
                    sliceObject.Slice();
                }
            }
        }
    }
}