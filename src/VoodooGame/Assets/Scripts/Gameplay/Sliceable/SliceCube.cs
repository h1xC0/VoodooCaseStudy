using Gameplay.Core;
using UnityEngine;

namespace Gameplay.Sliceable
{
    public class SliceCube : SliceObject
    {
        [SerializeField] private Vector3 _sliceCenterPoint;
        [SerializeField] private float _slicePieceExplosionForce;
        public override void Initialize()
        {
            base.Initialize();    
        }
        
        protected override void SetCenterPoint()
        {
            SliceCenterPoint = _sliceCenterPoint;
            SliceExplosionForce = _slicePieceExplosionForce;
        }
    }
}