using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTile : MonoBehaviour
{
    public float speed = 2.5f;  // 타일 이동 속도
    public float maxY = 5.0f;  // Y축 이동 최대 위치
    public Vector2 moveDirection = Vector2.right;     // 이동 방향

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

        if(transform.position.x >= transform.position.x + 2.0f)
        {
            moveDirection = -moveDirection;
        }
        else if(transform.position.x <= transform.position.x - 2.0f)
        {
            moveDirection = -moveDirection;
        }
        transform.Translate(moveDirection * speed);
    }
}
