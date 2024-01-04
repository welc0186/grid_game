using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerPaddle : MonoBehaviour
{
    public const float SPEED = 30.0f;
    private Rigidbody2D _rigidBody;
    
    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 velocity = _rigidBody.velocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
        Vector2 direction = new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical")
        );
		if (direction != Vector2.zero)
		{
			velocity.x = direction.x * SPEED;
		}
		else
		{
			velocity.x = Mathf.MoveTowards(velocity.x, 0, SPEED);
		}

		_rigidBody.velocity = velocity;
		// MoveAndSlide();
    }
}
