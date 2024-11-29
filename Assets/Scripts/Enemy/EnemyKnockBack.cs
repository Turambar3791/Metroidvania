using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockBack : MonoBehaviour
{
    private float KnockbackForce = 15f;
    public float KnockbackCounter;
    public float KnockbackTotalTime = 0.1f;
    public bool KnockFromRight;

    [SerializeField] private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (KnockbackCounter > 0)
        {
            if (KnockFromRight)
            {
                rigidBody.velocity = new Vector2(-KnockbackForce, 0);
            }
            else
            {
                rigidBody.velocity = new Vector2(KnockbackForce, 0);
            }

            KnockbackCounter -= Time.deltaTime;
        }
        else
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }
    }
}
