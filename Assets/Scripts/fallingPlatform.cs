using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingPlatform : MonoBehaviour
{
    public float fallDelay = 1f;
    private Rigidbody2D rb2d;
    private LayerMask playerLayer;
    private Vector3 originalPosition;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerLayer = LayerMask.GetMask("Player");
        animator = GetComponent<Animator>();
        originalPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Invoke("fall", fallDelay);
        }
    }

    private void fall()
    {
        rb2d.isKinematic = false;
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        //rise up the platform back to its original position after 5 seconds
        Invoke("Respawn", 5f);
    }

    private void Respawn()
    {
        rb2d.velocity = Vector2.zero;
        rb2d.isKinematic = true;
        animator.SetTrigger("respawn");
        transform.position = originalPosition;
    }
}
