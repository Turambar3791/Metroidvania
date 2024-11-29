using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 9;
    public int health;
    public EnemyKnockBack enemyKnockBack;

    private float ImmunityCounter;
    private float ImmunityTotalTime = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (ImmunityCounter > 0)
        {
            ImmunityCounter -= Time.deltaTime;
        }
    }

    public void TakeDamage(int damage, bool isKnockFromRight)
    {
        if(ImmunityCounter <= 0)
        {
            health -= damage;
            enemyKnockBack.KnockbackCounter = enemyKnockBack.KnockbackTotalTime;
            enemyKnockBack.KnockFromRight = isKnockFromRight;
            ImmunityCounter = ImmunityTotalTime;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
