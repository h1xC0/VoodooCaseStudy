using Constants;
using DG.Tweening;
using UnityEngine;

namespace Services.Transitions
{
    [RequireComponent(typeof(CanvasGroup))]
    public class SceneTransitionService : MonoBehaviour, ISceneTransitionService
    {
        [SerializeField] private float _fadeTime = 3f;
        private CanvasGroup _canvasGroup;
        
        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void FadeIn()
        {
            _canvasGroup
                .DOFade(1, AnimationConstants.AnimationSpeed)
                .SetDelay(_fadeTime)
                .OnComplete(() =>
                {
                    _canvasGroup.blocksRaycasts = true;
                    _canvasGroup.interactable = true;
                });
            
        }

        public void FadeOut()
        {
            _canvasGroup
                .DOFade(0, AnimationConstants.AnimationSpeed)
                .SetDelay(_fadeTime)
                .OnComplete(() =>
                {
                    _canvasGroup.blocksRaycasts = false;
                    _canvasGroup.interactable = false;
                });
        }
    }
}