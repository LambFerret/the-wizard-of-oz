using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace scene
{
    public class StageManager : MonoBehaviour
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
    }
}