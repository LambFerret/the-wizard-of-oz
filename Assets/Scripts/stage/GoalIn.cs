using System;
using System.Collections;
using player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace stage
{
    public class GoalIn : MonoBehaviour, IDataPersistence
    {
        public int currentStage;
        private bool _isThisStageCleared;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(GoToNext());
            }
        }
        private IEnumerator GoToNext()
        {
            Debug.Log("YOU Beat this stage! " + currentStage);
            _isThisStageCleared = true;
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