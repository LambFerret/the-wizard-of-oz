using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ ��� -> ����ƺ� ���� �� ����
public class LightTile : MonoBehaviour
{
    private SpriteRenderer _sprite;

    public float possibleMass = 0.5f;  // Ÿ�Ͽ� �ö� �� �ִ� ��� ����
    public float speed = 2.5f;    // Ÿ�� �ӵ�

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    // ���̵� �ƿ� ȿ��
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
            // ����ƺ� �ƴ� ĳ���Ͱ� ���� �� ������Ʈ �ı�
            StartCoroutine("FadeOut");
            Destroy(gameObject, 1.0f);
        }
    }
}
