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

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        slotMachine = GameObject.Find("Slotmachine");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Invoke(((Action)DestroyGameObjekt).Method.Name, 0.1f);

        //Damage Target
        if (collision.collider.gameObject.name.Equals("Player"))
        {
            PlayerHealthHandler playerHealth = player.GetComponent<PlayerHealthHandler>();
            playerHealth.healthSystem.Damage(5);
        }
        else if (collision.collider.gameObject.name.Equals("Slotmachine"))
        {
            SlotmachineHealthHandler slotmachineHealth = slotMachine.GetComponent<SlotmachineHealthHandler>();
            slotmachineHealth.healthSystem.Damage(5);
        }

    }

    private void DestroyGameObjekt()
    {
        Destroy(gameObject);
    }
}
