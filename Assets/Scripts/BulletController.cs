using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 10f; // The speed of the bullet
    public float lifeTime = 3f; // The lifetime of the bullet
    public int damage = -1; // The damage of the bullet

    private GameObject player; // Reference to the player GameObject

    // Start is called before the first frame update
    void Start()
    {
        // Destroy the bullet after the lifetime
        Destroy(gameObject, lifeTime);

        // Find the player GameObject
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            //shoot player last position
            Vector3 playerPosition = player.transform.position;
            Vector3 direction = playerPosition - transform.position;
            transform.up = direction;

            // Move the bullet
            GetComponent<Rigidbody2D>().velocity = transform.up * speed;
        }
        else
        {
            Debug.LogError("Player not found!");
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.UpdateHealth(damage);
                playerMovement.Knockback(30f, transform.position);
                Destroy(gameObject);
            }
        }
    }
}