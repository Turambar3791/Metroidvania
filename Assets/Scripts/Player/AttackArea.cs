using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public int damage = 4;
    public bool KnockFromRight = true;
    public bool isPogo;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<EnemyHealth>() != null)
        {
            if (collider.transform.position.y < transform.position.y)
            {
                isPogo = true;
            }
            else
            {
                isPogo = false;
            }

            if (collider.transform.position.x <= transform.position.x)
            {
                KnockFromRight = true;
            }
            else
            {
                KnockFromRight = false;
            }
            EnemyHealth health = collider.GetComponent<EnemyHealth>();
            health.TakeDamage(damage, KnockFromRight);
        }
    }
}