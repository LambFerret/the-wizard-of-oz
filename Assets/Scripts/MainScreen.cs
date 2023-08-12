using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScreen : MonoBehaviour
{
    private SpriteRenderer _sprite;
    public float fadeSpeed = 0.5f;

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();   
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);

            FadeOut();
        }
    }

    void FadeOut()
    {
        _sprite.DOFade(0, fadeSpeed).OnComplete(() =>
        {
            Destroy(gameObject);

        });
    }
}
