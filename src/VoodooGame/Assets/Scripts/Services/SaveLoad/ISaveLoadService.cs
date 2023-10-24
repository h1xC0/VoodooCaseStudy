using System;
using Constants;

namespace Services.SaveLoad
{
    public interface ISaveLoadService : IDisposable
    {
        void Save(PlayerProgressionModel playerProgressionModel);
        PlayerProgressionModel Load();
    }
}