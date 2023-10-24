using UnityEngine;

namespace BaseInfrastructure
{
    public class BaseView : MonoBehaviour
    {
        private bool _disposed;

        public virtual void Construct()
        {
            
        }
        
        public void OnDestroy()
        {
            if (_disposed) return;
            Dispose();
        }

        public void Dispose()
        {
            if (_disposed) return;
            
            DisposeView();

            _disposed = true;
            Destroy(gameObject);
        }

        public virtual void DisposeView()
        {
            
        }
    }
}