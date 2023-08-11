using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static ChangeCharacter;

// ���ν� �̵� ����
// �̴� ����
public class PlayerMovement : MonoBehaviour
{
    public Vector2 movePosition;      // �̵� ��ġ
    public float speed = 2.5f;        // �̵� �ӵ�
    public float jumpForce = 300.0f;  // ���� �ӵ�

    public int jumpCount = 0;      // ���� ���� Ƚ��
    public int possibleJump = 2;   // ���� ���� Ƚ��
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
        // ����Ű�� ������ ������ �� �� ���� �ʾҴٸ�
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
