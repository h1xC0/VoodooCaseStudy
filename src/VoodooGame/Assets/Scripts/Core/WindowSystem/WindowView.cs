using System;
using Core.WindowSystem.MVP;
using Services.AnimationService;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.WindowSystem
{
    public class WindowView : RawView, IWindowView
    {
        public event Action CloseWindowIntentEvent;
        public event Action ViewDestroyedEvent;
        public event Action ShowEndEvent;

        private IWindowAnimationService _windowAnimationService;
        [Inject] private IWindowManagerFactory _windowManagerFactory;

        protected IWindowAnimationService WindowAnimationService
        {
            get
            {
                if (_windowAnimationService != null)
                {
                    return _windowAnimationService;
                }

                _windowAnimationService = GetComponent<IWindowAnimationService>() ?? new WindowAnimationService();
                _windowAnimationService.Initialize(this);

                return _windowAnimationService;
            }
        }
        
        [SerializeField] private bool _closeOnClickOutside;
        protected bool CloseOnClickOutside
        {
            get => _closeOnClickOutside && _outsideAreaButton != null;
            set
            {
                _closeOnClickOutside = value;

                if (value && _outsideAreaButton == null)
                {
                    _outsideAreaButton = _windowManagerFactory.CreateOutsideAreaButton(this);
                    _outsideAreaButton.onClick.AddListener(OutsideAreaClickHandler);
                }

                if (_outsideAreaButton != null)
                {
                    _outsideAreaButton.interactable = value;
                }
            }
        }
        
        [SerializeField] private Button _closeWindowButton;
       
        private Button _outsideAreaButton;

        public override void Initialize()
        {
            
        }
        
        public void OnShowBegin()
        {
            gameObject.SetActive(true);
            _windowManagerFactory.GetForegroundBlocker().SetActive(true);
            OnShowBeginEvent();
        }

        public void OnShowEnd()
        {
            _windowManagerFactory.GetForegroundBlocker().SetActive(false);
            OnShowEndEvent();
            ShowEndEvent?.Invoke();
        }
        

        protected virtual void Close()
        {
            Dispose();
        }
        
        protected virtual void CloseButtonClickHandler()
        {
            InitiateWindowClosing();
        }

        protected virtual void OutsideAreaClickHandler()
        {
            InitiateWindowClosing();
        }

        protected void InitiateWindowClosing()
        {
            CloseWindowIntentEvent?.Invoke();
        }
        
        protected virtual void OnStartEvent()
        {
        }

        protected virtual void OnDisposeEvent()
        {
            Destroy(gameObject);
        }

        private void StateChangedHandler(ViewState state)
        {
            switch (state)
            {
                case ViewState.None:
                    break;
                case ViewState.Open:
                    gameObject.SetActive(true);
                    _windowAnimationService.Show();
                    break;
                case ViewState.Close:
                    WindowAnimationService.Hide();
                    break;
                case ViewState.SilentClose:
                    Close();
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
        
        protected virtual void OnShowBeginEvent()
        {
        }

        protected virtual void OnShowEndEvent()
        {
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public override void Dispose()
        {
            if (_closeWindowButton != null)
            {
                _closeWindowButton.onClick.RemoveListener(CloseButtonClickHandler);
            }

            if (_outsideAreaButton != null)
            {
                _outsideAreaButton.onClick.RemoveListener(OutsideAreaClickHandler);
            }

            ViewDestroyedEvent?.Invoke();
            OnDisposeEvent();
            
            base.Dispose();
        }
    }
}