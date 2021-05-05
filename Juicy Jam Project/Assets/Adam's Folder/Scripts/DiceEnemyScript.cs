using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class DiceEnemyScript : MonoBehaviour
{
    //Components
    Transform target;
    Transform playerTrans;
    Transform slotMachineTrans;
    Rigidbody2D rb;
    Animator animator;

    //Variables
    public float speed = 200f;
    public float nextWayPointDistance = 3f;
    public float playerChaseDistance = 16f;
    public float attackDistance = 4f;
    public float attackCoolDown = 1.5f;
    public bool ableToAttack = true;

    public GameObject babyDice;

    //Scripts
    public SFXManager sfx;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sfx = FindObjectOfType<SFXManager>();
    }

    void Start()
    {
        playerTrans = GameObject.Find("Player").GetComponent<Transform>();
        slotMachineTrans = GameObject.Find("Slotmachine").GetComponent<Transform>();
        target = playerTrans;

        InvokeRepeating("UpdatePath", 0f, 0.5f);
        InvokeRepeating(nameof(InvokeSpawnBabies), 0, 5);
        
    }

    void InvokeSpawnBabies()
    {
        animator.SetBool("Summon", true);
        Invoke(nameof(SpawnBabies), 1);
    }


    void UpdateTarget()
    {
        if (((transform.position - playerTrans.position).sqrMagnitude <= playerChaseDistance) &&
            !((transform.position - target.position).sqrMagnitude <= attackDistance))
        {
            target = playerTrans;
        }
        else
        {
            target = slotMachineTrans;
        }

        if ((transform.position - target.position).sqrMagnitude <= attackDistance)
        {
            if (ableToAttack)
            {
                ableToAttack = false;
                Attack();
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }
    
    void SpawnBabies()
    {
        sfx.PlaySound(7);
        for (int i=0; i < Powerups.extaDiceSummon + 1; i++)
        {
            Instantiate(babyDice, (Vector2)transform.position + Random.insideUnitCircle * 2, Quaternion.identity);
        }
        Invoke(nameof(SetIdle), 1);
    }

    void SetIdle()
    {

        animator.SetBool("Summon", false);
    }
    void ResetAttack()
    {
        ableToAttack = true;
    }

    void Attack()
    {
        if (target == playerTrans)
        {
            PlayerHealthHandler playerHealth = playerTrans.GetComponent<PlayerHealthHandler>();
            playerHealth.healthSystem.Damage(5 * Powerups.enemyContactDamageMult);
            playerTrans.GetComponent<PlayerMovement>().AddKnockback((playerTrans.position - transform.position).normalized);
        }
        else if (target == slotMachineTrans)
        {
            SlotmachineHealthHandler slotmachineHealth = slotMachineTrans.GetComponent<SlotmachineHealthHandler>();
            slotmachineHealth.healthSystem.Damage(5 * Powerups.enemyContactDamageMult);
        }

        Invoke(nameof(ResetAttack), attackCoolDown / Powerups.enemyAttackSpeedMult);
    }
}
