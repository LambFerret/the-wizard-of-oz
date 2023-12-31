using System;
using player;
using UnityEngine;
using DG.Tweening;

namespace scene
{
    public class CharacterUI : MonoBehaviour, IDataPersistence
    {
        public GameObject[] friends;
        public Character.CharacterState whoIsAlive;
        public float xMoveValue = 1f;
        public float moveDuration = 0.5f;
        private int _friendNumber;
        private float _originalXPositions;

        private void Start()
        {
            _originalXPositions = friends[0].transform.position.x;
            for (int i = 0; i < friends.Length; i++)
            {
                friends[i].SetActive(i < _friendNumber);
            }

            EventManager.Instance.OnChangeCharacter += ChangeFriend;
        }

        private void OnDestroy()
        {
            EventManager.Instance.OnChangeCharacter -= ChangeFriend;
        }

        private void ChangeFriend(Character.CharacterState chara)
        {
            if (whoIsAlive == chara) return;
            whoIsAlive = chara;

            for (int i = 0; i < friends.Length; i++)
            {
                var friend = friends[i];
                var characterSingleUI = friend.gameObject.GetComponent<CharacterSingleUI>();

                if (chara == characterSingleUI.whoAreYou)
                {
                    float targetX = _originalXPositions + xMoveValue;
                    friend.transform.DOMoveX(targetX, moveDuration).SetEase(Ease.InOutQuad);
                }
                else
                {
                    float targetX = _originalXPositions;
                    friend.transform.DOMoveX(targetX, moveDuration).SetEase(Ease.InOutQuad);
                }
            }
        }


        public void LoadData(PlayerData data)
        {
            int a = 0;
            foreach (var b in data.IsClear)
            {
                if (b) a++;
            }
            _friendNumber = a + 1;
        }

        public void SaveData(PlayerData data)
        {
            // no save
        }
    }
}