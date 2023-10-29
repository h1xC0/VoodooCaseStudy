using System;
using Services.ResourceProvider;
using UnityEngine;

namespace Gameplay.Core
{
    public interface IWeapon : IResource, IDisposable
    {
        Transform Transform { get; }
    }
}