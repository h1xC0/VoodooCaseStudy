using Gameplay.Core;
using Services.ResourceProvider;
using UnityEngine;
using Zenject;

namespace Gameplay.Sliceable
{
    public class SlicePrimitive : SliceObject
    {
        [SerializeField] private Vector3 _sliceCenterPoint;
        [SerializeField] private float _slicePieceExplosionForce;
        [SerializeField] private int _pointValue;
        
        public override void Initialize()
        {
            base.Initialize();  
            PointValue = _pointValue;
        }
        
        protected override void SetCenterPoint()
        {
            SliceCenterPoint = _sliceCenterPoint;
            SliceExplosionForce = _slicePieceExplosionForce;
        }
    }
}