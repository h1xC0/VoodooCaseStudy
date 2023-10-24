using Systems.Binders;

namespace Systems.CommandSystem
{
    public interface ICommandBinder: IBinder<ICommand>
    {
        ICommandBinding Bind<TSignal>() where TSignal : ISignal;
    }
}