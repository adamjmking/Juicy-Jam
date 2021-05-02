using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipEnemyScript : MonoBehaviour
{
    //Variables
    public float seekedDistanceToPlayer;
    public float chipSpeed;
    public float shootCooldown;
    public float coolDownUntilStartShooting;
    public float projectileSpeed;
    private bool shootingInvoked;

    //Components
    private Transform chipTransform;
    private Transform playerTransform;
    Rigidbody2D chipRigidBody;

    //GameObjects
    public GameObject player;
    public GameObject chipProjectile;

    //Properties
    private float DistanceToPlayer => Vector2.Distance(chipTransform.position, playerTransform.position);


    // Start is called before the first frame update
    void Start()
    {
        chipTransform = GetComponent<Transform>();
        playerTransform = player.GetComponent<Transform>();
        chipRigidBody = GetComponent<Rigidbody2D>();
        shootingInvoked = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(DistanceToPlayer);
    }

    private void FixedUpdate()
    {
        //Movement
        if (DistanceToPlayer > seekedDistanceToPlayer) {
            Vector2 normalizedVectorToPlayer = (playerTransform.position - chipTransform.position).normalized;
            chipRigidBody.velocity = chipSpeed * normalizedVectorToPlayer;
        }
        else
        {
            chipRigidBody.velocity = new Vector2(0, 0);
        }

        //(De)Activate Shooting
        if(DistanceToPlayer < seekedDistanceToPlayer && !shootingInvoked)
        {
            shootingInvoked = true; 
            InvokeRepeating(((Action)ShootProjectile).Method.Name, coolDownUntilStartShooting, shootCooldown);
        }
        if(DistanceToPlayer > seekedDistanceToPlayer && shootingInvoked)
        {
            shootingInvoked = false;
            CancelInvoke();
        }

    }

    private void ShootProjectile()
    {
        Vector2 normalizedVectorToPlayer = (playerTransform.position - chipTransform.position).normalized;
        GameObject newProjectile = Instantiate(chipProjectile);
        Rigidbody2D projectileRigidbody = newProjectile.GetComponent<Rigidbody2D>();
        projectileRigidbody.transform.position = chipTransform.position;
        projectileRigidbody.velocity = projectileSpeed * normalizedVectorToPlayer;
    }
}
