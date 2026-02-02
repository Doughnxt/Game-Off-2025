using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement variables")]
    [SerializeField] private float speed = 3;
    private float smoothDirection;
    [SerializeField] private float smoothTime = 0.1f;
    private float moveVelocity;
    public float direction;
    public bool movementEnabled;
    public bool facingRight;

    // Compenent variables
    private BoxCollider2D coll;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    private PlayerHealth playerHealth;

    [Header("Jump variables")]
    [SerializeField] private float jumpStrength = 7f;
    [SerializeField] private float fallMultiplier = 3.5f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float jumpTime = .5f;
    [SerializeField] private float jumpTimeCounter;
    private bool isJumping;
    private int jumpCounter = 1;
    [SerializeField] private float jumpGraceTime = 0.08f;
    private bool canStillJump = false;
    private bool jumpHeld;
    private float jumpBufferTime = 0.1f;
    private float jumpBufferCounter;

    [Header("Wall Jump variables")]
    private WallCheck wallCheck;
    private bool canWallJump;
    public bool canWallJumpAgain;
    [SerializeField] private float wallJumpBufferTime = 0.15f;
    [SerializeField] private float wallJumpDuration = 0.1f;
    [SerializeField] private Vector2 wallJumpStrength = new Vector2(5f, 25);
    private bool isWallJumping;
    private bool isWallSliding;
    [SerializeField] private float wallSlideSpeed = 10f;
    private float normalGravity;
    private float wallJumpDirecton;

    [Header("Dash variables")]
    [SerializeField] private float dashForce = 5f;
    [SerializeField] private float dashTime = .5f;
    public bool dashEnabled;
    [SerializeField] private float dashCooldown = .3f;
    private int dashCounter = 1;
    public bool isDashing;

    // Pushing variables
    public bool canPush = false;
    private BlockCheck blockCheck;

    // Enums
    public enum MovementState { idle, running, jumping, falling, pushing, dashing, sliding }
    public MovementState state;

    [Header("Misc. variables")]
    [SerializeField] private float loadTime = .65f;
    private SaveManager saveManager;


    // [Header("Sound variables")]
    //private AudioSource walkingSound;
    //[SerializeField] private AudioSource dashSound;
    //private bool walkingSoundPlaying;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();
        blockCheck = FindObjectOfType<BlockCheck>();
        wallCheck = FindObjectOfType<WallCheck>();
        saveManager = FindObjectOfType<SaveManager>();
        //walkingSound = GetComponent<AudioSource>();
        movementEnabled = true;
        dashEnabled = true;
        dashCounter = 1;
        isDashing = false;
        isWallJumping = false;
        canWallJump = true;
        canWallJumpAgain = true;
        isWallSliding = false;
        normalGravity = rb.gravityScale;
        //walkingSoundPlaying = false;
        StartCoroutine(FreezePlayerAtStart());
    }


    void Update()
    {
        ManageState();

        if (!playerHealth.isDead)
        {
            if (movementEnabled)
            {
                WallSlide();
                if (!wallCheck.isTouchingWall)
                {
                    if (!isWallJumping)
                    {
                        HeldDownJump();
                        JumpBuffer();
                        Fall();
                    }
                }
                if (saveManager.dashObtained)
                {
                    Dash();
                }
            }
        }

        if (IsGrounded())
        {
            dashCounter = 1;
        }
    }

    private void FixedUpdate()
    {
        if (movementEnabled)
        {
            Walk();
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (movementEnabled && !playerHealth.isDead)
        {
            direction = context.ReadValue<Vector2>().x;
        }
    }

    private void Walk()
    {
        smoothDirection = Mathf.SmoothDamp(smoothDirection, direction, ref moveVelocity, smoothTime);

        if (direction != 0)
        {
            rb.velocity = new Vector2(smoothDirection * speed, rb.velocity.y);
            if (direction > 0)
            {
                facingRight = true;
                sprite.flipX = false;
            }
            else
            {
                facingRight = false;
                sprite.flipX = true;
            }
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (movementEnabled && !playerHealth.isDead)
        {
            if (context.performed)
            {
                if (wallCheck.isTouchingWall && !IsGrounded())
                {
                    WallJump();
                    return;
                }

                if (!isWallJumping && !wallCheck.isTouchingWall)
                {
                    Jump();
                    jumpBufferCounter = jumpBufferTime;
                }

            }

            if (context.canceled)
            {
                jumpHeld = false;
                isJumping = false;
            }

        }

    }

    private void Jump()
    {
        //Jumping
        if (IsGrounded() || canStillJump)
        {
            if (jumpCounter == 1)
            {
                isJumping = true;
                jumpHeld = true;
                jumpTimeCounter = jumpTime;
                rb.velocity = Vector2.up * jumpStrength;
                jumpCounter = 0;
            }
        }
    }

    private void HeldDownJump()
    {
        if (jumpHeld && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpStrength;
                jumpTimeCounter -= Time.deltaTime;
            }

            if (IsGrounded() && jumpTimeCounter <= 0)
            {
                isJumping = false;
                jumpTimeCounter = jumpTime;
            }
        }

        if (IsGrounded())
        {
            canStillJump = true;
            jumpCounter = 1;
            isWallJumping = false;

        }

        else if (!IsGrounded())
        {
            StartCoroutine(JumpGracePeriod());
        }
    }

    private void JumpBuffer()
    {
        if (jumpBufferCounter > 0)
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (jumpBufferCounter > 0 && IsGrounded())
        {
            Jump();
            jumpBufferCounter = 0;
        }
    }

    private void Fall()
    {
        if (rb.velocity.y < 0)
        {
            if (!isWallSliding)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
        }
    }

    private IEnumerator JumpGracePeriod()
    {
        yield return new WaitForSeconds(jumpGraceTime);
        canStillJump = false;
    }

    private void WallJump()
    {
        wallJumpDuration = -direction;
        if (canWallJump && canWallJumpAgain)
        {
            canWallJumpAgain = false;
            isWallJumping = true;
            canWallJump = false;
            StartCoroutine(WallJumpBuffer());
            rb.velocity = new Vector2(wallJumpDuration * wallJumpStrength.x, wallJumpStrength.y);
        }
    }

    private IEnumerator WallJumpBuffer()
    {
        movementEnabled = false;
        yield return new WaitForSeconds(wallJumpDuration/6);
        movementEnabled = true;
        yield return new WaitForSeconds((5*wallJumpDuration)/6);
        isWallJumping = false;
        yield return new WaitForSeconds(wallJumpBufferTime);
        canWallJump = true;
        canWallJumpAgain = true;
    }

    private void WallSlide()
    {
        if (!IsGrounded() && wallCheck.isTouchingWall && direction != 0 && !isWallJumping)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -wallSlideSpeed));
        }
        else
        {
            isWallSliding = false;
        }

    }

    private void Dash()
    {
        if (dashEnabled && dashCounter == 1)
        {
            if (Input.GetButtonDown("Dash"))
            {
                dashCounter--;
                StartCoroutine(Dashing());
            }
        }
    }

    public bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0.1f, Vector2.down, .1f, groundLayer);
    }

    private IEnumerator Dashing()
    {
        movementEnabled = false;
        rb.velocity = Vector2.zero;
        dashEnabled = false;
        isDashing = true;
        if (saveManager.dashUpgraded)
        {
            Physics2D.IgnoreLayerCollision(10, 12, true);
            // Switch animator controller for upgraded dash
        }
        state = MovementState.dashing;
        //dashSound.Play();
        rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        if (facingRight)
        {
            rb.AddForce(Vector2.right * dashForce, ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(Vector2.left * dashForce, ForceMode2D.Impulse);
        }
        yield return new WaitForSeconds(dashTime);
        Physics2D.IgnoreLayerCollision(10, 12, false);
        rb.velocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        isDashing = false;
        movementEnabled = true;

        if (!IsGrounded() && rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (!IsGrounded() && rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }
        yield return new WaitForSeconds(dashCooldown);
        dashEnabled = true;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        //pushing blocks
        if (collision.gameObject.CompareTag("Pushable Block"))
        {
            if (Input.GetButton("Push"))
            {
                canPush = true;
            }
            else
            {
                canPush = false;
            }
        }
    }

    private void ManageState()
    {
        if (dashEnabled)
        {
            if (blockCheck != null)
            {
                if (IsGrounded())
                {
                    if (blockCheck.isTouchingBlock && canPush)
                    {
                        state = MovementState.pushing;
                    }
                }
            }

            if (IsGrounded() && direction != 0)
            {
                if (!blockCheck.isTouchingBlock)
                {
                    state = MovementState.running;
                    //if (!walkingSoundPlaying)
                    {
                        // walkingSound.Play();
                        // walkingSoundPlaying = true;
                    }
                }
            }
            else if (direction == 0 && IsGrounded())
            {
                state = MovementState.idle;
            }

            if (!IsGrounded() && rb.velocity.y > .1f)
            {
                state = MovementState.jumping;
            }
            else if (!IsGrounded() && rb.velocity.y < -.1f)
            {
                if (!isWallSliding)
                {
                    state = MovementState.falling;
                }
                else
                {
                    state = MovementState.sliding;
                }

            }
        }
        else
        {
            state = MovementState.dashing;
        }

        if (state != MovementState.running)
        {
            //if (walkingSoundPlaying)
            {
                //walkingSound.Stop();
                //walkingSoundPlaying = false;
            }
        }

        anim.SetInteger("MoveState", (int)state);
    }


    private IEnumerator FreezePlayerAtStart()
    {
        movementEnabled = false;
        yield return new WaitForSeconds(loadTime);
        movementEnabled = true;
    }


}