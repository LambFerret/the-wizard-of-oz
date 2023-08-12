using UnityEngine;

namespace enemy
{
    public class Crow : Enemy
    {
        public float speed;
        public bool direction = true;
        public float groundCheckDistance = 1f;

        private void MoveForward(bool isRight)
        {
            float adjustedSpeed = speed * Time.deltaTime;
            Vector2 moveDirection = isRight ? Vector2.right : Vector2.left;
            Vector2 movement = moveDirection * adjustedSpeed;
            transform.Translate(movement);
        }

        private bool IsGroundEnded()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, platformLayer);

            return hit.collider is null;
        }

        private void Update()
        {
            MoveForward(direction);
            if (IsGroundEnded()) direction = !direction;

        }
    }
}