using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// ����ƺ� �̵� ����
public class ScarecrowMovement : MonoBehaviour
{
    public Vector2 movePosition;
    public float speed = 2.5f;
    public float jumpForce = 1000.0f;
    public int jumpCount = 0;   // ���� Ƚ��

    public bool isStealth = false;       // ���� ���� Ȯ�� 
    public float startToStealth = 0.0f;  // ���� ���� �ð�
    public float timeToStealth = 2.5f;   // ���� ���� �ð� (��Ÿ��)

    private Rigidbody2D scarecrowRigidbody;
    private Collider2D scarecrowCollider;

    void Start()
    {
        scarecrowRigidbody = GetComponent<Rigidbody2D>();
        scarecrowCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(movePosition == Vector2.zero)
        {
            Stealth();
        }

        Move();
        Jump();
    }

    public void Move()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            movePosition = Vector2.right * speed * Time.deltaTime;
            transform.Translate(movePosition);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            movePosition = Vector2.left * speed * Time.deltaTime;
            transform.Translate(movePosition);
        }
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 1)
        {
            jumpCount++;

            scarecrowRigidbody.velocity = Vector2.zero;
            scarecrowRigidbody.AddForce(new Vector2(0, jumpForce));
        }
        else if(scarecrowRigidbody.velocity.y > 0)
        {
            scarecrowRigidbody.velocity = scarecrowRigidbody.velocity * 0.5f;
        }
    }

    // ���� ��ų
    public void Stealth()
    {
        startToStealth = Time.time;
        scarecrowCollider.enabled = false;

        // ���� ���� �ð��� ������ �ٽ� Enemy�� �ݶ��̴��� �浹 ó��
        if(startToStealth + timeToStealth < Time.time)
        {
            scarecrowCollider.enabled = true;
        }
    }
}
