using Services.Transitions;
using Systems.CommandSystem;
using Systems.CommandSystem.Payloads;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Commands
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

            _sceneTransitionService.FadeOut();

            var scene = payload as SceneNamePayload;
            if (scene is null)
            {
                Release();
                return;
            }

            var loadSceneOperation = _sceneLoader.LoadSceneAsync(scene.Info.Name, LoadSceneMode.Additive, null, LoadSceneRelationship.Child);
            loadSceneOperation.completed += ReleaseCommand;
        }

        private void ReleaseCommand(AsyncOperation operation)
        {
            operation.completed -= ReleaseCommand;
            Release();
        }
    }
}