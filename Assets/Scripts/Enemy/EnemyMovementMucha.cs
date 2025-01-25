using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementMucha : MonoBehaviour
{
    private GameObject player;
    private float speed = 3f;

    private float distance;

    public EnemyKnockBack enemyKnockBack;
    public EnemyRange enemyRange;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        if (enemyKnockBack.KnockbackCounter <= 0 && enemyRange.isInRange)
        {

            rb.MovePosition(transform.position + Vector3.forward * Time.deltaTime);
            distance = Vector3.Distance(transform.position, player.transform.position);

            Vector3 direction = player.transform.position - transform.position;

            Vector3 localScale = transform.localScale;
            if (direction.x < 0) localScale.x = Mathf.Abs(localScale.x);
            else localScale.x = Mathf.Abs(localScale.x) * -1;
            transform.localScale = localScale;

            transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else if(!enemyRange.isInRange) {
            rb.velocity = Vector2.zero;
        }
    }
}
