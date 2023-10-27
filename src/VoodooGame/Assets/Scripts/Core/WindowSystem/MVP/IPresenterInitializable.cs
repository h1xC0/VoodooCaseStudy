namespace Core.WindowSystem.MVP
{
    public interface IPresenterInitializable<TViewContract, TModelContract> 
        where TViewContract : IView
        where TModelContract : IModel
    {
        void Initialize(TViewContract viewContract, TModelContract modelContract);
    }
}