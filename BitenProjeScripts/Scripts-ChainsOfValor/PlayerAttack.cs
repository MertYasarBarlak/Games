using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
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
    [SerializeField] private AudioSource[] attackSource;


    private void Start()
    {
        hp = GetComponent<Health>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (hp.isAlive)
        {
            if (Input.GetAxisRaw("Fire1") > 0.5f)
            {
                if (!inAttack)
                {
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
                   
                    Collider2D[] targets = null;
                    targets = Physics2D.OverlapBoxAll(attackArea.transform.position, attackArea.GetComponent<BoxCollider2D>().size, attackArea.transform.eulerAngles.z);
                    if (attackTimer <= 0)
                    {
                        
                        for (int i = 0; i < targets.Length; i++)
                        {
                            if (targets[i].CompareTag("Enemy") && targets[i].gameObject.GetComponent<Health>() != null) targets[i].gameObject.GetComponent<Health>().ChangeHp(-damage);
                        }
                        int k = UnityEngine.Random.Range(0, attackSource.Length);
                        attackSource[k].Play();
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
                    }

                }
            }

            if (prepareTimer > 0) prepareTimer -= Time.deltaTime;
            if (attackTimer > 0) attackTimer -= Time.deltaTime;
            if (cooldownTimer > 0) cooldownTimer -= Time.deltaTime;
        }
    }
}