namespace Core.WindowSystem.Blockers
{
    public class BlockerConfig
    {
        public float AnimationTime { get; set; }
        
        public float Alpha { get; set; }

        public BlockerConfig()
        {
            AnimationTime = 0.1f;
            Alpha = 0.3f;
        }
    }
}