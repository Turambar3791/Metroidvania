using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private float jumpingPower = 16f;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        rigidBody.velocity = new Vector2(Input.GetAxis("Horizontal") * 5, rigidBody.velocity.y);

        //skakanie
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded()) {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpingPower);
        }

        if (Input.GetKeyUp(KeyCode.Space) && rigidBody.velocity.y > 0f) {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y * 0.5f);
        }
        
    }

    private bool IsGrounded() 
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}
