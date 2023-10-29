using Constants;
using Payloads;
using Services.Transitions;
using Systems.CommandSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Signals.SceneLoading
{
    public class LoadSceneCommand : Command
    {
        private readonly ZenjectSceneLoader _sceneLoader;
        private readonly ISceneTransitionService _sceneTransitionService;

        public LoadSceneCommand(ZenjectSceneLoader sceneLoader, ISceneTransitionService sceneTransitionService)
        {
            _sceneLoader = sceneLoader;
            _sceneTransitionService = sceneTransitionService;
        }

        protected override void Execute(ICommandPayload payload)
        {
            Retain();

            var scene = payload as SceneNamePayload;

            if (scene is null)
            {
                Release();
                return;
            }
            
            var loadSceneOperation = _sceneLoader.LoadSceneAsync(scene.SceneInfoLoad.Name, LoadSceneMode.Additive, null, LoadSceneRelationship.Child);
            loadSceneOperation.completed += ReleaseCommand;
        }

        private void ReleaseCommand(AsyncOperation operation)
        {
            operation.completed -= ReleaseCommand;

            _sceneTransitionService.FadeOut();

            Release();
        }
    }
}