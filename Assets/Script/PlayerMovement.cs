using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 movementInput;
    
    private float horizontal;
    [SerializeField] float originSpeed;
    [SerializeField] float currentSpeed;
    private bool isFacingRight = true;

    private bool isDashing = false;
    [SerializeField] float dashPower = 20f;
    [SerializeField] float dashCooldown = 2f;
    private float dashTimer = 0f;
    
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

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = originSpeed;
    }

    // Update is called once per frame
    
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
    
    private bool isJumpDown = false;
    public void OnJump(InputAction.CallbackContext context)
    {
        if (IsGrounded() && !context.performed)
        {
            extraJump = false;
        }
        
        if (context.started)
        {
            Debug.Log($"Jump");

            isJumpDown = true;
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
        horizontal = movementInput.x;

        if (IsGrounded())
        {
            coyoteTimeCount = coyoteTime;
        }
        else
        {
            coyoteTimeCount -= Time.deltaTime;
        }

        if (isJumpDown)
        {
            isJumpDown = false;
        }
        else
        {
            jumpBufferCount -= Time.deltaTime;
            if (jumpBufferCount < 0f)
            {
                jumpBufferCount = 0f;
            }
        }
        

        if (coyoteTimeCount > 0f && jumpBufferCount > 0f && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);

            jumpBufferCount = 0f;

            StartCoroutine(JumpCooldown());
        }
        
        Flip();
    }

    private void FixedUpdate()
    {
        var speed = currentSpeed;
        if (!isDashing)
        {
            speed = dashPower;
        }
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
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
        float dashTime = 0.2f; // Adjust as needed

        // Remember the velocity before dashing
        Vector2 originalVelocity = rb.velocity;

        // Apply dash force
        rb.velocity = new Vector2(rb.velocity.x + dashPower * Mathf.Sign(rb.velocity.x), rb.velocity.y);

        // Wait for dash time
        yield return new WaitForSeconds(dashTime);

        // Reset velocity to its original value
        rb.velocity = originalVelocity;

        // Set a cooldown for the dash
        dashTimer = Time.time + dashCooldown;
        isDashing = false;
    }
}
