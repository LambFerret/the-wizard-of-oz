using UnityEngine;

namespace enemy
{
    public abstract class Enemy : MonoBehaviour
    {
        public LayerMask platformLayer;
        public LayerMask playerLayer;
        public void Die()
        {

        }
    }
}