using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 도로시 이동 구현
public class PlayerMovement : MonoBehaviour
{
    public Vector2 movePosition;      // 이동 위치
    public float speed = 2.5f;        // 이동 속도
    public float jumpForce = 300.0f;  // 점프 속도

    public int jumpCount = 0;      // 누적 점프 횟수
    public int possibleJump = 2;   // 점프 가능 횟수
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
        // 점프키를 누르고 점프를 두 번 하지 않았다면
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
