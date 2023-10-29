using UnityEngine;

namespace Payloads
{
    public class LevelEndStatePayload : ICommandPayload
    {
        public bool LevelState;
        public bool LevelEnded;

        public Transform Parent;

        public LevelEndStatePayload(bool levelState, bool levelEnded)
        {
            LevelState = levelState;
            LevelEnded = levelEnded;
        }
    }
}