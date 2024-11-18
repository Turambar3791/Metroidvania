using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int maxHealth = 5;
    public int health;

    private float horizontal;
    private bool isFacingRight = true;

    private float speed = 6f;

    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private SpriteRenderer sprite;

    private float jumpingPower = 14.5f;

    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    //knockBack
    private float KnockbackForce = 15f;
    public float KnockbackCounter;
    public float KnockbackTotalTime = 0.1f;
    public bool KnockFromRight;

    //immunity
    public float ImmunityCounter;
    public float ImmunityTotalTime = 2f;

    //death
    public float DeathCounter;
    public float DeathTotalTime = 2f;

    public Vector2 spawnPoint;
    

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

        // poruszanie sie
        horizontal = Input.GetAxis("Horizontal");

        if (KnockbackCounter <= 0 && health > 0)
        {
            rigidBody.velocity = new Vector2(horizontal * speed, rigidBody.velocity.y);
        }

        // knock back
        if (KnockbackCounter > 0)
        {
            if (KnockFromRight)
            {
                rigidBody.velocity = new Vector2(-KnockbackForce, 3f);
            }
            else
            {
                rigidBody.velocity = new Vector2(KnockbackForce, 3f);
            }

            KnockbackCounter -= Time.deltaTime;
        } else
        {
            if(health <= 0)
            {
                rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            }
        }
        

        // immunity
        if (ImmunityCounter <= 0)
        {
        } else {
            ImmunityCounter -= Time.deltaTime;
        }

        if (DeathCounter <= 0)
        {
            if(health <= 0)
            {
                rigidBody.transform.position = spawnPoint;
                KnockbackCounter = 0;
                health = 5;
            }
        }
        else
        {
            DeathCounter -= Time.deltaTime;
        }

        if (IsGrounded()) {
            coyoteTimeCounter = coyoteTime;
        } else {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && health>0) {
            jumpBufferCounter = jumpBufferTime;
        } else {
            jumpBufferCounter -= Time.deltaTime;
        }

        //skakanie
        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f) {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpingPower);
        }

        if (Input.GetKeyUp(KeyCode.Space) && rigidBody.velocity.y > 0f) {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y * 0.5f);

            coyoteTimeCounter = 0f;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            if(DeathCounter <= 0) DeathCounter = DeathTotalTime;
        }
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
