using Gameplay;

namespace Payloads
{
    public class SetupGameplayPayload : ICommandPayload
    {
        public SpawnPoint SpawnPoint;
        public SetupGameplayPayload(SpawnPoint spawnPoint)
        {
            SpawnPoint = spawnPoint;
        }
    }
}