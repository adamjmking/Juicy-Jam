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

    // Start is called before the first frame update
    void Start()
    {
        healthSystem = new HealthSystem(health);
        healthSystem.OnHealthChanged += HealthChanged;
        healthSystem.SetMaxHealth(100 + Powerups.enemyExtraHealth);
        healthSystem.SetHealth(100 + Powerups.enemyExtraHealth);

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void HealthChanged(object sender, EventArgs args)
    {
        if (healthSystem.GetHealth() <= 0) Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public void Knockback(GameObject other)
    {
        Vector2 knockbackDir = transform.position - other.transform.position;
        knockbackDir.Normalize();
        rb.AddForce(knockbackDir * (knockbackForce / Powerups.enemyKnockbackResistance), ForceMode2D.Impulse);
    }
}
