using System.Collections;
using UnityEngine;

namespace enemy
{
    public class Sheep : Enemy
    {
        public float stampedeDelay;
        public float stampedeSpeed;
        public float speed;
        public bool direction = true;
        public float groundCheckDistance = 1f;
        public float sightRange = 1f;

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

        private bool IsPlayerInFront()
        {
            Vector2 rayOrigin = transform.position;
            Vector2 directionSide = direction ? Vector2.right : Vector2.left;
            Vector2 rayDirection = directionSide * sightRange;

            RaycastHit2D hit =
                Physics2D.Raycast(rayOrigin, rayDirection.normalized, rayDirection.magnitude, playerLayer);
            Debug.DrawRay(rayOrigin, rayDirection, Color.red);
            return hit.collider is not null;
        }

        private IEnumerator Stampede()
        {
            float initSpeed = speed;
            speed = 0;
            yield return new WaitForSeconds(stampedeDelay);
            float adjustedSpeed = stampedeSpeed * Time.deltaTime;
            Vector2 moveDirection = direction ? Vector2.right : Vector2.left;
            Vector2 movement = moveDirection * adjustedSpeed;
            transform.Translate(movement);
            yield return new WaitForSeconds(3);
            speed = initSpeed;
        }

        private void Update()
        {
            MoveForward(direction);
            if (IsGroundEnded()) direction = !direction;
            if (IsPlayerInFront())
            {
                StartCoroutine(Stampede());
            }
        }
    }
}