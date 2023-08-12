using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTile : MonoBehaviour
{
    public float speed = 2.5f;  // 타일 이동 속도
    public Vector2 startPosition;  // 시작 위치
    public Vector2 moveDirection = Vector2.right;     // 이동 방향

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
