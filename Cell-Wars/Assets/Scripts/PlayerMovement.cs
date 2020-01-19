using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementForce = 20f; // Magnitude of the movement force
    public float rotationForce = 16f;
    public float maxSpeed = 5f;
    public float maxAngularVelocity = 20f;
    public float wiggleStrength = 2f; // Size of the wiggling
    public int wiggleFrequency = 5;

    Vector2 inputDirection;
    Rigidbody2D rb; // Player Rigidbody Component

    int wiggleDirection; // 1 = CW, -1 = CCW
    int wiggleCounter; // Count to decide when to change wiggle direction

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        wiggleCounter = 0;
        wiggleDirection = 1;
    }

    void Update() {
        // Get the inputs from the controller / keyboard.
        inputDirection.x = Input.GetAxisRaw("Horizontal");
        inputDirection.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        Vector2 facingDirection = transform.up;

        var angleBetween = Vector2.Angle(facingDirection, inputDirection) / 180;
        var rotationDirection = Mathf.Sign((facingDirection.x * inputDirection.y) - (inputDirection.x * facingDirection.y));

        //Apply force to the player
        if (inputDirection.magnitude > 0) {
            // Rotate player
            rb.AddTorque(rotationDirection * (angleBetween + 0.2f) * rotationForce);

            // Add force
            rb.AddForce((1 - angleBetween) * facingDirection * movementForce);
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
