using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("PlayerStats")]
    public float playerSpeed = 5f;
    public float playerJumpForce = 10f;
    public float healthAmount = 10f;
    public int maxHealth = 100;
    public int currentHealth = 100;
    public int currentPoints = 0;

    public Transform GroundCheck;
    public Rigidbody2D body;
    public float groundCheckDistance = 0.2f;
    public SpriteRenderer sprite;
    public LayerMask groundLayerMask;
    private bool IsGrounded;
    private bool _Jump;
    private bool _canJump;
    private bool _hasDoubleJump;
    private float horizontalInput;

    public Animator animator;
    public GameController gameController;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        EventManager.OnPointsUpdated.AddListener(UpdatePoints);
        EventManager.OnHealthUpdated.AddListener(UpdateHealth);
        EventManager.OnPlayerDefeated.AddListener(OnPlayerDefeat);
        EventManager.OnPlayerWon.AddListener(OnPlayerWin);
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _Jump = true;
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

    private void UpdatePoints(int points)
    {
        currentPoints += points;
        Debug.Log("Puntaje actualizado: " + currentPoints);
    }

    private void UpdateHealth(int health)
    {
        currentHealth = Mathf.Min(currentHealth + health, maxHealth);
        Debug.Log("Vida actualizada: " + currentHealth);

        if (currentHealth <= 0)
        {
            EventManager.OnPlayerDefeated.Invoke();
        }
    }

    private void OnPlayerDefeat()
    {
        Debug.Log("¡Jugador derrotado!");
    }

    private void OnPlayerWin()
    {
        Debug.Log("¡Has ganado!");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Goal"))
        {
            EventManager.OnPlayerWon.Invoke();
        }
        else if (other.CompareTag("Coin"))
        {
            EventManager.OnPointsUpdated.Invoke(10);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Heart"))
        {
            EventManager.OnHealthUpdated.Invoke(20);
            Destroy(other.gameObject);
        }
    }
}
