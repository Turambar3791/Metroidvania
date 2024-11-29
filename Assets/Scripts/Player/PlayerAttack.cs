using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private GameObject attackArea = default;

    public bool attacking = false;
    public bool attackInput;

    public float timeToAttack = 0.25f;
    private float attackTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        attackArea = transform.Find("AttackArea").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        attackInput = UserInput.instance.AttackInput;

        if (attackInput)
        {
            Attack();
        }

        if(attacking)
        {
            attackTimer += Time.deltaTime;

            if(attackTimer >= timeToAttack)
            {
                attackTimer = 0;
                attacking = false;
                attackArea.SetActive(attacking);
            }
        }
    }

    private void Attack()
    {
        attacking = true;
        attackArea.SetActive(attacking);
    }
}
