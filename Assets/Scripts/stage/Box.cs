using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ö �����۰� �浹 �� -> ���� ���� �浹 ó��
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
