using System;
using player;
using UnityEngine;

namespace scene
{
    public class StageManager : MonoBehaviour, IDataPersistence
    {
        public void LoadData(PlayerData data)
        {
            for (int i = 0; i < data.PlayerHealth.Length; i++)
            {
                data.PlayerHealth[i] = 2;
            }
        }

        public void SaveData(PlayerData data)
        {
        }
    }
}