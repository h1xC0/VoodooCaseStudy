using System;
using Constants;
using Core.WindowSystem;
using DG.Tweening;
using Services.AnimationService;
using Services.ResourceProvider;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Windows.LevelStateWindow
{
    [RequireComponent(typeof(CanvasGroup))]
    public class LevelEndView : WindowView, ILevelEndView, IResource
    {
        public IObservable<Unit> OnNextLevelButtonPressed { get; private set; }
        
        [SerializeField] private Button nextLevelButton;
        [SerializeField] private GameObject _foodPanel;
        [SerializeField] private Image _foodImage;

        private IAnimationService _animationService;
        private CanvasGroup _canvasGroup;
        
        public void Initialize(IAnimationService animationService)
        {
            OnNextLevelButtonPressed = nextLevelButton.OnClickAsObservable();
            _animationService = animationService;
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void AnimateEnter()
        {
            var floatInOutAnimation =
                _animationService.SetupFloatInOutAnimation(_canvasGroup, 1, AnimationConstants.FloatInOutSpeed);
            floatInOutAnimation.Play();
        }

        public void SetFoodImage(Sprite sprite)
        {
            _foodPanel.SetActive(true);
            _foodImage.sprite = sprite;
        }
    }
}