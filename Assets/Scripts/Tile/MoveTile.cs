using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTile : MonoBehaviour
{
    public float speed = 2.5f;  // Ÿ�� �̵� �ӵ�
    public Vector2 startPosition;  // ���� ��ġ
    public Vector2 moveDirection = Vector2.right;     // �̵� ����

    private void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        //if (transform.position.y >= maxY)
        //{
        //    moveDirection = -moveDirection;
        //}
        //else if(transform.position.y <= -maxY)
        //{
        //    moveDirection = -moveDirection;
        //}
        //transform.Translate(moveDirection * speed * Time.deltaTime);

        if(transform.position.x >= startPosition.x + 1.5f)
        {
            moveDirection = -moveDirection;
        }
        else if(transform.position.x <= startPosition.x - 1.5f)
        {
            moveDirection = -moveDirection;
        }
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }
}
