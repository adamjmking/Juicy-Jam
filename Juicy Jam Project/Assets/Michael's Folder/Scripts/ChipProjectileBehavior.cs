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

    PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        slotMachine = GameObject.Find("Slotmachine");
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
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
}
