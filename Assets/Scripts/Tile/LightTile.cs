using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 가벼운 블록 -> 허수아비만 밟을 수 있음
public class LightTile : MonoBehaviour
{
    public float possibleMass = 0.5f;
    public float speed = 2.5f;

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.collider.CompareTag("Player") && coll.rigidbody.mass > 0.5)
        {
            // 허수아비가 아닌 캐릭터가 밟을 시 오브젝트 파괴
            Destroy(gameObject);
        }
    }
}
