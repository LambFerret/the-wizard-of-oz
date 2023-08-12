using System.Collections;
using UnityEngine;

namespace enemy
{
    public class Monkey : Enemy
    {
        public float projectileSpeed;
        public float projectileFiringRate;
        public float projectileDistance;
        public GameObject bananaPrefab;

        private void Start()
        {
            InvokeRepeating(nameof(LaunchBanana), projectileFiringRate, projectileFiringRate);
        }

        private void LaunchBanana()
        {
            GameObject banana = Instantiate(bananaPrefab, transform.position, Quaternion.identity);
            StartCoroutine(MoveBanana(banana, transform.position.x + projectileDistance, projectileSpeed));
        }


        private IEnumerator MoveBanana(GameObject banana, float maxDistance, float speed)
        {
            Vector2 startPosition = banana.transform.position;
            Vector2 endPosition = new Vector2(maxDistance, startPosition.y);

            float journeyLength = Vector2.Distance(startPosition, endPosition);
            float startTime = Time.time;
            float distanceCovered = 0;

            while (distanceCovered < journeyLength)
            {
                distanceCovered = (Time.time - startTime) * speed;
                float fractionOfJourney = distanceCovered / journeyLength;
                banana.transform.position = Vector2.Lerp(startPosition, endPosition, fractionOfJourney);
                yield return null;
            }

            Destroy(banana);
        }

    }
}