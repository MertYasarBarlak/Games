using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Health hp;
    [SerializeField] private GameObject attackArea;
    [SerializeField] private float prepareTime;
    [SerializeField] private float attackTime;
    [SerializeField] private float cooldownTime;
    private float prepareTimer;
    private float attackTimer;
    private float cooldownTimer;
    public int attackPhase = 0;
    private Animator anim;
    public float damage;
    private bool inAttack = false;
    private float tempSpeed;

    private void Start()
    {
        hp = GetComponent<Health>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (hp.isAlive)
        {
            if (attackArea.GetComponent<AttackArea>().playerDetected)
            {
                if (!inAttack)
                {
                    if (GetComponent<EnemyMoveFollewer>())
                    {
                        if (GetComponent<EnemyMoveFollewer>().speed != 0)
                        {
                            tempSpeed = GetComponent<EnemyMoveFollewer>().speed;
                            GetComponent<EnemyMoveFollewer>().speed = 0;
                        }
                    }
                    anim.SetBool("isAttacking", true);
                    inAttack = true;
                    attackPhase = 1;
                    prepareTimer = prepareTime;
                }
            }

            if (inAttack)
            {
                if (attackPhase == 1)
                {
                    if (prepareTimer <= 0)
                    {
                        attackTimer = attackTime;
                        attackPhase = 2;
                    }
                }
                if (attackPhase == 2)
                {
                    Collider2D[] targets = Physics2D.OverlapBoxAll(attackArea.transform.position, attackArea.GetComponent<BoxCollider2D>().size, attackArea.transform.eulerAngles.z);
                    if (attackTimer <= 0)
                    {
                        for (int i = 0; i < targets.Length; i++)
                        {
                            if (targets[i].CompareTag("Player") && targets[i].gameObject.GetComponent<Health>() != null) targets[i].gameObject.GetComponent<Health>().ChangeHp(-damage);
                        }
                        cooldownTimer = cooldownTime;
                        attackPhase = 3;
                    }
                }
                if (attackPhase == 3)
                {
                    if (cooldownTimer <= 0)
                    {
                        anim.SetBool("isAttacking", false);
                        attackPhase = 0;
                        inAttack = false;
                        GetComponent<EnemyMoveFollewer>().speed = tempSpeed;
                    }

                }
            }

            if (prepareTimer > 0) prepareTimer -= Time.deltaTime;
            if (attackTimer > 0) attackTimer -= Time.deltaTime;
            if (cooldownTimer > 0) cooldownTimer -= Time.deltaTime;
        }
    }
}