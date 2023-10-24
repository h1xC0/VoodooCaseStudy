using Constants;

namespace Systems.CommandSystem.Payloads
{
    public class SceneNamePayload : ICommandPayload
    {
        public SceneInfo Info;

        public SceneNamePayload(SceneInfo info)
        {
            Info = info;
        }
    }
}