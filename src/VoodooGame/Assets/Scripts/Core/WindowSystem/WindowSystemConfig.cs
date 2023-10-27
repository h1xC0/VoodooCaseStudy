using Core.WindowSystem.Blockers;
using UnityEngine;
using UnityEngine.UI;

namespace Core.WindowSystem
{
    public class WindowSystemConfig
    {
        public int Depth { get; set; }
        public float CanvasScaler { get; set; }
        
        public Vector2? ReferenceResolution { get; set; }

        public CanvasScaler.ScreenMatchMode ScreenMatchMode { get; set; }
        
        public BlockerConfig BlockerConfig { get; set; }

        public WindowSystemConfig()
        {
            Depth = -1;
            CanvasScaler = 0;
            ReferenceResolution = null;
            ScreenMatchMode = UnityEngine.UI.CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            BlockerConfig = new BlockerConfig();
        }

    }
}