using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 moveDirection;
    public bool jumpJustPressed;
    public bool jumpBeingHeld;
    public bool jumpReleased;
    public bool menuOpenCloseInput;

    private bool isFacingRight = true;

    public PlayerHealth playerHealth;
    public PlayerAttack playerAttack;

    private float speed = 6f;

    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private SpriteRenderer sprite;

    private float jumpingPower = 12.5f;

    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    //knockBack
    private float KnockbackForce = 15f;
    public float KnockbackCounter;
    public float KnockbackTotalTime = 0.1f;
    public bool KnockFromRight;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAttack.attacking == false) 
        {
            Flip();
        }

        // poruszanie sie
        jumpJustPressed = UserInput.instance.JumpJustPressed;
        jumpReleased = UserInput.instance.JumpReleased;
        moveDirection = UserInput.instance.MoveInput;

        if (KnockbackCounter <= 0 && playerHealth.health > 0)
        {
            rigidBody.velocity = new Vector2(moveDirection.x * speed, rigidBody.velocity.y);
        }

        // knockback
        if (KnockbackCounter > 0)
        {
            if (KnockFromRight)
            {
                rigidBody.velocity = new Vector2(-KnockbackForce, 7f);
            }
            else
            {
                rigidBody.velocity = new Vector2(KnockbackForce, 7f);
            }

            KnockbackCounter -= Time.deltaTime;
        } else
        {
            if(playerHealth.health <= 0)
            {
                rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            }
        }
        


        if (IsGrounded()) {
            coyoteTimeCounter = coyoteTime;
        } else {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (jumpJustPressed && playerHealth.health > 0) {
            jumpBufferCounter = jumpBufferTime;
        } else {
            jumpBufferCounter -= Time.deltaTime;
        }

        //skakanie
        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f) {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpingPower);
        }

        if (jumpReleased && rigidBody.velocity.y > 0f) {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y * 0.5f);

            coyoteTimeCounter = 0f;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && moveDirection.x < 0f || !isFacingRight && moveDirection.x > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}