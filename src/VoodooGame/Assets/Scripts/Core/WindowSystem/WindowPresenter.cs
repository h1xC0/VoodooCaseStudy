using System;
using Core.WindowSystem.MVP;

namespace Core.WindowSystem
{
    public class WindowPresenter : Presenter<IWindowView, IWindowModel>, IWindowManipulation
    {
        public event Action<IPresenter> OnOpenEvent;
        public event Action<IPresenter> OnCloseEvent;
        public event Action<IPresenter, ViewState> OnCloseCompleteEvent;
        public event Action<IPresenter> OnOpenCompleteEvent;
        
        public void Close()
        {
            WindowManager.Close(this);
        }
        
        public void SilentClose()
        {
            CloseWindow(true);
        }

        void IWindowManipulation.Open()
        {
            OnOpenEvent?.Invoke(this);
        
            Model.State.Value = ViewState.Open;
            OnOpen();
        }

        void IWindowManipulation.Close()
        {
            
        }
        
        protected virtual void OnCloseWindow()
        {
        }
        
        protected virtual void OnOpen()
        {
        }
        
        protected virtual void OnDispose()
        {
            Dispose();
        }

        private void CloseWindow(bool silentClose = false)
        {
            if (!silentClose)
            {
                Model.State.Value = ViewState.Close;
                OnCloseEvent?.Invoke(this);
            }
            else
            {
                Model.State.Value = ViewState.SilentClose;
            }

            if (View != null)
            {
                OnCloseWindow();
            }
        }
    }
}