using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthbarScript : MonoBehaviour
{
    PlayerHealthHandler playerHealth;
    SlotmachineHealthHandler slotmachineHealth;
    PlayerMovement playerMovement;
    

    public GameObject healthBarForeground;
    public GameObject slotmachineHealthForeground;
    public GameObject staminaBarForeground;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealthHandler>();
        slotmachineHealth = GameObject.Find("Slotmachine").GetComponent<SlotmachineHealthHandler>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        staminaBarForeground.transform.localScale = new Vector3((float)playerMovement.stamina/(float)playerMovement.maxStamina, 1);
    }

    public void HealthChanged()
    {
        healthBarForeground.transform.localScale = new Vector3(playerHealth.healthSystem.GetHealthPercent(), 1);
    }

    public void SlotmachineHealthChanged()
    {
        slotmachineHealthForeground.transform.localScale = new Vector3(slotmachineHealth.healthSystem.GetHealthPercent(), 1);
    }
}
