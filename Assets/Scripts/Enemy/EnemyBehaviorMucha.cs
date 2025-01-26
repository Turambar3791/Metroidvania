using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviorMucha : MonoBehaviour
{

    private PlayerMovement playerMovement;
    private PlayerHealth playerHealth;

    private int damage = 1;

    void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
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
