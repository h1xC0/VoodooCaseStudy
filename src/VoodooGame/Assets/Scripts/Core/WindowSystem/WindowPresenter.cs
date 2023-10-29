using System;
using Core.WindowSystem.MVP;

namespace Core.WindowSystem
{
    public class WindowPresenter<TView, TModel> : Presenter<TView, TModel>, IWindowManipulation
    where TView : IWindowView
    where TModel : class, IWindowModel
    {
        public event Action<IPresenter> OnOpenEvent;
        public event Action<IPresenter> OnCloseEvent;
        public event Action<IPresenter, ViewState> OnCloseCompleteEvent;
        public event Action<IPresenter> OnOpenCompleteEvent;

        public WindowPresenter(TView viewContract, TModel modelContract) : base(viewContract, modelContract)
        {
            View.Initialize();
        }

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
            SilentClose();
        }
        
        protected virtual void OnCloseWindow()
        {
            CompositeDisposable.Add(this);
            OnDispose();
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