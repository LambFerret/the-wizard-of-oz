using System.Collections;
using System.Collections.Generic;
using character;
using UnityEngine;
using static Character;
using UnityEngine.TextCore.Text;

// ���� �̵� ����
// �뽬
public class LionMovement : IAbility
{
    public float lookdir = 1.0f;
    public bool isDash = true;

    public float startToDash = 0.0f;    // ���� ���� �ð�
    public float timeToDash = 5.0f;    // �����ϴ� �ð�
    public float lastToDash = 0.0f;    // ������ ���� �ð�
    public float dashToLimit = 5.0f;  // ���� ��Ÿ��
    public float dashForce = 1000.0f;  // ���� ��

    public string Name { get; }

    public LionMovement()
    {
        Name = CharacterState.LION.ToString();
    }

    public void Action(Character character)
    {
        Rigidbody2D rb = character.GetComponent<Rigidbody2D>();
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            startToDash = Time.time;

            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(dashForce * lookdir, 0));
        }
        else if (startToDash + timeToDash < Time.time)
        {
            rb.AddForce(new Vector2(0, 0));
            lastToDash = Time.time;
        }
    }

    public void Init(Character character)
    {

    }
}
