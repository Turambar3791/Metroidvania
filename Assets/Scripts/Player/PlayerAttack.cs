using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private GameObject attackArea = default;
    private GameObject attackAreaUp = default;
    private GameObject attackAreaDown = default;

    public PlayerMovement playerMovement;

    public bool attacking = false;
    public bool attackInput;

    public float timeToAttack = 0.25f;
    private float attackTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        attackArea = transform.Find("AttackArea").gameObject;
        attackAreaUp = transform.Find("AttackAreaUp").gameObject;
        attackAreaDown = transform.Find("AttackAreaDown").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        attackInput = UserInput.instance.AttackInput;

        if (attackInput)
        {
            Attack();
        }

        if (attacking)
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= timeToAttack)
            {
                attackTimer = 0;
                attacking = false;
                attackArea.SetActive(attacking);
                attackAreaUp.SetActive(attacking);
                attackAreaDown.SetActive(attacking);
            }
        }
    }

    private void Attack()
    {
        attacking = true;
        if (UserInput.instance.MoveInput.y == 0)
        {
            attackArea.SetActive(attacking);
        }
        else if (UserInput.instance.MoveInput.y > 0)
        {
            attackAreaUp.SetActive(attacking);
        }
        else if (UserInput.instance.MoveInput.y < 0 && !playerMovement.isGrounded)
        {
            attackAreaDown.SetActive(attacking);
        }
    }
}