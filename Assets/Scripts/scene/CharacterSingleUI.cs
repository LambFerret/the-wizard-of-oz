using System;
using player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace scene
{
    public class CharacterSingleUI : MonoBehaviour, IDataPersistence
    {
        private Image _healthy;
        private Image _injured;
        private Image _warning;
        private bool _isInjured;

        private int _savedHealth;

        public Character.CharacterState whoAreYou;

        public void LoadData(PlayerData data)
        {
            _savedHealth = data.PlayerHealth[(int) whoAreYou];
            SetInjured(_savedHealth == 1);
        }

        public void SaveData(PlayerData data)
        {
            data.PlayerHealth[(int) whoAreYou] = _isInjured ? 1 : 2;
        }

        private void Awake()
        {
            _healthy = transform.Find("Healthy").GetComponent<Image>();
            _injured = transform.Find("Injured").GetComponent<Image>();
            _warning = transform.Find("Warning").GetComponent<Image>();
            SetInjured(false);
        }

        private void Start()
        {
            EventManager.Instance.OnHitByEnemy += HitByEnemy;
        }

        private void OnDestroy()
        {
            EventManager.Instance.OnHitByEnemy -= HitByEnemy;
        }

        private void HitByEnemy(Character.CharacterState chara)
        {
            if (chara == whoAreYou)
            {
                SetInjured(true);
            }
        }

        private bool _isGameOver;
        private void Update()
        {
            if (_isGameOver) return;
            if (_savedHealth == 0)
            {
                _isGameOver = true;
                Debug.Log("you are dead ");
                SceneManager.LoadScene("StageSelectScene");
            }
        }

        public void SetInjured(bool injured)
        {
            _isInjured = injured;
            _healthy.gameObject.SetActive(!injured);
            _injured.gameObject.SetActive(injured);
            _warning.gameObject.SetActive(injured);
        }
    }
}