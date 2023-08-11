using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class StageSelectBehavior : MonoBehaviour
{
    public float scrollSpeed;
    public bool isTest;
    public float animationDelay;

    private ScrollRect _scrollRect;
    private GameObject _levels;
    private int _stageNumber;
    private int _currentStage;


    private void Awake()
    {
        _scrollRect = GetComponent<ScrollRect>();
        _levels = transform.Find("Levels").gameObject;
        _stageNumber = _levels.transform.childCount;
        _currentStage = 0;
        // GetComponent<RectTransform>().sizeDelta = new Vector2(_stageNumber * spacing, 0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow)) GoRight();
        if (Input.GetKeyDown(KeyCode.LeftArrow)) GoLeft();
        _currentStage = (int)Math.Round(_scrollRect.horizontalNormalizedPosition * (_stageNumber - 1));
    }

    public void GoRight()
    {
        _currentStage++;
        if (_currentStage >= _stageNumber) _currentStage = _stageNumber - 1;
        ScrollToLevel();
    }

    public void GoLeft()
    {
        _currentStage--;
        if (_currentStage < 0) _currentStage = 0;
        ScrollToLevel();
    }

    public void GoToStage(int stage)
    {
        _currentStage = stage - 1;
        ScrollToLevel();
        StartCoroutine(GoLevel(stage));
    }

    private IEnumerator GoLevel(int stage)
    {
        string sceneName = $"Stage_0{stage}";
        yield return new WaitForSeconds(animationDelay);
        if (isTest)
        {
            Debug.Log($"GO TO LEVEL {sceneName}");
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    private void ScrollToLevel()
    {
        float targetPosition = (float)_currentStage / (_stageNumber - 1);
        DOTween.To(() => _scrollRect.horizontalNormalizedPosition,
            x => _scrollRect.horizontalNormalizedPosition = x,
            targetPosition, scrollSpeed);
    }
}