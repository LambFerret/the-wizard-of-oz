using System;
using System.Collections;
using UnityEngine;

namespace enemy
{
    public class AppleTree : Enemy
    {
        public float projectileFiringRate;
        public float fireStopDistance;
        public GameObject applePrefab;
        public LayerMask playerLayer;
        private bool isAppleLaunchingScheduled;

        private bool IsPlayerInFront()
        {
            Vector2 rayOrigin = transform.position;
            Vector2 directionSide = Vector2.right;
            Vector2 rayDirection = directionSide * fireStopDistance;

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection.normalized, rayDirection.magnitude, playerLayer);
            Debug.DrawRay(rayOrigin, rayDirection, Color.red);
            return hit.collider is not null;
        }


        private void Update()
        {
            if (IsPlayerInFront())
            {
                if (isAppleLaunchingScheduled)
                {
                    CancelInvoke(nameof(LaunchApple));
                    isAppleLaunchingScheduled = false;
                }
            }
            else
            {
                if (!isAppleLaunchingScheduled)
                {
                    InvokeRepeating(nameof(LaunchApple), projectileFiringRate, projectileFiringRate);
                    isAppleLaunchingScheduled = true;
                }
            }
        }


        private IEnumerator ParabolaMotion(GameObject apple, Vector2 startPoint, Vector2 endPoint, float height, float duration)
        {
            float progress = 0;
            float startTime = Time.time;

            while (apple is not null && progress < 1)
            {
                float t = (Time.time - startTime) / duration;
                progress = t;

                Vector2 currentPos = Vector2.Lerp(startPoint, endPoint, t);
                currentPos.y += height * Mathf.Sin(Mathf.PI * t);

                apple.transform.position = currentPos;

                yield return null;
            }
            if (apple is not null)
            {
                Destroy(apple);
            }
        }
        private void LaunchApple()
        {
            GameObject apple = Instantiate(applePrefab, transform.position, Quaternion.identity);
            Vector2 endPoint = new Vector2(transform.position.x + fireStopDistance, transform.position.y);
            StartCoroutine(ParabolaMotion(apple, transform.position, endPoint, 5f, 1f));
        }


    }
}