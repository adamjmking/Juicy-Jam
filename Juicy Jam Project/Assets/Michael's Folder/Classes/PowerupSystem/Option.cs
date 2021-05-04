using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option
{
    public int Tier { get; }

    public Option(int tier, PowerUp playerPowerUp, PowerUp enemyPowerUp)
    {
        Tier = tier;
        SetPowerUps(playerPowerUp, enemyPowerUp);
    }

    public PowerUp PlayerPowerUp { get; set; }
    public PowerUp EnemyPowerUp { get; set; }

    public void SetPowerUps(PowerUp playerPowerUp, PowerUp enemyPowerUp)
    {
        PlayerPowerUp = playerPowerUp;
        EnemyPowerUp = enemyPowerUp;
    }

    public void Activate()
    {
        PlayerPowerUp.Activate();
        EnemyPowerUp.Activate();
    }

    public string GetText()
    {
        return PlayerPowerUp.Name + "\n" + EnemyPowerUp.Name;
    }
}
