using Core.WindowSystem.MVP;

namespace Core.WindowSystem
{
    public interface IWindowManipulation
    {
        void Open();
        // void Open(IWindowParameters parameters);
        void Close();
    }
}