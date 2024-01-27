using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    private Vector2 movementInput;
    
    private float horizontal;
    [SerializeField] float originSpeed;
    [SerializeField] float currentSpeed;
    private bool isFacingRight = true;

    [SerializeField] float dashPower;
    bool isDashing = false;
    public float skillCD;
    private float dashTimer;
    
    private bool isJumping;
    bool extraJump;
    [SerializeField] float extraJumpPower;

    [SerializeField] float jumpPower;
    [SerializeField] float jumpBufferTime;
    [SerializeField] float jumpCD;
    private float jumpBufferCount;

    [SerializeField] float coyoteTime;
    private float coyoteTimeCount;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private float originalGravity;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = originSpeed;
        originalGravity = rb.gravityScale;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        Debug.Log($"{context.action.name} performed: {context.performed} started: {context.started} canceled: {context.canceled}");
        // Dash
        if (context.started && !isDashing && Time.time > dashTimer)
        {
            StartCoroutine(Dash());
        }
    }
    
    public void OnJump(InputAction.CallbackContext context)
    {
        if (IsGrounded() && !context.performed)
        {
            extraJump = false;
        }
        
        if (context.started)
        {
            Debug.Log($"Jump");

            jumpBufferCount = jumpBufferTime;
            if (IsGrounded() || extraJump)
            {
                Debug.Log($"Extra Jump");
                rb.velocity = new Vector2(rb.velocity.x, extraJump ? extraJumpPower : jumpPower);
                extraJump = !extraJump;
            }
            
        }
        
        if (context.canceled && rb.velocity.y > 0f)
        {
            Debug.Log($"Cancel Jump");
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

            coyoteTimeCount = 0f;
        }   
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!isDashing)
        {
            horizontal = movementInput.x;

            if (IsGrounded())
            {
                coyoteTimeCount = coyoteTime;
            }
            else
            {
                coyoteTimeCount -= Time.deltaTime;
            }

            if (isJumping)
            {
                jumpBufferCount -= Time.deltaTime;
                if (jumpBufferCount < 0f)
                {
                    jumpBufferCount = 0f;
                    isJumping = false;
                }
            }

            if (coyoteTimeCount > 0f && jumpBufferCount > 0f && !isJumping)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                jumpBufferCount = 0f;
                StartCoroutine(JumpCooldown());
            }
        }

        Flip();
    }

    private void FixedUpdate()
    {
        var speed = currentSpeed;
        if (!isDashing)
        {
            speed = originSpeed;
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    private IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(jumpCD);
        isJumping = false;
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        rb.gravityScale = 0;
        float dashTime = 0.25f;

        Vector2 originalVelocity = rb.velocity;
        rb.velocity = new Vector2(dashPower * Mathf.Sign(rb.velocity.x), rb.velocity.y);

        yield return new WaitForSeconds(dashTime);

        rb.gravityScale = originalGravity;
        dashTimer = Time.time + skillCD;
        isDashing = false;
    }
}
