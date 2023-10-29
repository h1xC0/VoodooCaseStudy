using Enums;
using Gameplay.Core;
using UnityEngine;

namespace Gameplay.Sliceable
{
    public class SliceIngredient : SliceObject
    {
        [SerializeField] private Vector3 _sliceCenterPoint;
        [SerializeField] private float _slicePieceExplosionForce;
        [SerializeField] private int _pointValue;
        
        [SerializeField] private IngredientType _ingredientType;
        public override void Initialize()
        {
            base.Initialize();  
            PointValue = _pointValue;
            IngredientType = _ingredientType;
        }
        
        protected override void SetCenterPoint()
        {
            SliceCenterPoint = _sliceCenterPoint;
            SliceExplosionForce = _slicePieceExplosionForce;
        }
    }
}