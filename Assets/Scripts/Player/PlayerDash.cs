using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float dashDistance = 5f; // The distance the player will dash
    public float dashDuration = 0.5f; // The duration of the dash

    private bool isDashing = false; // Flag to indicate whether the player is currently dashing
    private Vector3 dashStartPosition; // The starting position of the dash
    private float dashStartTime; // The time when the dash started

    private Rigidbody2D rb; // The Rigidbody2D component attached to the player
    private Transform cameraTarget; // The target for the camera to follow

    void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();

        // Create a new empty GameObject to act as the camera target
        cameraTarget = new GameObject().transform;

        // Set the camera target's position to the player's initial position
        cameraTarget.position = transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isDashing)
        {
            // Start the dash
            isDashing = true;
            dashStartPosition = transform.position;
            dashStartTime = Time.time;

            // Rotate the player to face the mouse position
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.right = mousePosition - transform.position;
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            // Calculate the dash distance and direction
            float dashDistanceRemaining = dashDistance - Vector3.Distance(transform.position, dashStartPosition);
            Vector3 dashDirection = transform.right; // Dash in the direction the player is facing

            // Apply the dash force
            rb.AddForce(dashDirection * dashDistanceRemaining / dashDuration, ForceMode2D.Impulse);

            // End the dash if the duration has passed
            if (Time.time - dashStartTime >= dashDuration)
            {
                isDashing = false;
            }
        }

        // Update the camera target's position to follow the player
        cameraTarget.position = Vector3.Lerp(cameraTarget.position, transform.position, Time.deltaTime * 10f);
    }

    void LateUpdate()
    {
        // Set the camera's position to follow the camera target
        Camera.main.transform.position = new Vector3(cameraTarget.position.x, cameraTarget.position.y, Camera.main.transform.position.z);
    }

    void OnDestroy()
    {
        // Destroy the camera target GameObject when the player is destroyed
        Destroy(cameraTarget.gameObject);
    }
}
