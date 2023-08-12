using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ -> ���νÿ� ����ƺ� �̿� ����
public class Jumper : MonoBehaviour
{
    private Rigidbody2D _rb;
    public float jumpForce = 9.0f;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();  
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("Player"))
        {
            _rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }
}
