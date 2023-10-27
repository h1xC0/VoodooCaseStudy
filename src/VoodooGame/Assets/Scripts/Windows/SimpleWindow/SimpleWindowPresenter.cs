using Core.WindowSystem.MVP;

namespace Windows
{
    public class SimpleWindowPresenter : Presenter<SimpleWindowView, SimpleWindowModel>
    {
        public SimpleWindowPresenter(SimpleWindowView viewContract, SimpleWindowModel modelContract) : base(viewContract, modelContract)
        {
            
        }
    }
}