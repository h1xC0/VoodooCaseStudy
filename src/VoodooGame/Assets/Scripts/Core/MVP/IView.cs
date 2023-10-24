using System;

namespace BaseInfrastructure
{
    public interface IView : IDisposable
    {
        void Construct();
        void DisposeView();
    }
}