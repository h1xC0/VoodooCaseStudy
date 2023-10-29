using System;
using Core.WindowSystem.Layers;
using UniRx;
using Zenject;

namespace Core.WindowSystem.MVP
{
    public class Presenter<TViewContract, TModelContract> : IPresenter
        where TViewContract : IView
        where TModelContract : class, IModel
    {
        public TViewContract View { get; set; }
        public TModelContract Model { get; private set; }
        protected CompositeDisposable CompositeDisposable { get; set; }

        [Inject] protected IWindowManager WindowManager { get; private set; }

        public Layer Layer
        {
            get => Model.Layer;
            set => Model.Layer = value;
        }

        public ViewState State => Model.State.Value;
        public bool IsOpen => Model.State.Value == ViewState.Open;
        
        public Presenter(TViewContract viewContract, TModelContract modelContract)
        {
            View = viewContract;
            Model = modelContract;
            CompositeDisposable = new CompositeDisposable();
            
            View.Initialize();
            AddDisposable(View);
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
