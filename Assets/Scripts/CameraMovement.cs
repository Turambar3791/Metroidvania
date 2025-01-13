using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private PlayerHealth playerHealth;
    public Camera myCamera;

    // Update is called once per frame
    void Update()
    { 
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        if(playerHealth.DeathCounter <= 0 && playerHealth.health <= 0)
        {
            myCamera.enabled = false;
        } else
        {
            myCamera.enabled = true;
        }
    }
}