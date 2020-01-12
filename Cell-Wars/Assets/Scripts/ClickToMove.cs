using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToMove : MonoBehaviour
{
    [SerializeField]
    private float MOVEMENT_SPEED = 5;

    private Vector2 targetPosition;
    private float distanceToTarget = 0;

    void Start()
    {
        targetPosition = transform.position;
    }
    
    void Update()
    {
        // On mouse click, update target position
        if (Input.GetMouseButtonDown(0)) {
            targetPosition = GetMouseToWorldPosition();
        }

        // Calculate distance to target
        distanceToTarget = Vector3.Distance(targetPosition, transform.position);

        //If we are not at the target, move there
        if (distanceToTarget > 0.1) {
            MoveToPosition(targetPosition);
        }
    }

    // Gets the position of the mouse on the screen in world coordinates
    private Vector2 GetMouseToWorldPosition() {
        return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
    }

    // Moves the object to the position at the defined movement speed
    private void MoveToPosition(Vector2 target) {
        Vector2 currentPosition = transform.position;
        var travelDirection = Vector3.Normalize(target - currentPosition);
        transform.Translate(travelDirection * Time.deltaTime * MOVEMENT_SPEED);
    }
}
