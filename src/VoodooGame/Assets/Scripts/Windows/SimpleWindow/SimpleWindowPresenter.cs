using Core.WindowSystem;
using Core.WindowSystem.Layers;

namespace Windows.SimpleWindow
{
    [ViewLayer(ViewLayer.Screen)]
    public class SimpleWindowPresenter : WindowPresenter<SimpleWindowView, SimpleWindowModel>
    {
        public SimpleWindowPresenter(SimpleWindowView viewContract, SimpleWindowModel modelContract) : base(viewContract, modelContract)
        {
        }
    }
}