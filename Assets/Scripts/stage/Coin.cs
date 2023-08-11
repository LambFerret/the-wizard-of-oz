using player;
using UnityEngine;

namespace stage
{
    public class Coin : MonoBehaviour, IDataPersistence
    {
        public enum CoinType
        {
            Brain,
            Heart,
            Brave
        }

        public string id;
        public CoinType coinType;
        public bool isCollected;

        [ContextMenu("Generate ID")]
        private void GenerateId()
        {
            id = coinType + System.Guid.NewGuid().ToString();
        }

        public void LoadData(PlayerData data)
        {
            data.IsCoinCollected.TryGetValue(id, out isCollected);
            if (isCollected) gameObject.SetActive(false);
        }

        public void SaveData(PlayerData data)
        {
            if (data.IsCoinCollected.ContainsKey(id)) return;
            data.IsCoinCollected.Add(id, isCollected);
        }
    }
}