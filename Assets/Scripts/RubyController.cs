using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    public int maxHealth { get; private set; } = 5;

    public int health { get { return currentHealth; } }
    private int currentHealth;

    Rigidbody2D rigidBody2D;
    float horizontal;
    float vertical;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();

        currentHealth = 1;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
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
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }
}
