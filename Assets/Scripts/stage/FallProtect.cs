using UnityEngine;

namespace stage
{
    public class FallProtect : MonoBehaviour
    {
        public GameObject arrival;

        private void Awake()
        {
            arrival = transform.Find("arrival").gameObject;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                other.transform.position = arrival.transform.position;
            }
        }
    }
}