using Constants;

namespace Payloads
{
    public class SceneNamePayload : ICommandPayload
    {
        public SceneInfo SceneInfoLoad;
        public SceneInfo SceneInfoUnload;

        public SceneNamePayload(SceneInfo sceneInfoLoad, SceneInfo sceneInfoUnload)
        {
            SceneInfoLoad = sceneInfoLoad;
            SceneInfoUnload = sceneInfoUnload;
        }
    }
}