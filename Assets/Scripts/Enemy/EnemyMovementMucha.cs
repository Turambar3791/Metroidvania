using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementMucha : MonoBehaviour
{
    public GameObject player;
    private float speed = 3f;

    private float distance;

    private PlayerMovement playerMovement;
    private PlayerHealth playerHealth;

    private int damage = 1;

    void Start()
    {
        player = GameObject.Find("Player");
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    void Update()
    {

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.MovePosition(transform.position + Vector3.forward * Time.deltaTime);
        distance = Vector3.Distance(transform.position, player.transform.position);
        Vector3 direction = player.transform.position - transform.position;
        Vector3 localScale = transform.localScale;
        if (direction.x < 0) localScale.x = Mathf.Abs(localScale.x);
        else localScale.x = Mathf.Abs(localScale.x) * -1;
        transform.localScale = localScale;

        transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
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
}
