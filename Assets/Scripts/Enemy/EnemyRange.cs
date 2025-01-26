using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour
{
    public bool isInRange;

    void Start()
    {
        isInRange = false;
    }


    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            isInRange = true;
        }
    }
}
