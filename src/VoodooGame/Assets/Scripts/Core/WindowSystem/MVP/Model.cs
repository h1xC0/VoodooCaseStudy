using Core.WindowSystem.Layers;
using UniRx;

namespace Core.WindowSystem.MVP
{
    public class Model : IModel
    {
        public ReactiveProperty<ViewState> State { get; } = new ReactiveProperty<ViewState>();
        public Layer Layer { get; set; }

        public virtual void Dispose()
        {
            
        }
    }
}