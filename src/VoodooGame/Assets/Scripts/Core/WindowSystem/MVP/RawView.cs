using System;
using UnityEngine;

namespace Core.WindowSystem.MVP
{
    public abstract class RawView : MonoBehaviour, IView
    {
        [SerializeField] private Canvas _canvas;

        private bool _disposed;

        public abstract void Initialize();
        
        public void OnDestroy()
        {
            if (_disposed) return;
            Dispose();
        }
        
        public void SetupLayer(IModel model)
        {
            CheckCanvas();
            
            if (_canvas != null && model.Layer != null)
            {
                var layerName = model.Layer.Name;
                
                if (_canvas.gameObject.activeInHierarchy && _canvas.enabled)
                {
                    _canvas.overrideSorting = true;
                    _canvas.sortingLayerName = layerName;
                }
                else
                {
                    Debug.LogError("Couldn't set sorting order {0} when canvas disabled or inactive gameobject", this);
                }
            }
        }
        
        private void CheckCanvas()
        {
            if (_canvas == null)
            {
                _canvas = gameObject.GetComponent<Canvas>();
            }
        }

        public virtual void Dispose()
        {
            if (_disposed) return;

            _disposed = true;
            Destroy(gameObject);
        }
    }
}