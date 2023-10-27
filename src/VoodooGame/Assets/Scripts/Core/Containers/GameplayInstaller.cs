using System;
using Windows.SimpleWindow;
using Commands;
using Constants;
using Core.WindowSystem;
using Signals;
using Systems.CommandSystem;
using Systems.CommandSystem.Payloads;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;
using Zenject;
using Input = UnityEngine.Input;

namespace Core.Containers
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindInstallers();
        }

        private void Start()
        {
            OpenSimpleWindow();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                var commandDispatcher = Container.Resolve<ICommandDispatcher>();
                    
                commandDispatcher.Dispatch<UnloadSceneSignal>(new SceneNamePayload(SceneNames.Gameplay));
                commandDispatcher.Dispatch<LoadSceneSignal>(new SceneNamePayload(SceneNames.Gameplay));
            }
        }

        private void OpenSimpleWindow()
        {
            Container
                .Resolve<IWindowManager>().Open <SimpleWindowPresenter>();
        }
    
        private void BindInstallers()
        {
            SimpleWindowInstaller.Install(Container);
        }
    }
}
