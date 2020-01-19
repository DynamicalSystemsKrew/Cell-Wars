using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyMovement : MonoBehaviour
{
    public float movementForce = 20f; // Magnitude of the movement force
    public float rotationForce = 16f;
    public float maxSpeed = 5f;
    public float maxAngularVelocity = 20f;
    public float wiggleStrength = 2f; // Size of the wiggling
    public int wiggleFrequency = 5;

    Vector2 inputDirection;
    Vector2 movementDirection;
    Rigidbody2D rb; // Player Rigidbody Component
    GameObject player;

    int wiggleDirection; // 1 = CW, -1 = CCW
    int wiggleCounter; // Count to decide when to change wiggle direction

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = this.GetComponent<Rigidbody2D>();
        wiggleCounter = 0;
        wiggleDirection = 1;
    }

    void Update() {
        Vector2 playerPosition = player.transform.position;
        // Get the inputs from the controller / keyboard.
        inputDirection.x = Input.GetAxisRaw("Horizontal2");
        inputDirection.y = Input.GetAxisRaw("Vertical2");

        var targetPosition = playerPosition + (inputDirection * 1);

        movementDirection = (targetPosition - new Vector2(transform.position.x, transform.position.y)).normalized;
    }

    void FixedUpdate()
    {
        Vector2 facingDirection = transform.up;

        var angleBetween = Vector2.Angle(facingDirection, movementDirection) / 180;
        var rotationDirection = Mathf.Sign((facingDirection.x * movementDirection.y) - (movementDirection.x * facingDirection.y));

        //Apply force to the player
        if (movementDirection.magnitude > 0) {
            // Rotate player
            rb.AddTorque(rotationDirection * (angleBetween + 0.2f) * rotationForce);

            // Add force
            rb.AddForce((1 - angleBetween) * facingDirection * movementForce * movementDirection.magnitude);
        }

        ClampMovement();
        AddWiggle();
    }

        // Clamps the movement to within the maximum speeds.
    void ClampMovement() {
        // Cap the players velocity at the max speed.
        if(rb.velocity.magnitude > maxSpeed)
         {
            rb.velocity = rb.velocity.normalized * maxSpeed;
         }

        // Cap the players velocity at the max speed.
        if(Mathf.Abs(rb.angularVelocity) > maxAngularVelocity)
         {
            rb.angularVelocity = Mathf.Clamp(rb.angularVelocity, -maxAngularVelocity, maxAngularVelocity);
         }
    }

        // Adds a slight wiggle to the player when moving.
    void AddWiggle() {
        if (wiggleCounter >= wiggleFrequency) {
            wiggleDirection = wiggleDirection * -1;
            wiggleCounter = 0;
        }
        rb.AddTorque((rb.velocity.magnitude / maxSpeed) * wiggleDirection * wiggleStrength);
        wiggleCounter++;
    }
}
