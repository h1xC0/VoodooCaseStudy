using Zenject;

namespace Factories
{
    public class GameFactory : AbstractFactory 
    {
        public GameFactory(DiContainer diContainer) : base(diContainer)
        {
            
        }

        public void CreatePlayerKnife()
        {
            
        }

        public void CreatePlayerCamera()
        {
            // CreateObject<>()
        }
    }
}