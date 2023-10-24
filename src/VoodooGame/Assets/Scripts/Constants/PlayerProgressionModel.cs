namespace Constants
{
    [System.Serializable]
    public class PlayerProgressionModel
    {
        public int ResourcesCount;
        public int LastLevelIndex;
        public string[] GiftSlots;
        
        public PlayerProgressionModel(int resourcesCount, int lastLevelIndex, string[] giftSlots)
        {
            ResourcesCount = resourcesCount;
            LastLevelIndex = lastLevelIndex;
            GiftSlots = giftSlots;
        }

        public PlayerProgressionModel()
        {
            
        }
    }
}