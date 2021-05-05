using System.Reflection;
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
    public static float projectileDamageMult = 1f;
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
        if (player != null)
        {
            PlayerHealthHandler playerHealth = player.GetComponent<PlayerHealthHandler>();
            if (playerHealth.healthSystem != null)
                {
                    playerHealth.healthSystem.SetMaxHealth(100 + extraHealth);
                    playerHealth.healthSystem.SetHealth(100 + extraHealth);
                }
        }
        #endregion

        #region Player Powerups
        extraStamina = 0;
        attackRangeMult = 1f;
        staminaRegenMult = 1f;
        slotMachineExtraHealth = 0;
        var slotmachine = SlotmachineHealthHandler.SlotmachineInstance;
        if (slotmachine != null)
        {
            if (slotmachine.healthSystem != null)
            {
                slotmachine.healthSystem.SetMaxHealth(100 + slotMachineExtraHealth);
                slotmachine.healthSystem.SetHealth(100 + slotMachineExtraHealth);
            }
        }
        #endregion

        #region Enemy Powerups
        enemyContactDamageMult = 1f;
        enemyExtraHealth = 0f;
        enemyMoveSpeedMult = 1f;
        enemyKnockbackResistance = 1f;
        projectileSizeMult = 1f;
        projectileRateOfFireMult = 1f;
        projectileSpeedMult = 1f;
        projectileDamageMult = 1f;
        enemyAttackSpeedMult = 1f;
        extaDiceSummon = 0;
        #endregion
    }

    //GameObjects
    public static GameObject player;

    #region Player Powerups
    #region Tier1
    public static void ActivatePMoreAttackDamage()
    {
        damageMult += 0.2f; //Added
    }
    public static void ActivatePTakeLessKnockback()
    {
        knockbackResistance += 0.1f; //Added
    }
    public static void ActivatePMoreMovementSpeed()
    {
        moveSpeedMult += 0.2f; //Added
    }
    #endregion
    #region Tier2
    public static void ActivatePMoreHealth()
    {
        extraHealth += 10f;

        PlayerHealthHandler playerHealth = player.GetComponent<PlayerHealthHandler>();
        playerHealth.healthSystem.SetMaxHealth(100 + extraHealth);
        playerHealth.healthSystem.SetHealth(100+ extraHealth);//Added
    }
    public static void ActivatePMoreStamina()
    {
        pMovement = player.GetComponent<PlayerMovement>();
        pMovement.UpdateMaxStamina(extraStamina); //Added
    }
    public static void ActivatePLargerAttackRange()
    {
        attackRangeMult += 0.1f; //Added
    }
    #endregion
    #region Tier3
    public static void ActivatePFasterStaminaRegen()
    {
        staminaRegenMult += 0.1f; //Added
    }
    public static void ActivatePSlotMachineShootsCoins()
    {
        coinScript = Object.FindObjectOfType<ShootCoinScript>();
        coinScript.UpdateCoinCount(coinShotDelta); //Added
    }
    public static void ActivatePSlotMachineExtraHealth()
    {
        slotMachineExtraHealth += 10f;
        SlotmachineHealthHandler slotmachineHealth = GameObject.Find("Slotmachine").GetComponent<SlotmachineHealthHandler>();
        slotmachineHealth.healthSystem.SetMaxHealth(100 + slotMachineExtraHealth);
        slotmachineHealth.healthSystem.SetHealth(100 + slotMachineExtraHealth);//Added
    }
    #endregion
    #endregion

    #region Enemy Powerups
    #region Tier1
    public static void ActivateEMoreAttackDamage()
    {
        enemyContactDamageMult += 0.1f; //Added
    }
    public static void ActivateEMoreHealth()
    {
        enemyExtraHealth += 10f; //Added
    }
    public static void ActivateEMoreMovementSpeed()
    {
        enemyMoveSpeedMult += 0.1f; //Added
    }
    #endregion
    #region Tier2
    public static void ActivateELargerProjectiles()
    {
        projectileSizeMult += 0.2f; //Added
    }
    public static void ActivateEFasterProjectileFireRate()
    {
        projectileRateOfFireMult += 0.2f; //Added
    }
    public static void ActivateELessCardKnockback()
    {
        enemyKnockbackResistance += 0.1f; //Added
    }
    #endregion
    #region Tier3
    public static void ActivateEFasterAttackSpeed()
    {
        enemyAttackSpeedMult += 0.1f; //Added
        projectileSpeedMult += 0.1f; //Added
    }
    public static void ActivateEMoreDiceSummoned()
    {
        extaDiceSummon += 1; //Added
    }
    public static void ActivateEPokerChipsDamage()
    {
        projectileDamageMult += 0.1f; //Added
    }
    #endregion
    #endregion

}
