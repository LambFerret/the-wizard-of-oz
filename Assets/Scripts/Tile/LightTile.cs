using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 가벼운 블록 -> 허수아비만 밟을 수 있음
public class LightTile : MonoBehaviour
{
    private SpriteRenderer _sprite;

    public float possibleMass = 0.5f;  // 타일에 올라갈 수 있는 허용 질량
    public float speed = 2.5f;    // 타일 속도

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    // 페이드 아웃 효과
    IEnumerable FadeOut()
    {
        for(int i = 10; i >= 0; i--)
        {
            float f = i / 10.0f;
            Color c = _sprite.material.color;
            c.a = f;
            _sprite.material.color = c;

            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.collider.CompareTag("Player") && coll.rigidbody.mass > 0.5)
        {
            // 허수아비가 아닌 캐릭터가 밟을 시 오브젝트 파괴
            StartCoroutine("FadeOut");
            Destroy(gameObject, 1.0f);
        }
    }
}
