using Core.WindowSystem;
using Core.WindowSystem.Layers;
using Core.WindowSystem.MVP;
using UniRx;

namespace Windows.SimpleWindow
{
    public class SimpleWindowModel : WindowModel
    {
        public ReactiveProperty<ViewState> State =>  
        public Layer Layer { get; set; }
        
        public override void SetParameters(IWindowParameters parameters)
        {
            
        }
        
        public void Dispose()
        {
        }
    }
}