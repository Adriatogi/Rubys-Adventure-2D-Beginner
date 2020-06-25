using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidBody2D;

    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidBody2D.AddForce(direction * force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Projectile Collision with " + collision.gameObject);

        Destroy(gameObject);
    }
}
