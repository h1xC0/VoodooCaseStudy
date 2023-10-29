using Core.WindowSystem;
using Core.WindowSystem.MVP;

namespace Windows.PersistentWindow.Common
{
    public class PersistentWindowModel : WindowModel, IPersistentWindowModel
    {
        public PersistentWindowModel()
        {
            State.Value = ViewState.Open;
        }
    }
}