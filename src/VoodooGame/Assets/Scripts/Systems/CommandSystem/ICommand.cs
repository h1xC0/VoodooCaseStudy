using System;
using Payloads;

namespace Systems.CommandSystem
{
    public interface ICommand : IDisposable
    {
        event Action OnExecuted;
        bool IsRetained { get; }
        void Invoke();
        void Invoke(ICommandPayload payload);
    }
}