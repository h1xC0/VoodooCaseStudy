using System;

namespace Services.EventBus
{
    public interface IEventBusService : IDisposable
    {
        void Subscribe(ISubscriber subscriber);
        void Unsubscribe(ISubscriber subscriber);
        void RaiseEvent<TSubscriber>(Action<TSubscriber> action)
            where TSubscriber : class, ISubscriber;
    }
}