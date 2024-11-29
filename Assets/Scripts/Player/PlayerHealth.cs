using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public int maxHealth = 5;
    public int health;

    //immunity
    public float ImmunityCounter;
    public float ImmunityTotalTime = 1.5f;

    //death
    public float DeathCounter;
    public float DeathTotalTime = 2f;

    public Vector2 spawnPoint;

    [SerializeField] private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        // immunity
        if (ImmunityCounter > 0)
        {
            ImmunityCounter -= Time.deltaTime;
        }

        // death
        if (DeathCounter <= 0)
        {
            if (health <= 0)
            {
                rigidBody.transform.position = new Vector3(spawnPoint.x, spawnPoint.y, -2);
                playerMovement.KnockbackCounter = 0;
                health = maxHealth;
            }
        }
        else
        {
            DeathCounter -= Time.deltaTime;
        }

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        playerMovement.KnockbackCounter = playerMovement.KnockbackTotalTime;
        ImmunityCounter = ImmunityTotalTime;
        if (health <= 0)
        {
            if (DeathCounter <= 0) DeathCounter = DeathTotalTime;
        }
    }
}
