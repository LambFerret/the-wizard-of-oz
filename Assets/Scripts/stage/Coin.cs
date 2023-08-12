using System;
using player;
using Unity.VisualScripting;
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
        public Sprite[] sprites;

        private void Start()
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprites[(int) coinType];
        }

        [ContextMenu("Generate ID")]
        private void GenerateId()
        {
            id = coinType + System.Guid.NewGuid().ToString();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                isCollected = true;
                gameObject.SetActive(false);
                EventManager.Instance.GetCoin(coinType);
            }
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