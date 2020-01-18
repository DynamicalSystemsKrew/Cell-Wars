using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float movementForce = 20f;   //Magnitude of the force of the Player
    [SerializeField]
    float maxSpeed = 3f;
    [SerializeField]
    float maxAngularVelocity = 3f;
    [SerializeField]
    float rotationSpeed = 3f;
    [SerializeField]
    float wiggleStrength = 0.01f;
    Vector2 inputDirection;
    Rigidbody2D rigidbody;      //Player Rigidbody Component

    int wiggleDirection;
    int wiggleCounter;
    [SerializeField]
    int wiggleFrequency = 20;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody2D>();
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
            rigidbody.AddTorque(rotationDirection * (angleBetween + 0.2f) * rotationSpeed);

            // Add force
            rigidbody.AddForce((1 - angleBetween) * facingDirection * movementForce);
        }

        // Cap the players velocity at the max speed.
        if(rigidbody.velocity.magnitude > maxSpeed)
         {
            rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;
         }

        // Cap the players velocity at the max speed.
        if(Mathf.Abs(rigidbody.angularVelocity) > maxAngularVelocity)
         {
            rigidbody.angularVelocity = Mathf.Clamp(rigidbody.angularVelocity, -maxAngularVelocity, maxAngularVelocity);
         }

        if (wiggleCounter >= wiggleFrequency) {
            wiggleDirection = wiggleDirection * -1;
            wiggleCounter = 0;
        }
        rigidbody.AddTorque((rigidbody.velocity.magnitude / maxSpeed) * wiggleDirection * wiggleStrength);
        wiggleCounter++;
        
    }
}
