namespace Core.WindowSystem.Layers
{
    public class Layer
    {
        private readonly IViewLayerSettings _settings;

        public string Name => Settings.LayerName;
        
        /// <summary>
        /// The higher this number, the lower the priority.
        /// </summary>
        public int Order { get; set; }

        public IViewLayerSettings Settings => _settings;
        
        public Layer(IViewLayerSettings layerSettings)
        {
            _settings = layerSettings;
        }

        public override string ToString()
        {
            return $"Layer: {Name}, Order: {Order}";
        }
    }
}