using System;
using Systems.CommandSystem.Payloads;

namespace Systems.CommandSystem
{
    public interface IListener
    {
        void AddListener(Type type, Action<ICommandPayload> action);
        void RemoveListener(Type type);
    }
}