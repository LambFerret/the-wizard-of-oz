using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyTile : MonoBehaviour
{
    public float possibleMass = 2.0f;
    public float speed = 3.5f;

    private void OnCollisionEnter2D(Collision2D coll)
    {
        // 충돌체가 Player이며 질량이 2 이상이면 아래로 이동 
        if (coll.collider.CompareTag("Player") && coll.rigidbody.mass >= possibleMass)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
    }
}
