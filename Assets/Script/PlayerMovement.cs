using System;
using System.Collections;
using Script;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class PlayerMovement : MonoBehaviour
{

    public Vector2 movementInput;
    
    private float horizontal;
    [SerializeField] float originSpeed;
    [SerializeField] float currentSpeed;
    private bool isFacingRight = true;

    [SerializeField] float dashPower;
    bool isDashing = false;
    private float dashTimer;
    [SerializeField] float vertDashMultiplier;
    
    [SerializeField] float jumpPower;
    [SerializeField] float jumpBufferTime;
    private float jumpBufferCount;

    [SerializeField] float coyoteTime;
    private float coyoteTimeCount;

    [SerializeField] private float knockbackDistance = 100f;
    [SerializeField] private float knockbackStunDuration = 0.25f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private float originalGravity;
    
    // Start is called before the first frame update
    private Animator _anim;
    Coroutine _atkCour = null;
    private void Start()
    { 
        //hellowoerld by Rew 1/27/2024

        _anim = GetComponent<Animator>();
        currentSpeed = originSpeed;
        originalGravity = rb.gravityScale;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (GetComponent<PlayerSkill>().canUseSkill)
        {
            if (context.started && !isDashing && Time.time > dashTimer)
            {
                StartCoroutine(Dash());
            }
        }
    }
    private bool isJumping = false;
    public void OnJump(InputAction.CallbackContext context)
    {
 
        if (context.started)
        {

            jumpBufferCount = jumpBufferTime;

            
            if ((coyoteTimeCount > 0f && jumpBufferCount > 0f ) && !isJumping)
            {
                isJumping = true;
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                jumpBufferCount = 0f;
                
                _anim.Play("Jump");
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
                isJumping = false;
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

            
        }

        Flip();
        
        _anim.SetBool("isWalking" , rb.velocity.x != 0f);
        _anim.SetBool("isGrounded", IsGrounded());
        _anim.SetBool("isFalling", rb.velocity.y < 0f);
    }
    
    
    bool isKnockback = false;
    public void GotAttacked(Vector2 attackDirection)
    {
        StartCoroutine(Knockback(attackDirection));
    }
 

    private void FixedUpdate()
    {
        var speed = currentSpeed;
        if (isKnockback)
        {
            return;
        }
        if (!isDashing)
        {
            speed = originSpeed;
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y) ;
        }
    }

    [SerializeField] float groundCheckRadius = 0.2f;
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position,groundCheckRadius);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    private IEnumerator Knockback(Vector2 attackDirection)
    {
        isKnockback = true;
        rb.velocity = Vector2.zero;
        Vector2 knockbackDirection =
            new Vector2((attackDirection.x), Random.Range(0.1f, 0.5f)).normalized * knockbackDistance;
        rb.AddForce(knockbackDirection, ForceMode2D.Impulse);
        
        _anim.Play("Knockback");
        yield return new WaitForSeconds(knockbackStunDuration);
        
        isKnockback = false;
        _anim.SetTrigger("endKnockback");

    }

    private IEnumerator Dash()
    {
        isDashing = true;
        rb.gravityScale = 0;
        float dashTime = 0.2f;

        float x = movementInput.x;
        float y = Mathf.Clamp(movementInput.y, 0, 1f);
        
        Vector2 originalVelocity = rb.velocity;
        if (Mathf.Abs(movementInput.x) < 0.1f && movementInput.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(movementInput.y, 0, 1f) * dashPower / vertDashMultiplier);
        }
        else
        {
            rb.velocity = new Vector2(dashPower * Mathf.Sign(movementInput.x), rb.velocity.y);
        }

        _anim.Play("Dash");
        yield return new WaitForSeconds(dashTime);

        rb.velocity = originalVelocity;
        rb.gravityScale = originalGravity;
        dashTimer = Time.time + GetComponent<PlayerSkill>().skillCD;
        isDashing = false;
        
        _anim.SetTrigger("endDash");
    }

   
}
