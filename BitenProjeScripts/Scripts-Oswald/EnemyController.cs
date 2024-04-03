using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] bool cantTakeDamage;
    [SerializeField] bool melee;

    GeneralHealth enemyHealthScript;
    [SerializeField] LayerMask targetLayer;

    GameObject attackArea;
    [SerializeField] float attackCooldown;
    [SerializeField] float attackDelay;
    float attackTimer = 0f;
    float tempTimer = 0f;
    [SerializeField] float damage;
    bool inAttack = false;
    [SerializeField] GameObject bullet;

    [SerializeField] float pathMinX;
    [SerializeField] float pathMaxX;
    [SerializeField] float speed;
    float tempSpeed;
    Rigidbody2D rb2D;
    Animator EnemyAnimator;

    bool isTrigger;
    float timer;

    public PlayerProofManager proofs;

    public GameObject healthBar;

    void Start()
    {
        enemyHealthScript = GetComponent<GeneralHealth>();
        attackArea = transform.GetChild(0).gameObject;
        rb2D = GetComponent<Rigidbody2D>();
        EnemyAnimator = GetComponent<Animator>();
        healthBar.GetComponent<Slider>().maxValue = enemyHealthScript.maxHealth;
        if (cantTakeDamage) healthBar.GetComponent<Slider>().value = enemyHealthScript.health;
    }
   
    void Update()
    {
        if (enemyHealthScript.alive == true)
        {
            timer += Time.deltaTime;
            Attack();
            if (!cantTakeDamage) healthBar.GetComponent<Slider>().value = enemyHealthScript.health;

            if (isTrigger)
            {
            }
            else
            {
                GoForAWalk();
            }
        }
        else
        {
            proofs.proofCount++;
            Destroy(gameObject);
        }
    }

    void Attack()
    {
        if (timer >= attackTimer + attackCooldown)
        {
            RaycastHit2D hit = Physics2D.BoxCast(attackArea.GetComponent<Collider2D>().bounds.center, attackArea.GetComponent<Collider2D>().bounds.size, 0, Vector2.right, 0, targetLayer);
            if (hit.collider != null)
            {
                if (!inAttack)
                {
                    tempTimer = timer;
                    tempSpeed = speed;
                    speed = 0f;
                    EnemyAnimator.SetBool("InAttack", true);
                    inAttack = true;
                }
            }
            if (timer >= tempTimer + attackDelay && inAttack)
            {
                if (melee)
                {
                    if (hit.collider != null) hit.transform.GetComponent<GeneralHealth>().GetDamage(damage);
                }
                else
                {
                    GameObject bulletClone = Instantiate(bullet, new Vector2(transform.position.x + (0.5f * (transform.localScale.x / Mathf.Abs(transform.localScale.x))), transform.position.y + 0.5f), Quaternion.identity);
                    bulletClone.GetComponent<EnemyBulletController>().SetBulletDamage(damage);
                    if((transform.localScale.x / Mathf.Abs(transform.localScale.x))> 0)  bulletClone.GetComponent<SpriteRenderer>().flipX = false;
                    if((transform.localScale.x / Mathf.Abs(transform.localScale.x))< 0)  bulletClone.GetComponent<SpriteRenderer>().flipX = true;
                    bulletClone.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x / Mathf.Abs(transform.localScale.x), 0) * bulletClone.GetComponent<EnemyBulletController>().bulletSpeed;
                }
                speed = tempSpeed;
                attackTimer = timer;
                EnemyAnimator.SetBool("InAttack", false);
                inAttack = false;
            }
        }
    }

    void GoForAWalk()
    {
        if (transform.position.x <= pathMinX && (transform.localScale.x / Mathf.Abs(transform.localScale.x)) == -1)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            if (!cantTakeDamage) healthBar.transform.localScale = new Vector3(healthBar.transform.localScale.x * -1, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        }
        if (transform.position.x >= pathMaxX && (transform.localScale.x / Mathf.Abs(transform.localScale.x)) == 1)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            if (!cantTakeDamage) healthBar.transform.localScale = new Vector3(healthBar.transform.localScale.x * -1, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        }
        rb2D.velocity = new Vector2(speed * (transform.localScale.x / Mathf.Abs(transform.localScale.x)), rb2D.velocity.y);
    }

    public void GetDamage(float getDamage)
    {
        if (!cantTakeDamage) GetComponent<GeneralHealth>().GetDamage(getDamage);
    }
}