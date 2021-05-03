using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipEnemyScript : MonoBehaviour
{
    //Variables
    public float seekedDistanceToTarget;
    public float chipSpeed;
    public float shootCooldown;
    public float coolDownUntilStartShooting;
    public float projectileSpeed;
    private bool shootingInvoked;

    //Components
    private Transform chipTransform;
    private Transform playerTransform;
    private Transform slotmachineTransform;
    Rigidbody2D chipRigidBody;

    //GameObjects
    private GameObject player;
    private GameObject slotmachine;
    public GameObject chipProjectile;

    //Properties
    private float DistanceToPlayer => Vector2.Distance(chipTransform.position, playerTransform.position);
    private float DistanceToSlotmachine => Vector2.Distance(chipTransform.position, slotmachineTransform.position);
    private bool PlayerNearerThanSlotmachine => DistanceToPlayer < DistanceToSlotmachine;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        slotmachine = GameObject.Find("Slotmachine");
        chipTransform = GetComponent<Transform>();
        playerTransform = player.GetComponent<Transform>();
        slotmachineTransform = slotmachine.GetComponent<Transform>();
        chipRigidBody = GetComponent<Rigidbody2D>();
        shootingInvoked = false;
    }

    private void FixedUpdate()
    {
        //Moves to the nearest target (player or slotmachine) until its near enough to shoot
        if (DistanceToPlayer > seekedDistanceToTarget && DistanceToSlotmachine > seekedDistanceToTarget) {
            Vector2 normalizedVectorToTarget;
            if (PlayerNearerThanSlotmachine)
            {
                normalizedVectorToTarget = (playerTransform.position - chipTransform.position).normalized;
            }
            else
            {
                normalizedVectorToTarget = (slotmachineTransform.position - chipTransform.position).normalized;
            }
            chipRigidBody.velocity = chipSpeed * normalizedVectorToTarget;
        }
        else
        {
            chipRigidBody.velocity = new Vector2(0, 0);
        }

        //(De)Activate Shooting
        if((DistanceToPlayer < seekedDistanceToTarget || DistanceToSlotmachine < seekedDistanceToTarget) && !shootingInvoked)
        {
            shootingInvoked = true; 
            InvokeRepeating(((Action)ShootProjectile).Method.Name, coolDownUntilStartShooting, shootCooldown);
        }
        if(DistanceToPlayer > seekedDistanceToTarget && DistanceToSlotmachine > seekedDistanceToTarget  && shootingInvoked)
        {
            shootingInvoked = false;
            CancelInvoke();
        }

    }

    private void ShootProjectile()
    {
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
        Rigidbody2D projectileRigidbody = newProjectile.GetComponent<Rigidbody2D>();
        projectileRigidbody.transform.position = chipTransform.position;
        projectileRigidbody.AddForce(projectileSpeed * normalizedVectorToTarget, ForceMode2D.Impulse);
    }
}
