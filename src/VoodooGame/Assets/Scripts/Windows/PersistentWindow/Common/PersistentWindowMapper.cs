using System;
using Core.WindowSystem;
using UnityEngine;
using Zenject;

namespace Windows.PersistentWindow.Common
{
    public class PersistentWindowMapper : IInitializable
    {
        private readonly IWindowRegistration _windowRegister;
        private readonly IPersistentWindowFactory _persistentWindowFactory;
        
        public PersistentWindowMapper(IWindowRegistration windowRegister,
            IPersistentWindowFactory persistentWindowFactory)
        {
            _windowRegister = windowRegister;
            _persistentWindowFactory = persistentWindowFactory;
        }
        
        public void Initialize()
        {
            _windowRegister.Register<PersistentWindowPresenter>(delegate (Transform parent, Action<IWindowView, IWindowModel> action)
            {
                action.Invoke(_persistentWindowFactory.CreatePersistentWindowView(parent), _persistentWindowFactory.CreatePersistentWindowModel());
            });
        }
    }
}