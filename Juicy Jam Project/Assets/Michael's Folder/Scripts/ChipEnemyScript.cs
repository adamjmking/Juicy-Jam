using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ChipEnemyScript : MonoBehaviour
{
    //Variables
    public float seekedDistanceToTarget;
    public float speed = 200f;
    public float nextWayPointDistance = 3f;
    public float playerChaseDistance = 16f;
    public float shootCooldown;
    public float coolDownUntilStartShooting;
    public float projectileSpeed;
    private bool shootingInvoked;

    //Pathfinding
    Path path;
    Seeker seeker;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    //Components
    private Transform chipTransform;
    private Transform playerTransform;
    private Transform slotmachineTransform;
    Rigidbody2D rigidBody;
    Transform target;
    Animator animator;

    //GameObjects
    private GameObject player;
    private GameObject slotmachine;
    public GameObject chipProjectile;

    //Properties
    private float DistanceToPlayer => Vector2.Distance(chipTransform.position, playerTransform.position);
    private float DistanceToSlotmachine => Vector2.Distance(chipTransform.position, slotmachineTransform.position);
    private bool PlayerNearerThanSlotmachine => DistanceToPlayer < DistanceToSlotmachine;

    //Scripts
    public SFXManager sfx;


    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();

        player = GameObject.Find("Player");
        slotmachine = GameObject.Find("Slotmachine");
        chipTransform = GetComponent<Transform>();
        playerTransform = player.GetComponent<Transform>();
        slotmachineTransform = slotmachine.GetComponent<Transform>();
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        shootingInvoked = false;
        target = slotmachineTransform;
        sfx = FindObjectOfType<SFXManager>();

        InvokeRepeating(nameof(UpdatePath), 0f, 0.5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rigidBody.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void UpdateTarget()
    {
        if (((transform.position - playerTransform.position).sqrMagnitude <= playerChaseDistance) &&
            !((transform.position - target.position).sqrMagnitude <= seekedDistanceToTarget))
        {
            target = playerTransform;
        }
        else
        {
            target = slotmachineTransform;
        }
    }

    private void FixedUpdate()
    {

        UpdateTarget();

        if ((DistanceToPlayer < seekedDistanceToTarget || DistanceToSlotmachine < seekedDistanceToTarget) && !shootingInvoked)
        {
            shootingInvoked = true;
            InvokeRepeating(((Action)ShootProjectile).Method.Name, coolDownUntilStartShooting, shootCooldown / Powerups.projectileRateOfFireMult);
        }
        if (DistanceToPlayer > seekedDistanceToTarget && DistanceToSlotmachine > seekedDistanceToTarget && shootingInvoked)
        {
            shootingInvoked = false;
            CancelInvoke();
        }

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

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rigidBody.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        rigidBody.AddForce(force);

        float distance = Vector2.Distance(rigidBody.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWayPointDistance)
        {
            currentWaypoint++;
        }

        /*//Update sprites
        if (rigidBody.velocity.x >= 0.01f)
        {
            animator.SetBool("Facing Right", true);
        }
        else if (rigidBody.velocity.x <= -0.01f)
        {
            animator.SetBool("Facing Right", false);
        }
        else
        {
            animator.SetBool("Facing Right", false);
        }*/
    }

    private void ShootProjectile()
    {
        sfx.PlaySound(4);

        Vector2 normalizedVectorToTarget;

        //get vector to the nearer target
        if (PlayerNearerThanSlotmachine)
        {
            normalizedVectorToTarget = (playerTransform.position - chipTransform.position).normalized;
        }
        else
        {
            normalizedVectorToTarget = (slotmachineTransform.position - chipTransform.position).normalized;
        }

        GameObject newProjectile = Instantiate(chipProjectile);
        newProjectile.transform.localScale = new Vector2(1, 1) * Powerups.projectileSizeMult;
        Rigidbody2D projectileRigidbody = newProjectile.GetComponent<Rigidbody2D>();
        projectileRigidbody.transform.position = chipTransform.position;
        projectileRigidbody.AddForce(projectileSpeed * Powerups.projectileSpeedMult * normalizedVectorToTarget, ForceMode2D.Impulse);
    }
}
