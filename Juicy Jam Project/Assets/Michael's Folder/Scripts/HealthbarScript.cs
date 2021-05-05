using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthbarScript : MonoBehaviour
{
    [SerializeField] PlayerHealthHandler playerHealth;
    [SerializeField] SlotmachineHealthHandler slotmachineHealth;
    [SerializeField] PlayerMovement playerMovement;

    public GameObject healthBarForeground;
    public GameObject slotmachineHealthForeground;
    public GameObject staminaBarForeground;

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
