using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float movementForce = 20f;   //Magnitude of the force of the Player
    [SerializeField]
    float maxSpeed = 3f;
    Vector2 movementDirection;
    Rigidbody2D rigidbody;      //Player Rigidbody Component

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody2D>();
    }

    void Update() {
        // Get the inputs from the controller / keyboard.
        movementDirection.x = Input.GetAxisRaw("Horizontal");
        movementDirection.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        //Apply force to the player
        rigidbody.AddForce(movementDirection * movementForce);

        // Cap the players velocity at the max speed.
        if(rigidbody.velocity.magnitude > maxSpeed)
         {
            rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;
         }
    }
}
