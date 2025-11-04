using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //movement variables
    [SerializeField] private float speed = 3;
    public float direction;
    public bool movementEnabled;
    public bool facingRight;

    //compenent variables
    private BoxCollider2D coll;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    private PlayerHealth playerHealth;

    //jump variables
    [SerializeField] private float jumpStrength = 7f;
    [SerializeField] private float fallMultiplier = 3.5f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float jumpTime = .5f;
    [SerializeField] private float jumpTimeCounter;
    private bool isJumping;

    //dash variables
    [SerializeField] private float dashForce = 5f;
    [SerializeField] private float dashTime = .5f;
    private bool dashEnabled;
    [SerializeField] private float dashCooldown = .3f;
    private int dashCounter = 1;

    //enums
    private enum MovementState { idle, running, jumping, falling, pushing, dashing, healing }
    MovementState state;

    //misc. variables
    [SerializeField] private float loadTime = .3f;
    //private SavepointManager savepointManager;

    //sound variables
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
        //savepointManager = FindObjectOfType<SavepointManager>();
        //walkingSound = GetComponent<AudioSource>();
        movementEnabled = true;
        dashEnabled = true;
        dashCounter = 1;
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
                Jump();
                Dash();
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

    private void Walk()
    {
        direction = Input.GetAxis("Horizontal");
        if (direction != 0)
        {
            rb.velocity = new Vector2(direction * speed, rb.velocity.y);
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

    private void Jump()
    {
        //Falling
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        //Jumping
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpStrength;
        }

        if (Input.GetButton("Jump") && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpStrength;
                jumpTimeCounter -= Time.deltaTime;
            }

            if (IsGrounded() && jumpTimeCounter <= 0)
            {
                isJumping = true;
                jumpTimeCounter = jumpTime;
                rb.velocity = Vector2.up * jumpStrength;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
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
        dashEnabled = false;
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
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
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

    private void ManageState()
    {
        if (dashEnabled)
        {
            if (IsGrounded() && direction != 0)
            {
                state = MovementState.running;
                //if (!walkingSoundPlaying)
                {
                    //walkingSound.Play();
                    //walkingSoundPlaying = true;
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
                state = MovementState.falling;
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