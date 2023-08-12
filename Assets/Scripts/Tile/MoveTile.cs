using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTile : MonoBehaviour
{
    public float speed = 2.5f;  // Ÿ�� �̵� �ӵ�
    public float maxY = 5.0f;  // Y�� �̵� �ִ� ��ġ
    public Vector2 moveDirection = Vector2.up;     // �̵� ����

    void Update()
    {
        if (transform.position.y >= maxY)
        {
            moveDirection = -moveDirection;
        }
        else if(transform.position.y <= -maxY)
        {
            moveDirection = -moveDirection;
        }
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }
}
