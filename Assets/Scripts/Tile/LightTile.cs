using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 가벼운 블록 -> 허수아비만 밟을 수 있음
public class LightTile : MonoBehaviour
{
    private SpriteRenderer _sprite;

    public float possibleMass = 0.5f;  // 타일에 올라갈 수 있는 허용 질량
    public float fadeSpeed = 0.5f;    // 타일 속도

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    // 페이드 아웃 효과
    void FadeOut()
    {

        _sprite.DOFade(0, fadeSpeed).OnComplete(() =>
        {
            Destroy(gameObject);

        });
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player") && coll.GetComponent<Rigidbody2D>().mass >  0.5)
        {
            // 허수아비가 아닌 캐릭터가 밟을 시 오브젝트 파괴
            FadeOut();
        }
    }
}
