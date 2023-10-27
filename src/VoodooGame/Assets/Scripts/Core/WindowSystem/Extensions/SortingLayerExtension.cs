using UnityEngine;

namespace Core.WindowSystem.Extensions
{
    public static class SortingLayerExtension
    {
        public static int GetSortingLayer(this string layer)
        {
            return SortingLayer.GetLayerValueFromName(layer);
        }
    }
}