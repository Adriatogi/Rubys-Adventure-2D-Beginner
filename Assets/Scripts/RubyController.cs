using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    public int maxHealth { get; private set; } = 5;
    [SerializeField]
    private float timeInvincible = 2.0f;

    public int health { get; private set; }

    private bool isInvincible;
    private float invincibleTimer;

    private Rigidbody2D rigidBody2D;
    private float horizontal;
    private float vertical;

    private Animator animator;
    private Vector2 lookDirection = new Vector2(1, 0);

    [SerializeField]
    private GameObject projectilePrefab;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        health = maxHealth;
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if(!Mathf.Approximately(move.x, 0.0f)|| !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection = move;
            lookDirection.Normalize();
        }

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
            {
                isInvincible = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidBody2D.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));

            if (hit.collider != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();

                if(character != null)
                {
                    character.DisplayDialog();
                }
            }
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);
    }

    private void FixedUpdate()
    { 
        Vector2 position = rigidBody2D.position;
        position.x += _speed* horizontal * Time.deltaTime;
        position.y += _speed* vertical * Time.deltaTime;

        rigidBody2D.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible) return;

            isInvincible = true;
            invincibleTimer = timeInvincible;

            animator.SetTrigger("Hit");
        }

        health = Mathf.Clamp(health + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(health / (float)maxHealth);
    }

    private void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidBody2D.position + Vector2.up * 0.5f, Quaternion.identity);


        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");
    }
}
