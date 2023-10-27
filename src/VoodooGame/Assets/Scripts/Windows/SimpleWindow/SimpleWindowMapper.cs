using System;
using Core.WindowSystem;
using UnityEngine;
using Zenject;

namespace Windows
{
    public class SimpleWindowMapper : IInitializable
    {
        private readonly IWindowRegistration _windowRegister;
        private readonly ISimpleWindowFactory _simpleWindowFactory;
        
        public SimpleWindowMapper(IWindowRegistration windowRegister,
            ISimpleWindowFactory simpleWindowFactory)
        {
            _windowRegister = windowRegister;
            _simpleWindowFactory = simpleWindowFactory;
        }
        
        public void Initialize()
        {
            _windowRegister.Register<SimpleWindowPresenter>(delegate (Transform parent, Action<IWindowView> action)
            {
                action.Invoke(_simpleWindowFactory.CreateSimpleWindowView(parent));
            });
        }
    }
}