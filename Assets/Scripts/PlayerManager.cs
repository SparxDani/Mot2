 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [Header("PlayerStats")]
    public float playerSpeed = 5f;
    public float playerJumpForce = 10f;
    public int maxHealth = 100;
    public int currentHealth = 100;
    public int currentPoints = 0;
    public float groundCheckDistance;

    public Transform GroundCheck;
    public Rigidbody2D body;
    public SpriteRenderer sprite;
    public LayerMask groundLayerMask;
    private bool _Jump;
    private bool _canJump;
    private bool _hasDoubleJump;
    private float horizontalInput;
    private float changeInput;

    public Color[] Colors;
    public int colorIndex;

    public Animator animator;
    private float jumpInput;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput != 0)
        {
            animator.SetBool("isRunning", true);
            animator.SetBool("isIdle", false);
        }
        else
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isIdle", true);
        }

        if (horizontalInput > 0)
        {
            sprite.flipX = false;
        }
        else if (horizontalInput < 0)
        {
            sprite.flipX = true;
        }
        if (jumpInput == 1)
        {
            _Jump = true;
        }
        if (changeInput == 1)
        {
            ColorChanger();
        }



    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(horizontalInput * playerSpeed, body.velocity.y);
        CheckRaycast();

        if (_Jump)
        {
            if (_canJump)
            {
                body.AddForce(Vector2.up * playerJumpForce, ForceMode2D.Impulse);
                _canJump = false;
                _Jump = false;
                animator.SetTrigger("Jump");
            }
            else if (_hasDoubleJump)
            {
                body.AddForce(Vector2.up * playerJumpForce, ForceMode2D.Impulse);
                _hasDoubleJump = false;
                _Jump = false;
                //animator.SetTrigger("DoubleJump");
            }
        }
    }
    
    public void OnColorChange(InputAction.CallbackContext context)
    {
        changeInput = context.ReadValue<float>();

    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        horizontalInput = context.ReadValue<float>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jumpInput = context.ReadValue<float>();

    }

    private void ColorChanger()
    {
        colorIndex = (colorIndex + 1) % Colors.Length;
        sprite.color = Colors[colorIndex];

    }
    private void CheckRaycast()
    {
        Debug.DrawRay(GroundCheck.position, Vector2.down * groundCheckDistance, Color.cyan);
        RaycastHit2D hit = Physics2D.Raycast(GroundCheck.position, Vector2.down, groundCheckDistance, groundLayerMask);

        if (hit.collider != null)
        {
            _canJump = true;
            _hasDoubleJump = true;
            animator.SetBool("IsJumping", false);
        }
        else
        {
            _canJump = false;
            animator.SetBool("IsJumping", true);
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Goal"))
        {
            EventManager.PlayerWon();
        }
        else if (other.CompareTag("Coin"))
        {
            currentPoints += 10;
            EventManager.UpdatePoints(currentPoints);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Heart"))
        {
            currentHealth = Mathf.Min(currentHealth + 20, maxHealth);
            EventManager.UpdateHealth(currentHealth , maxHealth);
            Destroy(other.gameObject);
        }
    }


}
