using System;
using UniRx;
using UnityEngine;

namespace BaseInfrastructure
{
    public class BasePresenter<TViewContract> : IDisposable where TViewContract : IView
    {
        protected readonly TViewContract View;
        protected readonly CompositeDisposable CompositeDisposable;

        public BasePresenter(TViewContract viewContract)
        {
            View = viewContract;
            CompositeDisposable = new CompositeDisposable();
            
            View.Construct();
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
