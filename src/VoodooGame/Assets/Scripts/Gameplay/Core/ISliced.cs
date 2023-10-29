using System;
using Enums;
using EzySlice;
using UnityEngine;

namespace Gameplay.Core
{
    public interface ISliced
    {
        event Action<GameObject[]> SliceEvent;
        GameObject[] Slice();
        int PointValue { get; }
        IngredientType IngredientType { get; }
    }
}