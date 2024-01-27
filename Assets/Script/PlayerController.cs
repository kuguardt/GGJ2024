using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static int playerCount = 0;

    private int playerId;
    
    private Vector2 movementInput;
    
    [SerializeField] List<Color> playerColors = new List<Color>(){Color.red, Color.blue, Color.green, Color.yellow};
    // Start is called before the first frame update
    
    void Awake()
    {
        playerId = playerCount;
        playerCount++;
        GetComponent<SpriteRenderer>().color = playerColors[playerId];
    }
    
    
    private float horizontal;
    [SerializeField] float originSpeed;
    [SerializeField] float currentSpeed;
    private bool isFacingRight = true;

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

    private bool isJumpDown = false;
    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log($"{context.action.name} performed: {context.performed} started: {context.started} canceled: {context.canceled}");
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
        
        if (IsGrounded() && !context.performed)
        {
            extraJump = false;
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
        rb.velocity = new Vector2(horizontal * currentSpeed, rb.velocity.y);
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
}
