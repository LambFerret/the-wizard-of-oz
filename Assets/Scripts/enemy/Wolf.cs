using System;
using UnityEngine;

namespace enemy
{
    public class Wolf : Enemy
    {
        public float speed;
        public bool direction = true;
        public float groundCheckDistance = 1f;
        private SpriteRenderer _sprite;

        private void Awake()
        {
            _sprite = GetComponent<SpriteRenderer>();
        }

        private void MoveForward(bool isRight)
        {
            float adjustedSpeed = speed * Time.deltaTime;
            Vector2 moveDirection = isRight ? Vector2.right : Vector2.left;
            Vector2 movement = moveDirection * adjustedSpeed;
            transform.Translate(movement);
        }

        private bool IsGroundEnded()
        {
            var rayOrigin = transform.position;
            var origin = new Vector2(rayOrigin.x, rayOrigin.y);
            var rayDirection = (direction ? Vector2.right : Vector2.left) * groundCheckDistance + Vector2.down;
            var distance = (rayDirection - origin).magnitude;
            Debug.DrawRay(rayOrigin, rayDirection, Color.red);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection);
            Debug.Log(hit.collider.tag);
            return hit.collider is null;
        }

        private void Update()
        {
            MoveForward(direction);
            if (IsGroundEnded())
            {
                direction = !direction;
            }
        }
    }
}