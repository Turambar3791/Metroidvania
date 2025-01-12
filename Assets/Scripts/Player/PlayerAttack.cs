using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private GameObject attackArea = default;
    private GameObject attackAreaUp = default;
    private GameObject attackAreaDown = default;
    private GameObject waterguy = default;
    private GameObject waterguyAttack = default;
    private GameObject waterguyAttackUp = default;
    private GameObject waterguyAttackBottom = default;

    public PlayerMovement playerMovement;

    public bool attacking = false;
    public bool attackInput;

    private bool enableAttack = true;

    public float timeToAttack = 0.15f;
    private float attackTimer = 0f;

    private float attackBlockerCounter;
    private float attackBlockerTotalTime = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        attackArea = transform.Find("AttackArea").gameObject;
        attackAreaUp = transform.Find("AttackAreaUp").gameObject;
        attackAreaDown = transform.Find("AttackAreaDown").gameObject;
        waterguy = transform.Find("waterguy").gameObject;
        waterguyAttack = transform.Find("waterguyAttack").gameObject;
        waterguyAttackUp = transform.Find("waterguyAttackUp").gameObject;
        waterguyAttackBottom = transform.Find("waterguyAttackBottom").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        attackInput = UserInput.instance.AttackInput;

        if (attackInput && enableAttack)
        {
            Attack();
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
                attackArea.SetActive(attacking);
                attackAreaUp.SetActive(attacking);
                attackAreaDown.SetActive(attacking);

                waterguyAttack.SetActive(attacking);
                waterguyAttackUp.SetActive(attacking);
                waterguyAttackBottom.SetActive(attacking);
                waterguy.SetActive(true);

                enableAttack = false;
                attackBlockerCounter = attackBlockerTotalTime;
            }
        }
    }

    private void Attack()
    {
        attacking = true;
        waterguy.GetComponent<Animator>().SetFloat("xVelocity", 0);
        if (UserInput.instance.MoveInput.y == 0)
        {
            attackArea.SetActive(attacking);
            waterguyAttack.SetActive(attacking);
            waterguy.SetActive(false);
        }
        else if (UserInput.instance.MoveInput.y > 0)
        {
            attackAreaUp.SetActive(attacking);
            waterguyAttackUp.SetActive(attacking);
            waterguy.SetActive(false);
        }
        else if (UserInput.instance.MoveInput.y < 0 && !playerMovement.isGrounded)
        {
            attackAreaDown.SetActive(attacking);
            waterguyAttackBottom.SetActive(attacking);
            waterguy.SetActive(false);
        }
    }
}