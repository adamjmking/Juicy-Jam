using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem
{
    //Variables
    private float health;
    private float maxHealth;

    //Events
    public event EventHandler OnHealthChanged;
    public event EventHandler OnDamageTaken;

    public HealthSystem(float maxHealth)
    {
        this.maxHealth = maxHealth;
        this.health = maxHealth;
    }

    public HealthSystem(float maxHealth, float health)
    {
        this.maxHealth = maxHealth;
        this.health = health;
    }

    public void Damage(float damageAmount)
    {
        health -= damageAmount;
        if (health < 0) health = 0;
        OnHealthChanged(this, EventArgs.Empty);
        try
        {
            OnDamageTaken(this, EventArgs.Empty);
        }
        catch (Exception e) { }
    }

    public void Heal(float healAmount)
    {
        health += healAmount;
        if (health > maxHealth) health = maxHealth;
        OnHealthChanged(this, EventArgs.Empty);
    }

    public float GetHealth()
    {
        return health;
    }

    public float GetHealthPercent()
    {
        return health / maxHealth;
    }

    public void SetMaxHealth(float health)
    {
        maxHealth = health;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public void SetHealth(float newHealth)
    {
        health = newHealth;
        if (health < 0) health = 0;
        if (health > maxHealth) health = maxHealth;
        OnHealthChanged(this, EventArgs.Empty);
    }
}
