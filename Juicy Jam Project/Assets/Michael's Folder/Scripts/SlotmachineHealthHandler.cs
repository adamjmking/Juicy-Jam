using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlotmachineHealthHandler : MonoBehaviour
{
    //Variables
    public float health;
    public HealthSystem healthSystem { get; set; }
    public static SlotmachineHealthHandler SlotmachineInstance;

    private void Awake()
    {
        SlotmachineInstance = this;
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
        if (healthSystem.GetHealth() <= 0) Die();
        GameObject.Find("UICanvas").GetComponent<HealthbarScript>().SlotmachineHealthChanged();
    }

    private void Die()
    {
        SceneManager.LoadScene("DeathScene");
    }

    private void OnDestroy()
    {
        SlotmachineInstance = null;
    }
}
