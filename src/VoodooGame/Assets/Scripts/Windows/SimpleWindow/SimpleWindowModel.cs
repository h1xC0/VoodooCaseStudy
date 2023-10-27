using Core.WindowSystem;
using Core.WindowSystem.Layers;
using Core.WindowSystem.MVP;
using UniRx;

namespace Windows.SimpleWindow
{
    public class SimpleWindowModel : WindowModel
    {
        public ReactiveProperty<ViewState> State => new ReactiveProperty<ViewState>();
        public Layer Layer { get; set; }

        public override void Dispose()
        {
            
        }
    }
}