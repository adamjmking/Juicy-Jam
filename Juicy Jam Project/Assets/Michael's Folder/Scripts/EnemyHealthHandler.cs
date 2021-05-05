using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthHandler : MonoBehaviour
{
    //Variables
    public float health;
    public HealthSystem healthSystem { get; set; }

    //Knockback
    Rigidbody2D rb;
    public float knockbackForce = 20f;

    //Scripts
    public SFXManager sfx;

    // Start is called before the first frame update
    void Start()
    {
        healthSystem = new HealthSystem(health);
        healthSystem.OnHealthChanged += HealthChanged;
        healthSystem.OnDamageTaken += DamageTaken;
        healthSystem.SetMaxHealth(100 + Powerups.enemyExtraHealth);
        healthSystem.SetHealth(100 + Powerups.enemyExtraHealth);
        sfx = FindObjectOfType<SFXManager>();

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void DamageTaken(object sender, EventArgs args)
    {
        if (healthSystem.GetHealth() <= 0) return;
        switch (gameObject.name)
        {
            case "ChipStack(Clone)":
            case "ChipStack":
                sfx.PlayChipHurtSound();
                break;
            case "Card Enemy(Clone)":
            case "Card Enemy":
                sfx.PlayCardHurtSound();
                break;
            case "Dice Enemy(Clone)":
            case "Dice Enemy":
                sfx.PlayDiceHurtSound();
                break;
        }
    }

    private void HealthChanged(object sender, EventArgs args)
    {
        if (healthSystem.GetHealth() <= 0) Die();
    }

    private void Die()
    {
        switch (gameObject.name)
        {
            case "ChipStack(Clone)":
            case "ChipStack":
                sfx.PlayChipDeathSound();
                break;
            case "Card Enemy(Clone)":
            case "Card Enemy":
                sfx.PlayCardDeathSound();
                break;
            case "Dice Enemy(Clone)":
            case "Dice Enemy":
                sfx.PlayDiceDeathSound();
                break;
            case "BabyDice(Clone)":
                sfx.PlayBabyDiceDeathSound();
                break;
        }
        Destroy(gameObject);
    }

    public void Knockback(GameObject other)
    {
        Vector2 knockbackDir = transform.position - other.transform.position;
        knockbackDir.Normalize();
        rb.AddForce(knockbackDir * (knockbackForce / Powerups.enemyKnockbackResistance), ForceMode2D.Impulse);
    }
}
