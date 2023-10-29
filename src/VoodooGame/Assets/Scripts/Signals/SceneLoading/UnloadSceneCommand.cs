using Payloads;
using Services.Transitions;
using Systems.CommandSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Signals.SceneLoading
{
    public class UnloadSceneCommand : Command
    {
        private readonly ISceneTransitionService _sceneTransitionService;

        public UnloadSceneCommand(ISceneTransitionService sceneTransitionService)
        {
            _sceneTransitionService = sceneTransitionService;
        }

        protected override void Execute(ICommandPayload payload)
        {
            Retain();
            
            _sceneTransitionService.FadeIn();
            
            var scene = payload as SceneNamePayload;
            if (scene is null)
            {
                Release();
                return;
            }
            
            var unloadSceneOperation = SceneManager.UnloadSceneAsync(scene.SceneInfoUnload.Name);
            unloadSceneOperation.completed += ReleaseCommand;
            
            Release();
        }
        
        private void ReleaseCommand(AsyncOperation operation)
        {
            operation.completed -= ReleaseCommand;
            Release();
        }
    }
}