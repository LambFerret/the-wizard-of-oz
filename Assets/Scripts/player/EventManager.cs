using System;
using stage;
using UnityEngine;

namespace player
{
    public class EventManager : MonoBehaviour
    {
        public static EventManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("Found more than one Game Events Manager in the scene.");
            }

            Instance = this;
        }

        public event Action<Character.CharacterState> OnHitByEnemy;

        public void HitByEnemy(Character.CharacterState chara)
        {
            OnHitByEnemy?.Invoke(chara);
        }

        public event Action<Character.CharacterState> OnChangeCharacter;

        public void ChangeCharacter(Character.CharacterState chara)
        {
            OnChangeCharacter?.Invoke(chara);
        }

        public event Action<Coin.CoinType> OnGetCoin;

        public void GetCoin(Coin.CoinType type)
        {
            OnGetCoin?.Invoke(type);
        }

        public event Action OnGetFilm;
        public void GetFilm()
        {
            OnGetFilm?.Invoke();
        }
    }
}