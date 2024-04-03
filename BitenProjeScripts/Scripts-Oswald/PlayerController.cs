using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb2D;
    float moveSpeed;
    [SerializeField] float maxSpeed;
    Animator playerAnimator;
    GeneralHealth health;

    bool grounded;
    int jumpCount;
    [SerializeField] int maxJump;
    bool justJumped;
    [SerializeField] float jumpSpeed;

    [SerializeField] Transform groundCheckTransform;
    [SerializeField] float groundCheckDistance;
    [SerializeField] LayerMask groundCheckLayer;
    [SerializeField] LayerMask enemyCheckLayer;

    [SerializeField] GameObject bullet;
    [SerializeField] float damage;
    [SerializeField] bool fireable = false;
    public bool defaultFireable = false;
    float timer = 0f;
    [SerializeField] float attackCooldown;
    [SerializeField] float attackDelay;
    float attackTimer = 0f;
    bool fire1Down = false;
    bool inPrepareToFire = false;
    float tempTimer = 0f;
    [SerializeField] int startAmmo;
    public int ammo;

    public float spawnX;
    public float spawnY;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        moveSpeed = maxSpeed;
        fireable = false;
        ammo = startAmmo;
        health = GetComponent<GeneralHealth>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        GroundCheck();
        VerticalMovement();
        HorizontalMovement();
        Flip();
        FireBullet();
        VariablesToAnim();
        if (!GetComponent<GeneralHealth>().alive) Respawn();
    }

    void Respawn()
    {
        transform.position = new Vector2(spawnX, spawnY);
        GetComponent<GeneralHealth>().Spawn();
    }

    void VariablesToAnim()
    {
        playerAnimator.SetFloat("PlayerSpeed", Mathf.Abs(rb2D.velocity.x));
        playerAnimator.SetFloat("PlayerYSpeed", rb2D.velocity.y);
        playerAnimator.SetBool("PlayerFireable", fireable);
    }

    void VerticalMovement()
    {
        rb2D.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb2D.velocity.y);
    }

    void HorizontalMovement()
    {
        if (grounded)
        {
            jumpCount = maxJump;
        }
        else
        {
            if (jumpCount == maxJump) jumpCount--;
        }
        if (justJumped)
        {
            if (Input.GetAxis("Jump") < 0.01) justJumped = false;
        }
        else
        {
            if (Input.GetAxis("Jump") > 0.01 && jumpCount > 0)
            {
                justJumped = true;
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
                jumpCount--;
            }
        }
    }

    void GroundCheck()
    {
        if (Physics2D.OverlapCircle(groundCheckTransform.position, groundCheckDistance, groundCheckLayer) || Physics2D.OverlapCircle(groundCheckTransform.position, groundCheckDistance, enemyCheckLayer)) grounded = true;
        else grounded = false;
    }

    void Flip()
    {
        if (rb2D.velocity.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (rb2D.velocity.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    void FireBullet()
    {
        int i = 0;
        if (fireable)
        {
            if (ammo > 0)
            {
                if (Input.GetAxis("Fire1") > 0.01 && !fire1Down)
                {
                    fire1Down = true;
                }
                if (Input.GetAxis("Fire1") < 0.01 && fire1Down)
                {
                    fire1Down = false;
                }

                if (timer >= attackTimer + attackCooldown)
                {
                    if (!inPrepareToFire && fire1Down)
                    {
                        playerAnimator.SetBool("PlayerFire", true);
                        moveSpeed = 0f;
                        tempTimer = timer;
                        inPrepareToFire = true;
                    }
                    if (timer >= (tempTimer + attackDelay) && inPrepareToFire)
                    {
                        attackTimer = timer;
                        moveSpeed = maxSpeed;
                        inPrepareToFire = false;
                        if (!GetComponent<SpriteRenderer>().flipX) i = 1;
                        if (GetComponent<SpriteRenderer>().flipX) i = -1;
                        GameObject bulletClone = Instantiate(bullet, new Vector2((transform.position.x + (i * 0.5f)), transform.position.y + 0.5f), Quaternion.identity);
                        bulletClone.GetComponent<PlayerBulletController>().SetBulletDamage(damage);
                        if (i == 1) bulletClone.GetComponent<SpriteRenderer>().flipX = false;
                        if (i == -1) bulletClone.GetComponent<SpriteRenderer>().flipX = true;
                        bulletClone.GetComponent<Rigidbody2D>().velocity = new Vector2(i, 0) * bulletClone.GetComponent<PlayerBulletController>().bulletSpeed;
                        ammo--;
                        playerAnimator.SetBool("PlayerFire", false);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Vegas")
        {
            fireable = false;
        }
        if (collision.transform.tag == "NotVegas")
        {
            fireable = true;
        }
        if (collision.transform.tag == "Ammo")
        {
            ammo += collision.gameObject.GetComponent<AmmoSupply>().includeAmmo;
            Destroy(collision.gameObject);
        }
        if (collision.transform.tag == "Trap")
        {
            health.GetDamage(100);
        }
    }

    public void GetCheckpoint(float x, float y)
    {
        spawnX = x;
        spawnY = y;
    }
}