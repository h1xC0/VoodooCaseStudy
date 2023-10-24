using System;

namespace Systems.Binders
{
    public interface IBinding<T> : IDisposable where T : class
    {
        void To<TKey>() where TKey : T;
    }
}