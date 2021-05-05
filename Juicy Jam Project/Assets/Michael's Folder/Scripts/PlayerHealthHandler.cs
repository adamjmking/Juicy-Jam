using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthHandler : MonoBehaviour
{
    //Variables
    public float health;
    public HealthSystem healthSystem { get; set; }

    //Components
    Animator animator;
    PlayerAttack playerAttack;
    PlayerMovement playerMovement;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerAttack = GetComponent<PlayerAttack>();
        playerMovement = GetComponent<PlayerMovement>();
        healthSystem = new HealthSystem(health);
        healthSystem.OnHealthChanged += HealthChanged; 
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void HealthChanged(object sender, EventArgs args)
    {
        if (healthSystem.GetHealth() <= 0) Stunned();
        GameObject.Find("UICanvas").GetComponent<HealthbarScript>().HealthChanged();
    }

    private void Stunned()
    {
        animator.SetBool("Stunned", true);
        playerMovement.ableToMove = false;
        playerAttack.ableToAttack = false;
        Rigidbody2D rbPlayer = playerMovement.GetComponent<Rigidbody2D>();
        rbPlayer.velocity = Vector2.zero;
        Invoke(nameof(GetUp), 0.67f);
    }

    private void GetUp()
    {
        playerMovement.ableToMove = true;
        playerAttack.ableToAttack = true;
        animator.SetBool("Stunned", false);
        float healthHealed = healthSystem.GetMaxHealth() * 0.15f;
        healthSystem.Heal(healthHealed);
        GameObject.Find("UICanvas").GetComponent<HealthbarScript>().HealthChanged();
    }
}
