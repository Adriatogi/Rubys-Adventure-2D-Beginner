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

    private int direction = 1;

    private Rigidbody2D rigidBody2D;
    private Animator animator;

    private bool broken = true;

    private void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        StartCoroutine(DirectionRoutine());
    }

    private void Update()
    {
        if (!broken) return;

        if (vertical)
        {
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }
    }

    private void FixedUpdate()
    {
        if (!broken) return;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        RubyController player = collision.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    public void Fix()
    {
        broken = false;
        rigidBody2D.simulated = false;
        animator.SetTrigger("Fixed");
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
