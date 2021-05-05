using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathTimerScript : MonoBehaviour
{
    public float deathTime;
    //Scripts
    public SFXManager sfx;

    // Start is called before the first frame update
    void Start()
    {
        sfx = FindObjectOfType<SFXManager>();
        sfx.PlaySound(0);
        Invoke(nameof(SwitchScene), deathTime);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SwitchScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
