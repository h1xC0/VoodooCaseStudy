using Core.WindowSystem;
using Core.WindowSystem.Layers;
using Core.WindowSystem.MVP;
using UniRx;

namespace Windows.LevelStateWindow.Common
{
    public class LevelStateWindowModel : WindowModel, ILevelStateModel
    {
        public LevelStateWindowModel()
        {
            State.Value = ViewState.Open;
        }
    }
}