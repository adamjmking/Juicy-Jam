using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp
{
    public string Name { get; }
    public Action Method { get; }
    public bool PlayerPowerUp { get; }

    public PowerUp(string name, Action method, bool playerPowerUp)
    {
        Name = name;
        Method = method;
        PlayerPowerUp = playerPowerUp;
    }

    public void Activate()
    {
        Method.Invoke();
    }
}
