using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Camera camera;

    // Update is called once per frame
    void Update()
    { 
        if(playerHealth.DeathCounter <= 0 && playerHealth.health <= 0)
        {
            camera.enabled = false;
        } else
        {
            camera.enabled = true;
        }
    }
}