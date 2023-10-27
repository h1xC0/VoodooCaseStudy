using System;
using EzySlice;
using UnityEngine;

namespace Gameplay.Core
{
    public interface ISliced
    {
        event Action<GameObject[]> SliceEvent;
        GameObject[] Slice();
    }
}