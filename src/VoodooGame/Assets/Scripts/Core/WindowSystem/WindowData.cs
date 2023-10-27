using Core.WindowSystem.Layers;

namespace Core.WindowSystem
{
    public class WindowData : IWindowData
    {
        public int Id { get; set; }
        public Layer Layer { get; set; }

        public static IWindowData Create(int id, Layer layer)
        {
            var data = new WindowData
            {
                Id = id,
                Layer = layer
            };

            return data;
        }

        public override string ToString()
        {
            return $"(ID: {Id}) ({Layer})";
        }
    }
}