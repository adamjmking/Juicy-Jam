using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipProjectileBehavior : MonoBehaviour
{
    //GameObjects

    //GameObjects
    private GameObject player;
    private GameObject slotMachine;

    //Knockback
    Rigidbody2D rigidbody;
    public float knockbackForce = 20f;

    PlayerMovement playerMovement;

    //Scripts
    public SFXManager sfx;

    // Start is called before the first frame update
    void Start()
    {
        sfx = FindObjectOfType<SFXManager>();
        player = GameObject.Find("Player");
        slotMachine = GameObject.Find("Slotmachine");
        playerMovement = player.GetComponent<PlayerMovement>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        sfx.PlaySound(5);

        Invoke(((Action)DestroyGameObjekt).Method.Name, 0.05f);

        //Damage Target
        if (collision.collider.gameObject.name.Equals("Player"))
        {
            if (!playerMovement.dashing) //if we are dashing, the player is invincible
            {
                PlayerHealthHandler playerHealth = player.GetComponent<PlayerHealthHandler>();
                playerHealth.healthSystem.Damage(5 * Powerups.projectileDamageMult);
            }
        }
        else if (collision.collider.gameObject.name.Equals("Slotmachine"))
        {
            SlotmachineHealthHandler slotmachineHealth = slotMachine.GetComponent<SlotmachineHealthHandler>();
            slotmachineHealth.healthSystem.Damage(5 * Powerups.projectileDamageMult);
        }

    }

    private void DestroyGameObjekt()
    {
        Destroy(gameObject);
    }

    public void Knockback(GameObject other)
    {
        Vector2 knockbackDir = transform.position - other.transform.position;
        knockbackDir.Normalize();
        rigidbody.AddForce(knockbackDir * (knockbackForce / Powerups.enemyKnockbackResistance), ForceMode2D.Impulse);
    }
}
