using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private float speed = 2f;
    private bool IsFacingRight;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask groundLayer;

    public Player player;

    Rigidbody2D rigidBody;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        if (!IsGrounded() || IsWall())
        {
            if(IsFacingRight)
            {
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
                IsFacingRight = false;
                speed = -speed;
            } else
            {
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
                IsFacingRight = true;
                speed = -speed;
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(player.ImmunityCounter <= 0)
        {
            if (collision.gameObject.tag == "Player")
            {
                player.KnockbackCounter = player.KnockbackTotalTime;
                player.ImmunityCounter = player.ImmunityTotalTime;
                if (collision.transform.position.x <= transform.position.x)
                {
                    player.KnockFromRight = true;
                }
                else
                {
                    player.KnockFromRight = false;
                }
            }
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool IsWall()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, groundLayer);
    }
}
