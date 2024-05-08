using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    public float fallSpeed = 2f;
    public float activationDelay = 2f; // Delay in seconds before the button falls
    public float fallDistance = 10f; // Distance the button falls

    private bool isPlayerOnButton = false;
    private Vector3 originalPosition;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalPosition = transform.position;
    }

    private void Update()
    {
        if (isPlayerOnButton)
        {
            // Move the button down
            rb.MovePosition(transform.position + Vector3.down * fallSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Start the activation delay coroutine
            Invoke("Fall", activationDelay);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Reset the button if the player leaves before it falls
            CancelInvoke("Fall");
            isPlayerOnButton = false;
        }
    }

    private void Fall()
    {
        isPlayerOnButton = true;
        // Set the rigidbody to dynamic to allow it to fall
        rb.bodyType = RigidbodyType2D.Dynamic;
        // Set the gravity scale to make it fall faster
        rb.gravityScale = fallDistance / activationDelay;
    }
}