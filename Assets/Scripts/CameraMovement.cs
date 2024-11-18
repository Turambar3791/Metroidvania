using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Player player;
    public Camera camera;

    // Update is called once per frame
    void Update()
    { 
        if(player.DeathCounter <= 0 && player.health <= 0)
        {
            camera.enabled = false;
        } else
        {
            camera.enabled = true;
        }
    }
}
