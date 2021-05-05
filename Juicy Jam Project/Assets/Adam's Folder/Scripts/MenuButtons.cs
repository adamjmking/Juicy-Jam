using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{

    //Scripts
    public SFXManager sfx;

    private void Start()
    {
        sfx = FindObjectOfType<SFXManager>();
    }
    public void GoToScene(string name)
    {
        sfx.PlaySound(0);
        SceneManager.LoadScene(name);
    }
}
