namespace Core.WindowSystem.Layers
{
    public interface IViewLayerSettings
    {
        string LayerName { get; }

        bool IsChildOfPreviousLayer { get; }
    }
}