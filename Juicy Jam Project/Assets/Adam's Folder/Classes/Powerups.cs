using UnityEngine;

public static class Powerups
{
    //Initializations
    #region General Powerups
    public static float damageMult = 1f;
    public static float knockbackResistance = 1f;
    public static float moveSpeedMult = 1f;
    public static float extraHealth = 0f;
    #endregion

    #region Player Powerups
    public static float attackRangeMult = 1f;
    public static float staminaRegenMult = 1f;
    public static int extraStamina = 1;
    public static float slotMachineExtraHealth = 0f;
    public static int coinShotDelta = 1;
    #endregion

    #region Enemy Powerups
    public static float enemyContactDamageMult = 1f;
    public static float enemyExtraHealth = 0f;
    public static float enemyMoveSpeedMult = 1f;
    public static float enemyKnockbackResistance = 1f;
    public static float projectileSizeMult = 1f;
    public static float projectileRateOfFireMult = 1f;
    public static float projectileSpeedMult = 1f;
    public static float projectileHomeInWeight = 1f;
    public static float enemyAttackSpeedMult = 1f;
    public static int extaDiceSummon = 0;
    #endregion

    //Scripts
    static PlayerMovement pMovement;
    static ShootCoinScript coinScript;

    public static void ResetPowerupValues() //Call this when the game needs to restart
    {
    #region General Powerups
    damageMult = 1f;
    knockbackResistance = 1f;
    moveSpeedMult = 1f;
    extraHealth = 0f;
    #endregion

    #region Player Powerups
    extraStamina = 0;
    attackRangeMult = 1f;
    staminaRegenMult = 1f;
    slotMachineExtraHealth = 0;
    #endregion

    #region Enemy Powerups
    enemyContactDamageMult = 1f;
    enemyExtraHealth = 0f;
    enemyMoveSpeedMult = 1f;
    enemyKnockbackResistance = 1f;
    projectileSizeMult = 1f;
    projectileRateOfFireMult = 1f;
    projectileSpeedMult = 1f;
    projectileHomeInWeight = 1f;
    enemyAttackSpeedMult = 1f;
    extaDiceSummon = 0;
    #endregion
}

    public static void UpdatePowerUpValues(int tier, int button)
    {
        switch (tier)
        {
            case 1:
                {
                    switch (button)
                    {
                        case 1:
                            damageMult += 0.2f; //Added
                            enemyContactDamageMult += 0.1f;
                            break;
                        case 2:
                            knockbackResistance += 0.1f;
                            enemyExtraHealth += 10f;
                            break;
                        case 3:
                            moveSpeedMult += 0.2f; //Added
                            enemyMoveSpeedMult += 0.1f; //Added
                            break;
                    }
                }
                break;
            case 2:
                {
                    switch (button)
                    {
                        case 1:
                            extraHealth += 10f;
                            projectileSizeMult += 0.2f; //Added
                            break;
                        case 2:
                            pMovement = Object.FindObjectOfType<PlayerMovement>();
                            pMovement.UpdateMaxStamina(extraStamina); //Added
                            projectileRateOfFireMult += 0.2f; //Added
                            break;
                        case 3:
                            attackRangeMult += 0.1f; //Added
                            enemyKnockbackResistance += 0.1f;
                            break;
                    }
                }
                break;
            case 3:
                {
                    switch (button)
                    {
                        case 1:
                            staminaRegenMult += 0.1f; //Added
                            enemyAttackSpeedMult += 0.1f; 
                            projectileSpeedMult += 0.1f; //Added
                            break;
                        case 2:
                            coinScript = Object.FindObjectOfType<ShootCoinScript>();
                            coinScript.UpdateCoinCount(coinShotDelta); //Added
                            extaDiceSummon += 1;
                            break;
                        case 3:
                            slotMachineExtraHealth += 10f;
                            projectileHomeInWeight += 0.1f;
                            break;
                    }
                }
                break;
        }

    }
}
