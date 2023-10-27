using Windows.SimpleWindow;
using Core.Gameplay.Levels;
using Core.WindowSystem.Settings;

namespace Constants
{
    public static class ResourceNames
    {
        public static ResourceInfo LevelConfiguration = new(typeof(LevelConfiguration), "Prefabs/LevelConfiguration");
        public static ResourceInfo WindowSystemSettings = new(typeof(WindowSystemSettings), "Configurations/WindowSystemSettings");
        public static ResourceInfo SimpleWindowView = new(typeof(SimpleWindowView), "Prefabs/Windows/SimpleWindow");
    }
}