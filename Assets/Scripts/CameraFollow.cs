using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private float horizontal;
    private bool isFacingRight = true;
    public Player player;

    private Vector3 offset = new Vector3(1f, 0f, -10f);
    private float smoothTime = 0.23f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");

        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        if(player.health > 0) Flip();
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            if (isFacingRight)
            {
                offset = new Vector3(1f, 0f, -10f);
            } else {
                offset = new Vector3(-1f, 0f, -10f);
            }
        }
    }
}