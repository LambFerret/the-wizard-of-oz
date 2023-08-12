using System;
using player;
using UnityEngine;

namespace stage
{
    public class Projectile : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                EventManager.Instance.HitByEnemy(other.gameObject.GetComponent<Character>().currentState);
                Destroy(this);
            }
        }
    }
}