using System;

namespace Services.LevelConfigurationService
{
    public interface ILevelConfigurationService : IDisposable
    {
        public int TotalLevels { get; }
    }
}