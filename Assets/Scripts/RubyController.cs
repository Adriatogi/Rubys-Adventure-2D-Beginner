using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{

    private float _speed = 5.0f;
    Rigidbody2D rigidBody2D;
    float horizontal;
    float vertical;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
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
}
