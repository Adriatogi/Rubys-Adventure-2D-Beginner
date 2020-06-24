using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private bool vertical = false;
    [SerializeField]
    private float changeTIme = 3.0f;

    Rigidbody2D rigidBody2D;
    int direction = 1;

    private void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();

        StartCoroutine(DirectionRoutine());
    }

    private void FixedUpdate()
    {
        Vector2 position = rigidBody2D.position;

        if (vertical)
        {
            position.y += Time.deltaTime * _speed * direction;
        }
        else
        {
            position.x += Time.deltaTime * _speed * direction;
        }

        rigidBody2D.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        RubyController player = collision.gameObject.GetComponent<RubyController>();

        if(player != null)
        {
            player.changeHealth(-1);
        }
    }
    IEnumerator DirectionRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(changeTIme);
            direction = -direction;
        }
    }
}
