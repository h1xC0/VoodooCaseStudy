using System;
using Core.Gameplay.Levels;

namespace Services.LevelConfigurationService
{
    public interface ILevelConfigurationService : IDisposable
    {
        public int TotalLevels { get; }

        LevelConfiguration GetLevelConfiguration(int levelIndex);
    }
}