using System;
using Core.WindowSystem.MVP;
using UnityEngine;

namespace Core.WindowSystem
{
    public interface IWindowRegistration
    {
        void Register<T>(Action<Transform, Action<IWindowView, IWindowModel>> createMethod)
            where T : IPresenter;
    }
}