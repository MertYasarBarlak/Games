using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ParticleSystem Dust;
    [Header("Setup")]
    [SerializeField] private GroundCheck2D groundCheck;
    private Rigidbody2D rb;
    private Health health;
    private bool hasHealth;
    private Animator anim;
    private bool hasAnim;

    [Header("Run")]
    [SerializeField] private float runSpeed = 9f;
    [SerializeField] private float accelerationTime = 0.1f;
    [SerializeField] private float deaccelerationTime = 0.05f;
    [SerializeField] private float airControllMultiplier = 0.2f;
    private float acceleration;
    private float scaleX;
    private bool isAbleToRun = true;

    [Header("Jump")]
    [SerializeField] private float jumpSpeed = 9f;
    private bool jumpSwitch;
    [SerializeField] private int maxJump;
    public int bonusJump;
    private int jumpCount;
    [SerializeField] private float jumpButtonRemember = 0.1f;
    private float jumpButtonRememberTimer;
    [SerializeField] private float groundedRememeber = 0.1f;
    private float groundedRememeberTimer;

    [Header("Wall Jump")]
    [SerializeField] private SideCheck2D sideCheck;
    public bool isWallJumpable;
    [SerializeField] private float wallSlidingSpeed = 2f;
    private bool isWallSliding = false;
    [SerializeField] private Vector2 wallJumpSpeed = new Vector2(5f, 10f);

    [Header("Dash")]
    [SerializeField] private float dashSpeed = 18f;
    [SerializeField] private float dashTime = 0.2f;
    [SerializeField] private float dashCooldown = 1f;
    [SerializeField] private TrailRenderer trailRenderer;
    public bool canDash = true;
    private bool dashSwitch;

    private enum MovementState { idle, running, jumping, falling, dashing, sliceing }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null) Debug.LogWarning("Player has not Rigidbody2D!");

        health = GetComponent<Health>();
        hasHealth = health != null;

        if (groundCheck == null) Debug.LogWarning("Player ground check not set!");

        anim = GetComponent<Animator>();
        hasAnim = anim != null;

        scaleX = transform.localScale.x;
    }

    private void Update()
    {
        if (hasHealth)
        {
            if (health.isAlive)
            {
                HorizontalMovement();
                VerticalMovement();
                Flip();
                WallSlide();
                WallJump();
                Dash();
            }
        }
        else
        {
            HorizontalMovement();
            VerticalMovement();
            Flip();
            WallSlide();
            WallJump();
            Dash();
        }

        if (hasAnim) UpdateAnimationState();
    }

    private void HorizontalMovement()
    {
        if (isAbleToRun)
        {
            float wantedSpeedX = Input.GetAxisRaw("Horizontal") * runSpeed - rb.velocity.x;

            acceleration = ((int)Input.GetAxisRaw("Horizontal") == 0) ? wantedSpeedX * (Time.deltaTime / deaccelerationTime) : wantedSpeedX * (Time.deltaTime / accelerationTime);

            if (!groundCheck.grounded) acceleration *= airControllMultiplier;

            rb.velocity += new Vector2(acceleration, 0);
        }
    }

    private void VerticalMovement()
    {
        if (groundCheck.grounded)
        {
            if (groundedRememeberTimer != groundedRememeber) groundedRememeberTimer = groundedRememeber;
        }
        else
        {
            if (jumpCount == (maxJump + bonusJump)) jumpCount--;
            if (groundedRememeberTimer > 0) groundedRememeberTimer -= Time.deltaTime;
        }
        if (groundedRememeberTimer > 0) jumpCount = (maxJump + bonusJump);

        if (jumpButtonRememberTimer > 0) jumpButtonRememberTimer -= Time.deltaTime;

        if ((int)Input.GetAxisRaw("Jump") > 0.5) if (jumpButtonRememberTimer != jumpButtonRemember) jumpButtonRememberTimer = jumpButtonRemember;

        if (jumpSwitch)
        {
            if ((int)Input.GetAxisRaw("Jump") == 0) jumpSwitch = false;
        }
        else
        {
            if ((jumpButtonRememberTimer > 0) && jumpCount > 0)
            {
                jumpSwitch = true;
                CreateDust();
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                jumpCount--;
                if (bonusJump != 0)
                {
                    bonusJump--;
                    if (bonusJump < 0) bonusJump = 0;
                }
            }
        }
    }

    private void WallSlide()
    {
        if (sideCheck.sideCheck && !groundCheck.grounded && (int)Input.GetAxisRaw("Horizontal") != 0)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (Input.GetButtonDown("Jump") && isWallSliding)
        {
            rb.velocity = new Vector2(-wallJumpSpeed.x * Mathf.Sign(transform.localScale.x), wallJumpSpeed.y);
        }
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(DashCoroutine());
        }
    }

    IEnumerator DashCoroutine()
    {
        canDash = false;
        isAbleToRun = false;
        dashSwitch = true;
        float orginialGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashSpeed, 0f);
        trailRenderer.emitting = true;
        yield return new WaitForSeconds(dashTime);
        trailRenderer.emitting = false;
        rb.gravityScale = orginialGravity;
        isAbleToRun = true;
        dashSwitch = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private void Flip()
    {
        if ((int)Input.GetAxisRaw("Horizontal") < -0.5f) transform.localScale = new Vector3(-scaleX, transform.localScale.y, transform.localScale.z);
        else if ((int)Input.GetAxisRaw("Horizontal") > 0.5f) transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (Mathf.Abs(rb.velocity.x) > 0.1f)
        {
            state = MovementState.running;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > 0.1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -0.1f)
        {
            state = MovementState.falling;
        }

        if (isWallSliding) state = MovementState.sliceing;

        if (dashSwitch) state = MovementState.dashing;

        anim.SetInteger("state", (int)state);
    }

    void CreateDust()
    {
        Dust.Play();
    }
}