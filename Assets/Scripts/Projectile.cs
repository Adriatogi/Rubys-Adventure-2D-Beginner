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

    private void Update()
    {
        if (transform.position.magnitude > 1000.0f)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyController e = collision.GetComponent<EnemyController>();

        if (e != null)
        {
            e.Fix();
        }

        Destroy(gameObject);
    }
}
