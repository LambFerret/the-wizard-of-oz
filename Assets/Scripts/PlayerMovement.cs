using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���ν� �̵� ����
public class PlayerMovement : MonoBehaviour
{
    public Vector2 movePosition;      // �̵� ��ġ
    public float speed = 2.5f;        // �̵� �ӵ�
    public float jumpForce = 300.0f;  // ���� �ӵ�

    public int jumpCount = 0;      // ���� ���� Ƚ��
    public int possibleJump = 2;   // ���� ���� Ƚ��
    public bool isGround = false;

    private Rigidbody2D playerRigidbody;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
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
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            movePosition = Vector2.left * speed * Time.deltaTime;
            transform.Translate(movePosition);
        }
    }

    public void Jump()
    {
        // ����Ű�� ������ ������ �� �� ���� �ʾҴٸ�
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < possibleJump)
        {
            jumpCount++;

            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
        }
        //else if(Input.GetKeyUp(KeyCode.Space) && playerRigidbody.velocity.y > 0)
        //{
        //    playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
        //}
        else if(playerRigidbody.velocity.y > 0)
        {
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("Ground"))
        {
            jumpCount = 0;
        }
    }
}
