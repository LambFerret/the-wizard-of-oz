using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static ChangeCharacter;

// 도로시 이동 구현
// 이단 점프
public class PlayerMovement : MonoBehaviour
{
    public Vector2 movePosition;      // 이동 위치
    public float speed = 2.5f;        // 이동 속도
    public float jumpForce = 300.0f;  // 점프 속도

    public int jumpCount = 0;      // 누적 점프 횟수
    public int possibleJump = 2;   // 점프 가능 횟수
    public bool isGround = false;
    bool hasChanged;
    private Rigidbody2D playerRigidbody;
    private ChangeCharacter character;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        character = transform.parent.gameObject.GetComponent<ChangeCharacter>();
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
            hasChanged = false;
            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
        }
        else if(playerRigidbody.velocity.y > 0)
        {
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("Ground") || coll.collider.CompareTag("Tile"))
        {
            jumpCount = 0;
            if (character.currentState == character.nextState) return;
            hasChanged = true;
            character.transform.GetChild((int)character.currentState).gameObject.SetActive(false);
            character.transform.GetChild((int)character.nextState).gameObject.SetActive(true);

            character.currentState = character.nextState;
            if (character.currentState == characterState.LION)
                character.nextState = characterState.DOROTHY;
            else
                character.nextState = character.currentState + 1;
        }
    }
}
