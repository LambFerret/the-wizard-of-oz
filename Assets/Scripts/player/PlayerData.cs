using Script.player;

namespace player
{
    public class PlayerData
    {
        private const int StageNumber = 5;

        public bool[] HasFilm;
        public bool[] IsClear;
        public SerializableDictionary<string, bool> IsCoinCollected;
        public int PlayerHealth;


        public PlayerData()
        {
            IsClear = new bool[StageNumber];
            HasFilm = new bool[StageNumber];
            IsCoinCollected = new SerializableDictionary<string, bool>();
            PlayerHealth = 2;
        }
    }
}