using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float playerSpeed = 5f;
    public float playerJumpForce = 10f;
    public Transform GroundCheck;
    public Rigidbody2D body;
    public float groundCheckDistance = 0.2f;
    public SpriteRenderer sprite;
    public LayerMask groundLayerMask;
    private bool IsGrounded;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        IsGrounded = Physics2D.Raycast(GroundCheck.position, Vector2.down, groundCheckDistance, groundLayerMask);
        if (IsGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            body.velocity = new Vector2(body.velocity.x, playerJumpForce);
        }
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * playerSpeed, body.velocity.y);

        

    }

}
