using Windows.LevelStateWindow;
using Windows.LevelStateWindow.Common;
using Windows.PersistentWindow.Common;
using Windows.SimpleWindow;
using Core.Gameplay.Levels;
using Core.WindowSystem.Settings;
using Gameplay.CameraFollower;
using Gameplay.Core;

namespace Constants
{
    public static class ResourceNames
    {
        public static ResourceInfo Levels = new(typeof(LevelConfiguration), "Configurations/LevelConfigurations");
        public static ResourceInfo WindowSystemSettings = new(typeof(WindowSystemSettings), "Configurations/WindowSystemSettings");
        public static ResourceInfo SimpleWindowView = new(typeof(SimpleWindowView), "Prefabs/Windows/SimpleWindow");
        public static ResourceInfo PlayerKnife = new(typeof(PlayerKnife), "Prefabs/Player/Knife");
        public static ResourceInfo PlayerCamera = new(typeof(VirtualCameraFollower), "Prefabs/Player/CameraFollower");
        public static ResourceInfo VictoryWindowView = new(typeof(LevelEndView), "Prefabs/Windows/GameState/VictoryWindow");
        public static ResourceInfo FailWindowView = new(typeof(LevelEndView), "Prefabs/Windows/GameState/FailWindow");
        public static ResourceInfo LevelStateWindowView = new(typeof(LevelStateWindowView), "Prefabs/Windows/LevelStateWindow");
        public static ResourceInfo PersistentWindowView = new(typeof(PersistentWindowView), "Prefabs/Windows/PersistentWindow");
    }
}