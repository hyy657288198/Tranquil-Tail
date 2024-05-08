using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private AudioSource soundEffect;
    
    [SerializeField] private LayerMask playerLayer;
    private bool isSoundPlaying = false;
    
    private float attack = -1f;
    private float lastAttackTime;
    private float attackCooldown = 1f;
    private float currHealth;
    private float maxHealth = 2f;
    public AudioClip attackSoundEffect;
    public AudioClip deadSoundEffect;
    public enemyHPbar healthBar;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        soundEffect = GetComponent<AudioSource>();
        currHealth = maxHealth;
        healthBar.updateEnemyHP(currHealth, maxHealth);
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, playerLayer);
        
        if (hit.collider != null && hit.collider.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = hit.collider.gameObject.GetComponent<PlayerMovement>();
            if (player != null && !player.isGrounded())
            {
                float playerLastAttackTime = PlayerPrefs.GetFloat("playerLastAttackTime", 0f);
                if (Time.time - playerLastAttackTime >= player.attackCooldown)
                {
                    currHealth -= player.hit;
                    Debug.Log("hit: " + currHealth);
                    healthBar.updateEnemyHP(currHealth, maxHealth);
                    if (currHealth == 0)
                    {
                        die();
                    }
                    playerLastAttackTime = Time.time;
                    PlayerPrefs.SetFloat("playerLastAttackTime", playerLastAttackTime);
                    PlayerPrefs.Save();
                }
            }
            //when player interact with enemy from left or right, player will be attacked
            else
            {
                if (Time.time - lastAttackTime > attackCooldown)
                {
                    soundEffect.clip = attackSoundEffect;
                    soundEffect.Play(); 
                    Debug.Log("is attacked");
                    player.UpdateHealth(attack);
                    player.Knockback(30f, player.transform.position);
                    lastAttackTime = Time.time;
                }
            }
        }
    }

    private void die()
    {
        animator.SetTrigger("Death");

        if (!isSoundPlaying)
        {
            soundEffect.clip = deadSoundEffect;
            soundEffect.Play();
            isSoundPlaying = true;
        }
        Destroy(gameObject, 1f);
    }
}
