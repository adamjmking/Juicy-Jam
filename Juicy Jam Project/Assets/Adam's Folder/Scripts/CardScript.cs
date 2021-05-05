using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class CardScript : MonoBehaviour
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

    //Pathfinding
    Path path;
    Seeker seeker;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    void Awake()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        playerTrans = GameObject.Find("Player").GetComponent<Transform>();
        slotMachineTrans = GameObject.Find("Slotmachine").GetComponent<Transform>();
        target = playerTrans;

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
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
        if (path == null) return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWayPointDistance)
        {
            currentWaypoint++;
        }

        //Update sprites
        if (rb.velocity.x >= 0.01f)
        {
            animator.SetBool("Facing Right", true);
        }
        else if (rb.velocity.x <= -0.01f)
        {
            animator.SetBool("Facing Right", false);
        }
        else
        {
            animator.SetBool("Facing Right", false);
        }

        UpdateTarget();
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
