using Constants;

namespace Core.WindowSystem.Blockers
{
    public interface IBlocker
    {
        void SetActive(bool active, string layer = WindowsLayer.Default);

        void ApplyConfig(BlockerConfig blockerConfig);
    }
}