using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ChangeCharacter;
using UnityEngine.TextCore.Text;

// 사자 이동 구현
// 대쉬
public class LionMovement : MonoBehaviour
{
    public Vector2 movePosition;
    public float lookdir = 1.0f;
    public float speed = 2.5f;
    public float jumpForce = 1000.0f;
    public int jumpCount = 0;

    public bool isDash = true;
    public float startToDash = 0.0f;    // 돌격 시작 시간
    public float timeToDash = 5.0f;    // 돌격하는 시간
    public float lastToDash = 0.0f;    // 마지막 돌격 시간
    public float dashToLimit = 5.0f;  // 돌격 쿨타임
    public float dashForce = 1000.0f;  // 돌격 힘

    private Rigidbody2D lionRigidbody;
    private Collider2D lionCollider;
    private ChangeCharacter character;

    void Start()
    {
        lionRigidbody = GetComponent<Rigidbody2D>();
        lionCollider = GetComponent<Collider2D>();
        character = transform.parent.gameObject.GetComponent<ChangeCharacter>();

        lastToDash = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (lastToDash + dashToLimit < Time.time)
        {
            Dash();
        }
        //Dash();

        Move();
        Jump();
    }

    public void Move()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            lookdir = 1.0f;
            movePosition = Vector2.right * speed * Time.deltaTime;
            transform.Translate(movePosition);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            lookdir = -1.0f;
            movePosition = Vector2.left * speed * Time.deltaTime;
            transform.Translate(movePosition);
        }
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 1)
        {
            jumpCount++;

            lionRigidbody.velocity = Vector2.zero;
            lionRigidbody.AddForce(new Vector2(0, jumpForce));
        }
        else if (lionRigidbody.velocity.y > 0)
        {
            lionRigidbody.velocity = lionRigidbody.velocity * 0.5f;
        }
    }

    // 돌격 스킬
    public void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            startToDash = Time.time;

            lionRigidbody.velocity = Vector2.zero;
            lionRigidbody.AddForce(new Vector2(dashForce * lookdir, 0));
        }
        else if (startToDash + timeToDash < Time.time)
        {
            lionRigidbody.AddForce(new Vector2(0, 0));
            lastToDash = Time.time;
        }
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
