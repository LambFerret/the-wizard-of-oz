using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 양철 나무꾼과 충돌 시 -> 나무 상자 충돌 처리
public class Box : MonoBehaviour
{
    public GameObject fireEffect; 

    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player") && coll.rigidbody.mass > 1.0f)
        {
            Instantiate(fireEffect, transform.position, transform.rotation);
        }

        Destroy(gameObject);
        Destroy(fireEffect, 2.0f);
    }
}
