using System;
using System.Collections;
using player;
using stage;
using TMPro;
using UnityEngine;

namespace scene
{
    public class CoinCounter : MonoBehaviour, IDataPersistence
    {
        public TextMeshProUGUI text;
        public Coin.CoinType type;
        public int visibleTime;
        public float yDistance;
        public GameObject playerPosition;
        private int _total;
        private int _collected;
        private RectTransform _rectTransform; // Reference to the RectTransform component

        private void Awake()
        {
            text.text = "0 / 0";
        }

        private void Start()
        {
            EventManager.Instance.OnGetCoin += GetCoin;
            _rectTransform = GetComponent<RectTransform>(); // Get the RectTransform component
            text.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            EventManager.Instance.OnGetCoin -= GetCoin;
        }

        private void GetCoin(Coin.CoinType t)
        {
            if (t == type)
            {
                _collected++;
            }

            StartCoroutine(UpdateVisual());
        }

        private IEnumerator UpdateVisual()
        {
            text.gameObject.SetActive(true);
            yield return new WaitForSeconds(visibleTime);
            text.gameObject.SetActive(false);
        }


        private void Update()
        {
            Vector3 worldPosition = playerPosition.transform.position; // Get the player's world-space position
            worldPosition.y += yDistance; // Apply the vertical offset
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition); // Convert to screen-space position
            _rectTransform.position = screenPosition; // Set the UI element's position
            text.text = _collected + " / " + _total;
        }

        public void LoadData(PlayerData data)
        {
            var a = data.IsCoinCollected;
            foreach (var pair in a)
            {
                var b = pair.Key;
                if (b.Contains(type.ToString()))
                {
                    _total++;
                    if (pair.Value)
                    {
                        _collected++;
                    }
                }
            }
        }

        public void SaveData(PlayerData data)
        {
        }
    }
}