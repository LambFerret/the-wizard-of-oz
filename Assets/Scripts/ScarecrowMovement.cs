using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static ChangeCharacter;
using UnityEngine.TextCore.Text;

// 허수아비 이동 구현
// 은신 기능
public class ScarecrowMovement : MonoBehaviour
{
    public Vector2 movePosition;
    public float speed = 2.5f;
    public float jumpForce = 1000.0f;
    public int jumpCount = 0;   // 점프 횟수

    public bool isStealth = false;       // 은신 상태 확인 
    public float startToStealth = 0.0f;  // 은신 시작 시간
    public float timeToStealth = 2.5f;   // 은신 가능 시간

    private Rigidbody2D scarecrowRigidbody;
    private Collider2D scarecrowCollider;
    private ChangeCharacter character;

    void Start()
    {
        scarecrowRigidbody = GetComponent<Rigidbody2D>();
        scarecrowCollider = GetComponent<Collider2D>();
        character = transform.parent.gameObject.GetComponent<ChangeCharacter>();
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

    // 은신 스킬
    public void Stealth()
    {
        //startToStealth = Time.time;
        //scarecrowCollider.enabled = false;

        //// 은신 가능 시간이 지나면 다시 Enemy의 콜라이더와 충돌 처리
        //if (startToStealth + timeToStealth < Time.time)
        //{
        //    scarecrowCollider.enabled = true;
        //}
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.collider.CompareTag("Ground") || coll.collider.CompareTag("Tile"))
        {
            jumpCount = 0;

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
