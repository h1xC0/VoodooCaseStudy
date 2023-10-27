using Factories;
using Zenject;

namespace Windows
{
    public class SimpleWindowFactory : AbstractFactory
    {
        public SimpleWindowFactory(DiContainer diContainer) : base(diContainer)
        {
            
        }
    }
}