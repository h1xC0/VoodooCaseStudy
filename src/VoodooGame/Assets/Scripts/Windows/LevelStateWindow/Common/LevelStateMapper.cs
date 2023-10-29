using System;
using Core.WindowSystem;
using UnityEngine;
using Zenject;

namespace Windows.LevelStateWindow.Common
{
    public class LevelStateMapper : IInitializable
    {
        private readonly IWindowRegistration _windowRegister;
        private readonly ILevelStateWindowFactory _levelStateWindowFactory;

        public LevelStateMapper(IWindowRegistration windowRegister,
            ILevelStateWindowFactory levelStateWindowFactory)
        {
            _windowRegister = windowRegister;
            _levelStateWindowFactory = levelStateWindowFactory;
        }
        
        public void Initialize()
        {
            _windowRegister.Register<LevelStateWindowPresenter>(delegate (Transform parent, Action<IWindowView, IWindowModel> action)
            {
                action.Invoke(_levelStateWindowFactory.CreateLevelStateWindowView(parent), _levelStateWindowFactory.CreateLevelStateWindowModel());
            });
        }
    }
}