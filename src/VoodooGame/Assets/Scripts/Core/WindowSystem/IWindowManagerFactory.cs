using Core.WindowSystem.Blockers;
using Core.WindowSystem.MVP;
using GP.Framework.WindowSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Core.WindowSystem
{
    public interface IWindowManagerFactory
    {
        Canvas GetMainCanvas();
        
        IBlocker GetBackgroundBlocker();
        
        GameObject GetForegroundBlocker();

        TPresenter CreateWindowPresenter<TPresenter>(IWindowView windowView, IWindowModel windowModel, IWindowParameters parameters)
            where TPresenter : class, IPresenter, new();
        Button CreateOutsideAreaButton(IWindowView windowView);
    }
}