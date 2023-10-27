using UnityEngine;
using UnityEngine.UI;

namespace Core.WindowSystem.Blockers
{
    public class Blocker : MonoBehaviour, IBlocker
    {
        [SerializeField]
        private Canvas _canvas;
        
        [SerializeField]
        private Image _image;

        private BlockerConfig _config = new BlockerConfig();

        public void SetActive(bool active, string layer)
        {
            float alpha = (active) ? _config.Alpha : 0f;
            float time = (active) ? _config.AnimationTime : 0f;
            
            _image.CrossFadeAlpha(alpha, time, false);

            _image.raycastTarget = active;
            _canvas.sortingOrder = -1;
            if (active)
            {
                string layerName = layer;
                _canvas.sortingLayerName = layerName;
            }
        }

        public void ApplyConfig(BlockerConfig blockerConfig)
        {
            _config = blockerConfig;
        }
    }
}