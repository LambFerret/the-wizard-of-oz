using player;
using UnityEngine;

namespace stage
{
    public class Film :MonoBehaviour, IDataPersistence
    {
        public bool isCollected;
        public int filmNumber;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                isCollected = true;
                gameObject.SetActive(false);
                EventManager.Instance.GetFilm();
            }
        }
        public void LoadData(PlayerData data)
        {
            isCollected = data.HasFilm[filmNumber];
            if (isCollected) gameObject.SetActive(false);
        }

        public void SaveData(PlayerData data)
        {
            data.HasFilm[filmNumber] = isCollected;
        }
    }
}