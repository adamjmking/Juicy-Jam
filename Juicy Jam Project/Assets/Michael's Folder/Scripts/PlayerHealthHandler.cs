using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthHandler : MonoBehaviour
{
    //Variables
    public float health;
    public HealthSystem healthSystem { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        healthSystem = new HealthSystem(health);
        healthSystem.OnHealthChanged += HealthChanged; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void HealthChanged(object sender, EventArgs args)
    {
        if (healthSystem.GetHealth() <= 0) Die();
        GameObject.Find("UICanvas").GetComponent<HealthbarScript>().HealthChanged();
    }

    private void Die()
    {
        //player dies
    }
}
