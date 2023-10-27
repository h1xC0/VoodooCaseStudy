using System;
using UnityEngine;

namespace Core.WindowSystem.Layers
{
    [Serializable]
    public class ViewLayerSettings : IViewLayerSettings
    {
        [SerializeField] 
        private string _layerName;
        
        [SerializeField] 
        private bool _isChildOfPreviousLayer = true;

        public string LayerName => _layerName;

        public bool IsChildOfPreviousLayer => _isChildOfPreviousLayer;
    }
}