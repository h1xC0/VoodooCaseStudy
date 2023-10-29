using Core.WindowSystem;
using Services.ResourceProvider;

namespace Windows.LevelStateWindow.Common
{
    public class LevelStateWindowView : WindowView, ILevelStateView, IResource
    {

        public ILevelEndView GetVictoryView(LevelEndView victoryView)
        {
            return Instantiate(victoryView, transform);
        }

        public ILevelEndView GetFailView(LevelEndView failView)
        {
            return Instantiate(failView, transform);
        }        
    }
}