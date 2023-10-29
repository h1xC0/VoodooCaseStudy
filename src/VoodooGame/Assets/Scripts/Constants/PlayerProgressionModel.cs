namespace Constants
{
    [System.Serializable]
    public class PlayerProgressionModel
    {
        public int MoneyCount;
        public int LastLevelIndex;
        
        public PlayerProgressionModel(int moneyCount, int lastLevelIndex)
        {
            MoneyCount = moneyCount;
            LastLevelIndex = lastLevelIndex;
        }

        public PlayerProgressionModel()
        {
            MoneyCount = 0;
            LastLevelIndex = 0;
        }
    }
}