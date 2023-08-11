using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyTile : MonoBehaviour
{
    public float possibleMass = 2.0f;
    public float speed = 2.5f;

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("Player") && coll.rigidbody.mass >= possibleMass)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
    }
}
