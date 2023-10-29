using System;
using Enums;
using Extensions;
using EzySlice;
using UnityEngine;
using Zenject;

namespace Gameplay.Core
{
    public abstract class SliceObject : MonoBehaviour, ISliced
    {
        public event Action<GameObject[]> SliceEvent;
        protected Vector3 SliceCenterPoint { get; set; }
        protected float SliceExplosionForce { get; set; }
        public int PointValue { get; protected set; }
        public IngredientType IngredientType { get; protected set; }

        [SerializeField] private Material _crossSectionMaterial;


        [Inject]
        public virtual void Initialize()
        {
            SetCenterPoint();
        }

        protected abstract void SetCenterPoint();

        public GameObject[] Slice()
        {
            var slicedParts = gameObject.SliceInstantiate(transform.position + SliceCenterPoint, Vector3.forward, _crossSectionMaterial);
            SliceEvent?.Invoke(slicedParts);
            
            Vibration.VibratePredefined(Vibration.PredefinedEffect.EFFECT_TICK);
            
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
            Destroy(slicedPiece, 5);
            Destroy(gameObject, 8);
        }
        private void OnDrawGizmos()
        {
            SetCenterPoint();
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position + SliceCenterPoint, new Vector3(5f, 2, 0.1f));
        }
    }
}