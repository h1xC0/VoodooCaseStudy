using System;
using Core.WindowSystem.Layers;
using UniRx;
using Zenject;

namespace Core.WindowSystem.MVP
{
    public abstract class Presenter<TViewContract, TModelContract> : IPresenter
        where TViewContract : IView
        where TModelContract : class, IModel
    {
        protected IView View { get; set; }
        protected IModel Model { get; private set; }
        protected CompositeDisposable CompositeDisposable { get; set; }

        [Inject] protected IWindowManager WindowManager { get; private set; }

        public Layer Layer
        {
            get => Model.Layer;
            set => Model.Layer = value;
        }

        public ViewState State => Model.State.Value;
        public bool IsOpen => Model.State.Value == ViewState.Open;
        
        public void Initialize(IView viewContract, IModel modelContract)
        {
            View = viewContract;
            Model = modelContract;
            CompositeDisposable = new CompositeDisposable();
            
            View.Initialize();
            View.SetupLayer(modelContract);
        }

        public void AddDisposable(IDisposable disposable)
        {
            CompositeDisposable.Add(disposable);
        }

        public void Dispose()
        {
            CompositeDisposable?.Dispose();
        }
    }
}
