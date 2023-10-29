using Core.WindowSystem;

namespace Windows.LevelStateWindow.Common
{
    public interface ILevelStateView : IWindowView
    {
        ILevelEndView GetVictoryView(LevelEndView victoryView);
        ILevelEndView GetFailView(LevelEndView failView);
    }
}