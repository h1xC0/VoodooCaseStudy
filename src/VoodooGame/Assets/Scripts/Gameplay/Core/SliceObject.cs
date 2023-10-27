using System;
using EzySlice;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using Plane = EzySlice.Plane;

namespace Gameplay.Core
{
    public abstract class SliceObject : MonoBehaviour, ISliced
    {
        public event Action<GameObject[]> SliceEvent;
        protected Vector3 SliceCenterPoint { get; set; }

        protected float SliceExplosionForce { get; set; }

        [Inject]
        public virtual void Initialize()
        {
            SetCenterPoint();
        }

        protected abstract void SetCenterPoint();

        public GameObject[] Slice()
        {
            var slicedParts = gameObject.SliceInstantiate(transform.position + SliceCenterPoint, Vector3.forward);
            SliceEvent?.Invoke(slicedParts);
            
            foreach (var part in slicedParts)
            {
                AddComponents(part);
            }

            HideObject();
            return slicedParts;
        }
        
        private void HideObject()
        {
            gameObject.SetActive(false);
        }

        private void AddComponents(GameObject slicedPiece)
        {
            slicedPiece.layer = 7;
                
            var meshCollider = slicedPiece.AddComponent<MeshCollider>();
            var rb = slicedPiece.AddComponent<Rigidbody>();
                
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            meshCollider.convex = true;
            
            rb.AddExplosionForce(SliceExplosionForce, transform.position + SliceCenterPoint, 5, 1, ForceMode.Acceleration);
        }
        private void OnDrawGizmos()
        {
            SetCenterPoint();
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position + SliceCenterPoint, new Vector3(5f, 2, 0.1f));
        }
    }
}