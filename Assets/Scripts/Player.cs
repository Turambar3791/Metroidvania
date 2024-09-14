using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Player : MonoBehaviour
{
    private float horizontal;
    private bool isFacingRight = true;

    private float speed = 6f;

    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private float jumpingPower = 14.5f;

    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    //knockBack
    private float KnockbackForce = 15f;
    public float KnockbackCounter;
    public float KnockbackTotalTime = 0.1f;
    public float ImmunityCounter;
    public float ImmunityTotalTime = 1.5f;
    public bool KnockFromRight;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");

        if (KnockbackCounter <= 0)
        {
            rigidBody.velocity = new Vector2(horizontal * speed, rigidBody.velocity.y);
        } else {
            if(KnockFromRight)
            {
                rigidBody.velocity = new Vector2(-KnockbackForce, 3f);
            } else {
                rigidBody.velocity = new Vector2(KnockbackForce, 3f);
            }

            KnockbackCounter -= Time.deltaTime;
        }
        if (ImmunityCounter <= 0)
        {

        } else {
            ImmunityCounter -= Time.deltaTime;
        }
            

        if (IsGrounded()) {
            coyoteTimeCounter = coyoteTime;
        } else {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
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

        Flip();
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
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
