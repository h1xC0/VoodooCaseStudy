using UnityEngine.UI;

namespace Core.WindowSystem.Extensions
{
    public class NonDrawingGraphic : Graphic
    {
        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();
        }
    }
}