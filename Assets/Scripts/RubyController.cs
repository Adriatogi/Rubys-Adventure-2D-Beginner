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

    public int health { get { return currentHealth; } }
    private int currentHealth;

    private bool isInvincible;
    private float invincibleTimer;

    private Rigidbody2D rigidBody2D;
    private float horizontal;
    private float vertical;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
            {
                isInvincible = false;
            }
        }
    }

    private void FixedUpdate()
    { 
        Vector2 position = rigidBody2D.position;
        position.x = position.x + _speed* horizontal * Time.deltaTime;
        position.y = position.y + _speed* vertical * Time.deltaTime;

        rigidBody2D.MovePosition(position);
    }

    public void changeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible) return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

    }
}
