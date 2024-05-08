using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jump = 10f;

    [SerializeField] private float jumpTime = 0.4f;
    private float jumpTimeCounter;
    private bool isJumping;

    private float maxHealth = 5f;
    private float currHealth;
    public HPBar bar;
    public float hit = 1f;
    public float attackCooldown = 1f;

    private float maxSpeed = 10f;
    private float maxJump = 20f;
    public bool played = false;

    private enum PlayerState
    {
        idle,
        running,
        jumping,
        falling
    };

    // Start is called before the first frame update
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        //get player attribute from PlayerPrefs
        speed = PlayerPrefs.GetFloat("speed", speed);
        jump = PlayerPrefs.GetFloat("jump", jump);
        currHealth = PlayerPrefs.GetFloat("CurrHealth", maxHealth);

        bar = FindObjectOfType<HPBar>();
        if (bar == null)
        {
            Debug.LogError("bar not found in the scene.");
        }
        Debug.Log("max " + maxHealth);
        bar.SetValue(currHealth, maxHealth);

    }

    // Update is called once per frame
    private void Update()
    {


        float horizontal = Input.GetAxis("Horizontal");
        rigidBody.velocity = new Vector2(horizontal * speed, rigidBody.velocity.y);

        //enable onholding jump

        if (isGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            // rigidBody.velocity = new Vector2(rigidBody.velocity.x, jump);
        }

        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jump);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        //when player press escape, pause game and open the pause menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
            played = true;
        }

        updateAnimation(horizontal);
    }

    private void updateAnimation(float horizontal)
    {

        PlayerState state;

        if (horizontal < 0)
        {
            spriteRenderer.flipX = true;
            state = PlayerState.running;
        }
        else if (horizontal > 0)
        {
            spriteRenderer.flipX = false;
            state = PlayerState.running;
        }
        else
        {
            state = PlayerState.idle;
        }

        if (rigidBody.velocity.y > 0.1f)
        {
            state = PlayerState.jumping;
        }
        else if (rigidBody.velocity.y < -0.1f)
        {
            state = PlayerState.falling;
        }

        animator.SetInteger("state", (int)state);
    }

    public bool isGrounded()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);
    }

    public bool isPlayerJumping()
    {
        return isJumping;
    }

    public void UpdateHealth(float healthChange)
    {
        currHealth += healthChange;
        currHealth = Mathf.Clamp(currHealth, 0, maxHealth);
        PlayerPrefs.SetFloat("CurrHealth", currHealth);
        PlayerPrefs.Save();
        Debug.Log("currHealth" + currHealth);
        bar.SetValue(currHealth, maxHealth);
        if (currHealth == 0)
        {
            GameOver();
        }
    }

    public void TakeDamage(float damage)
    {
        UpdateHealth(-damage);
    }

    public void Knockback(float knockbackForce, Vector3 knockbackDirection)
    {
        rigidBody.velocity = new Vector2(0, 0);
        rigidBody.AddForce(new Vector2(0, knockbackForce), ForceMode2D.Impulse);
        StartCoroutine(FlashRed());
    }

    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    public void UpdateSpeed(float speedChange)
    {
        if (speed + speedChange > maxSpeed)
        {
            speed = maxSpeed;
        }
        else
        {
            speed += speedChange;
        }
        speed += speedChange;
        PlayerPrefs.SetFloat("speed", speed);
        PlayerPrefs.Save();
    }

    public void UpdateJump(float jumpChange)
    {
        if (jump + jumpChange > maxJump)
        {
            jump = maxJump;
        }
        else
        {
            jump += jumpChange;
        }
        jump += jumpChange;
        PlayerPrefs.SetFloat("jump", jump);
        PlayerPrefs.Save();
    }

    public void GameOver()
    {
        SceneManager.LoadScene("Lose");
    }
}
