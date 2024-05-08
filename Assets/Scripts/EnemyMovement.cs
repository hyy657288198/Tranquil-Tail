using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{   
    private SpriteRenderer spriteRenderer; //sprite renderer
    private Animator animator; //animator
    [SerializeField] private Transform[] waypoints; //waypoints
    [SerializeField] private float speed; //speed of enemy
    private int currentWaypointIndex = 0; //current waypoint index

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < 0.1f) {
            //get parameter Death from animator, if it is triggered, stop the enemy
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Death")) {
                speed = 0;
                return;
            }
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length) {
                currentWaypointIndex = 0;
            }
            //when moving to the next waypoint, flip the sprite
            if (waypoints[currentWaypointIndex].transform.position.x < transform.position.x) {
                spriteRenderer.flipX = true;
            } else {
                spriteRenderer.flipX = false;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, speed * Time.deltaTime);
    }
    
}
