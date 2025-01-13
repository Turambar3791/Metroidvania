
using System;
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

    public bool isFacingRight = true;

    public PlayerHealth playerHealth;
    public PlayerAttack playerAttack;
    public AttackArea attackArea;
    Animator animator;

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

    private float PogoPower = 9f;
    public float PogoCounter;
    public float PogoTotalTime = 0.2f;

    public bool isGrounded;

    //top transition fly
    private float topFlyPower = 12f;
    public float topFlyCounter;
    private float exitTopPower = 6f;
    public float exitTopCounter;
    private float exitTopSidePower = 6f;
    private float exitTopSideCounter;
    public float exitTopSideDirection;

    public bool enableMovementPause = true;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = transform.Find("waterguy").gameObject.GetComponent<Animator>();
    }

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

        if (KnockbackCounter <= 0 && playerHealth.health > 0 && enableMovementPause)
        {
            if (moveDirection.x > 0)
            {
                rigidBody.velocity = new Vector2(1 * speed, rigidBody.velocity.y);
            }
            else if (moveDirection.x < 0)
            {
                rigidBody.velocity = new Vector2(-1 * speed, rigidBody.velocity.y);
            }
            else
            {
                rigidBody.velocity = new Vector2(0 * speed, rigidBody.velocity.y);
            }
            animator.SetFloat("xVelocity", Math.Abs(rigidBody.velocity.x));
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
        }
        else
        {
            if (playerHealth.health <= 0)
            {
                rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            }
        }

        // coyoteTime
        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
            animator.SetBool("isJumping", !isGrounded);
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (jumpJustPressed && playerHealth.health > 0)
        {
            jumpBufferCounter = jumpBufferTime;
            isGrounded = false;
            animator.SetBool("isJumping", !isGrounded);
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        // skakanie
        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpingPower);
        }
        if (jumpReleased && rigidBody.velocity.y > 0f)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y * 0.5f);

            coyoteTimeCounter = 0f;
        }

        // pogo
        if (attackArea.isPogo)
        {
            PogoCounter = PogoTotalTime;
            attackArea.isPogo  = false;
        }
        if (PogoCounter > 0)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, PogoPower);
            PogoCounter -= Time.deltaTime;
        }

        // top transition fly
        if (topFlyCounter > 0)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, topFlyPower);
            topFlyCounter -= Time.deltaTime;
        }
        if (exitTopCounter > 0)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, exitTopPower);
            exitTopCounter -= Time.deltaTime;
            if (exitTopCounter < 0.1f) exitTopSideCounter = 0.3f;
        }
        if (exitTopSideCounter > 0)
        {
            rigidBody.velocity = new Vector2(exitTopSidePower * exitTopSideDirection, rigidBody.velocity.y);
            exitTopSideCounter -= Time.deltaTime;
        }
    }

    private bool IsGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        return isGrounded;
    }

    public void Flip()
    {
        if (isFacingRight && moveDirection.x < 0f || !isFacingRight && moveDirection.x > 0f && enableMovementPause)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
