using System;
using System.Collections;
using player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace scene
{
    public class StageManager : MonoBehaviour, IDataPersistence
    {
        public GameObject[] collectables;
        public int currentStage;
        private bool _isThisStageCleared;

        private void Update()
        {
            foreach (var collectable in collectables)
            {
                if (collectable.activeSelf) return;
            } // if all collectables are collected

            if (_isThisStageCleared) return;
            _isThisStageCleared = true;
            StartCoroutine(GoToNext());
        }

        private IEnumerator GoToNext()
        {
            Debug.Log("YOU Beat this stage! " + currentStage);
            yield return new WaitForSeconds(4f);
            SceneManager.LoadScene("Stage_0" + (currentStage + 1));
        }

        public void LoadData(PlayerData data)
        {

        }

        public void SaveData(PlayerData data)
        {
            if (_isThisStageCleared)
            {
                data.IsClear[currentStage] = true;
            }
        }
    }
}