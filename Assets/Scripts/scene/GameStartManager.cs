using System;
using System.Collections;
using player;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace scene
{
    public class GameStartManager : MonoBehaviour, IDataPersistence
    {
        [Header("Config")] public float textSpeed;
        public float fadeSpeed = 1.0f;

        [Header("Game Objects")] public TextMeshProUGUI sentence;
        public GameObject dialogueGameObject;

        private string _fullText;
        private bool _textComplete;

        private void Awake()
        {
            _fullText = sentence.text;
            sentence.text = "";
        }

        private void Start()
        {
            StartCoroutine(ShowText());
        }

        private IEnumerator ShowText()
        {
            for (int i = 0; i < _fullText.Length + 1; i++)
            {
                sentence.text = _fullText.Substring(0, i);
                yield return new WaitForSeconds(textSpeed);
            }

            _textComplete = true;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                DataPersistenceManager.Instance.NewGame();
            }

            if (_textComplete && (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0)))
            {
                StartCoroutine(FadeOutAndLoadScene());
            }
        }

        private IEnumerator FadeOutAndLoadScene()
        {
            DataPersistenceManager.Instance.LoadGame();
            if (!dialogueGameObject.TryGetComponent(out CanvasGroup canvasGroup))
            {
                canvasGroup = dialogueGameObject.AddComponent<CanvasGroup>();
            }

            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= fadeSpeed * Time.deltaTime;
                yield return null;
            }

            try
            {
                SceneManager.LoadScene("Stage_0" + _sceneToClear);
            }
            catch (Exception e)
            {
                Debug.Log("Scene cannot load : " + _sceneToClear);
                SceneManager.LoadScene("Stage_01");
            }
        }

        private int _sceneToClear;

        public void LoadData(PlayerData data)
        {
            for (int i = 0; i < data.IsClear.Length; i++)
            {
                if (!data.IsClear[i])
                {
                    _sceneToClear = i;
                    return;
                }
            }
        }

        public void SaveData(PlayerData data)
        {
        }
    }
}