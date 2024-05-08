using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject projectilePrefab; // The projectile prefab to shoot
    //player
    public GameObject player;
    public float shootInterval = 2f; // Interval between shots
    private float shootTimer = 0f;
    // Start is called before the first frame update
    void Update()
    {
        // Update the shoot timer
        shootTimer += Time.deltaTime;

        //bool to store if player is in range
        bool playerInRange = false;

        // Check if the player is in range
        if (player != null)
        {
            // Calculate the distance between the enemy and the player
            float distance = Vector3.Distance(transform.position, player.transform.position);

            // Check if the player is within range
            if (distance < 10f)
            {
                playerInRange = true;
            }
        }

        // Check if it's time and range to shoot
        if (shootTimer >= shootInterval && playerInRange)
        {
            // Reset the shoot timer
            shootTimer = 0f;

            // Shoot a projectile
            Shoot();
        }
    }

    void Shoot()
    {
        // Instantiate a projectile at the enemy's position and rotation
        Instantiate(projectilePrefab, transform.position, transform.rotation);
    }
}
