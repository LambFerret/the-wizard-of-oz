using System;
using UnityEngine;
using UnityEngine.UI;

namespace scene
{
    public class CharacterSingleUI : MonoBehaviour
    {
        private Image _healthy;
        private Image _injured;
        private Image _warning;
        private bool _isInjured;

        public Character.CharacterState whoAreYou;


        private void Awake()
        {
            _healthy = transform.Find("Healthy").GetComponent<Image>();
            _injured = transform.Find("Injured").GetComponent<Image>();
            _warning = transform.Find("Warning").GetComponent<Image>();
            SetInjured(false);
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