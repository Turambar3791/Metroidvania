using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementKrecik : MonoBehaviour
{
    public GameObject player;
    private float speed = -2f;
    private bool IsFacingRight = false;
    private PlayerMovement playerMovement;
    private PlayerHealth playerHealth;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask groundLayer;
    private GameObject krecikAreaAttack;
    private Rigidbody2D rb;

    private int damage = 1;
    private int distanceFromPlayer = 8;
    public bool attacking = false;

    private bool enableAttack = true;
    public bool enableAttackPause = true;

    public float timeToAttack = 0.15f;
    private float attackTimer = 0f;

    private float attackBlockerCounter;
    private float attackBlockerTotalTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        krecikAreaAttack = GameObject.Find("KrecikAttackArea").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 ls = transform.localScale;
        float distance = (transform.position - player.transform.position).magnitude;
        Vector3 direction = player.transform.position - transform.position;
        
        if (direction.x < 0) ls.x = Mathf.Abs(ls.x);
        else ls.x = Mathf.Abs(ls.x) * -1;
        
        transform.localScale = ls;
        
        if (distance > distanceFromPlayer)
        {
            krecikAreaAttack.SetActive(false);
            if (player.transform.position.x > transform.position.x)
            {
                transform.Translate(Vector2.right * (-speed * Time.deltaTime));
            } else if (player.transform.position.x < transform.position.x)
            {
                transform.Translate(Vector2.right * (speed * Time.deltaTime));
            }   
            
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
        } else if (distance < distanceFromPlayer)
        {
            if (enableAttack && enableAttackPause)
            {
                attacking = true;
                krecikAreaAttack.SetActive(attacking);
            }

            if (attackBlockerCounter > 0)
            {
                attackBlockerCounter -= Time.deltaTime;

                if (attackBlockerCounter < 0)
                {
                    enableAttack = true;
                }
            }

            if (attacking)
            {
                attackTimer += Time.deltaTime;

                if (attackTimer >= timeToAttack)
                {
                    attackTimer = 0;
                    attacking = false;

                    krecikAreaAttack.SetActive(attacking);

                    enableAttack = false;
                    attackBlockerCounter = attackBlockerTotalTime;
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (playerHealth.ImmunityCounter <= 0)
        {
            if (collider.CompareTag("Player"))
            {
                playerHealth.TakeDamage(damage);
                if (collider.transform.position.x <= transform.position.x)
                {
                    playerMovement.KnockFromRight = true;
                }
                else
                {
                    playerMovement.KnockFromRight = false;
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

    private void Attack()
    {
        attacking = true;
    }
}
