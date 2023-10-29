using System;
using UnityEngine;

namespace Core.WindowSystem.MVP
{
    public abstract class RawView : MonoBehaviour, IView
    {

        private bool _disposed;

        public virtual void Initialize()
        {
            
        }
        
        public void OnDestroy()
        {
            if (_disposed) return;
            Dispose();
        }

        public virtual void Dispose()
        {
            if (_disposed) return;

            _disposed = true;
            Destroy(gameObject);
        }
    }
}