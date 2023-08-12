using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ ��� -> ����ƺ� ���� �� ����
public class LightTile : MonoBehaviour
{
    public float possibleMass = 0.5f;
    public float speed = 2.5f;

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.collider.CompareTag("Player") && coll.rigidbody.mass > 0.5)
        {
            // ����ƺ� �ƴ� ĳ���Ͱ� ���� �� ������Ʈ �ı�
            Destroy(gameObject);
        }
    }
}
