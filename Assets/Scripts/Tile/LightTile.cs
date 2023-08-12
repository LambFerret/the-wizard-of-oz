using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ ��� -> ����ƺ� ���� �� ����
public class LightTile : MonoBehaviour
{
    private SpriteRenderer _sprite;

    public float possibleMass = 0.5f;  // Ÿ�Ͽ� �ö� �� �ִ� ��� ����
    public float fadeSpeed = 0.5f;    // Ÿ�� �ӵ�

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    // ���̵� �ƿ� ȿ��
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
            // ����ƺ� �ƴ� ĳ���Ͱ� ���� �� ������Ʈ �ı�
            FadeOut();
        }
    }
}
