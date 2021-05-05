using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelBehavior : MonoBehaviour
{
    //Variables
    private bool isSpun;
    public float spinSpeed;
    public int currentTier;
    private bool spinButtonPressed;

    //Components
    private Canvas canvas;
    private Rigidbody2D wheelRigidbody;
    private RectTransform wheelTransform;

    //Scripts
    public SFXManager sfx;

    //GameObjects
    private GameObject wheel;
    private GameObject wheelParent;
    [SerializeField] private GameObject buttonList;
    private SpawnerManagerScript spawnerManager;

    //PowerUps
        //Player PowerUps
        private PowerUp[] tier1PPowerUps = new PowerUp[3];
        private PowerUp[] tier2PPowerUps = new PowerUp[3];
        private PowerUp[] tier3PPowerUps = new PowerUp[3];
        //Enemy PowerUps
        private PowerUp[] tier1EPowerUps = new PowerUp[3];
        private PowerUp[] tier2EPowerUps = new PowerUp[3];
        private PowerUp[] tier3EPowerUps = new PowerUp[3];

    //Options
    private Option[] tier1Options = new Option[3];
    private Option[] tier2Options = new Option[3];
    private Option[] tier3Options = new Option[3];

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        wheel = GameObject.Find("Wheel");
        wheelRigidbody = wheel.GetComponent<Rigidbody2D>();
        wheelTransform = wheel.GetComponent<RectTransform>();
        wheelParent = wheel.transform.parent.gameObject;
        sfx = FindObjectOfType<SFXManager>();
        spawnerManager = GameObject.Find("SpawnerManager").GetComponent<SpawnerManagerScript>();

        CreatePowerUps();

        canvas.enabled = false;
        allowedToReadResult = false;
        isSpun = false;
        wheelParent.SetActive(false);
        buttonList.SetActive(false);
        WheelUpdate();
    }

    //First few frames must be skipped since the wheel hasn't began to spin
    private bool allowedToReadResult;

    private void FixedUpdate()
    {
        GameObject.Find("UICanvas").GetComponentInChildren<Text>().text = "Waves Survived: " + StaticVariables.survivedRounds;
        if (isSpun && wheelRigidbody.angularVelocity > 5)
        {
            wheelRigidbody.AddTorque(-10);
        }
        if(isSpun && wheelRigidbody.angularVelocity < -5)
        {
            wheelRigidbody.AddTorque(8);
        }
        if (isSpun && allowedToReadResult && wheelRigidbody.angularVelocity == 0)
        {
            float rotation = wheelTransform.eulerAngles.z;
            currentTier = (int)(((rotation) / (360f / 3)) + 1);
            Invoke(nameof(ShowOptions), 1);
        }
    }

    //Added a little delay so the player can see the result of the spin
    private void ShowOptions()
    {
        buttonList.SetActive(true);
        wheelParent.SetActive(false);
        SetButtonText();
        isSpun = false;
    }

    private void WheelUpdate()
    {
        if (GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            if (!canvas.enabled)
            {
                sfx.PlayRoundWonSound();
                canvas.enabled = true;
                wheelParent.SetActive(true);
                buttonList.SetActive(false);

                Pause();
            }
        }

        Invoke(nameof(WheelUpdate), 0.1f);
    }

    public void Spin()
    {
        if (!isSpun && !spinButtonPressed)
        {
            spinButtonPressed = true;
            sfx.PlaySound(2); //Plays wheel spin sfx
            wheelRigidbody.AddTorque(-50);
            CreateOptions();
            Invoke(nameof(StartSpin), 0f);
        }
    }

    public void StartSpin()
    {
        isSpun = true;
        allowedToReadResult = false;
        Invoke(nameof(AllowedToReadResult), 0.1f);
        wheelTransform.Rotate(0, 0, UnityEngine.Random.Range(1, 360f), Space.Self);
        wheelRigidbody.AddTorque(spinSpeed);
    }

    public void AllowedToReadResult()
    {
        allowedToReadResult = true;
    }

    public void ClickPowerUp(int buttonNumber)
    {
        //to debug Powerups change the method in the next statement
        //new PowerUp("slotmachine shoots coins", new Action(Powerups.ActivatePSlotMachineShootsCoins), true).Activate();
        //and comment out this switch
        switch (currentTier)
        {
            case 1:
                tier1Options[buttonNumber - 1].Activate();
                break;
            case 2:
                tier2Options[buttonNumber - 1].Activate();
                break;
            case 3:
                tier3Options[buttonNumber - 1].Activate();
                break;
        }

        wheelParent.SetActive(false);
        buttonList.SetActive(false);
        canvas.enabled = false;
        sfx.PlaySound(1); //Plays powerup sfx

        // spawn next Wave
        spawnerManager.Spawn();
        spawnerManager.IncreaseEnemies();

        spinButtonPressed = false;

        Unpause();

        StaticVariables.survivedRounds++;

        HealthSystem playerHealth = FindObjectOfType<PlayerHealthHandler>().healthSystem;
        if(playerHealth.GetHealth() < (playerHealth.GetMaxHealth() / 2))
        {
            playerHealth.SetHealth(playerHealth.GetMaxHealth() / 2);
        }

        HealthSystem slotmachineHealth = FindObjectOfType<SlotmachineHealthHandler>().healthSystem;
        slotmachineHealth.SetHealth(slotmachineHealth.GetMaxHealth());

    }

    private void SetButtonText()
    {
        Option[] optionArr = new Option[3];
        switch (currentTier)
        {
            case 1:
                optionArr = tier1Options;
                break;
            case 2:
                optionArr = tier2Options;
                break;
            case 3:
                optionArr = tier3Options;
                break;
        }

        GameObject.Find("Option1").GetComponentInChildren<Text>().text = optionArr[0].GetText();
        GameObject.Find("Option2").GetComponentInChildren<Text>().text = optionArr[1].GetText();
        GameObject.Find("Option3").GetComponentInChildren<Text>().text = optionArr[2].GetText();
    }

    #region creating, shuffling and assinging powerUps to options

    private void CreatePowerUps()
    {
        //PlayerPowerUps
        tier1PPowerUps[0] = new PowerUp("more attack damage", new Action(Powerups.ActivatePMoreAttackDamage), true);
        tier1PPowerUps[1] = new PowerUp("take less knockback", new Action(Powerups.ActivatePTakeLessKnockback), true);
        tier1PPowerUps[2] = new PowerUp("more movement speed", new Action(Powerups.ActivatePMoreMovementSpeed), true);

        tier2PPowerUps[0] = new PowerUp("more health", new Action(Powerups.ActivatePMoreHealth), true);
        tier2PPowerUps[1] = new PowerUp("more stamina", new Action(Powerups.ActivatePMoreStamina), true);
        tier2PPowerUps[2] = new PowerUp("larger attack range", new Action(Powerups.ActivatePLargerAttackRange), true);

        tier3PPowerUps[0] = new PowerUp("faster stamina regeneration", new Action(Powerups.ActivatePFasterStaminaRegen), true);
        tier3PPowerUps[1] = new PowerUp("slotmachine shoots coins", new Action(Powerups.ActivatePSlotMachineShootsCoins), true);
        tier3PPowerUps[2] = new PowerUp("more slotmachine health", new Action(Powerups.ActivatePSlotMachineExtraHealth), true);

        //EnemyPowerUps
        tier1EPowerUps[0] = new PowerUp("more attack damage", new Action(Powerups.ActivateEMoreAttackDamage), false);
        tier1EPowerUps[1] = new PowerUp("more health", new Action(Powerups.ActivateEMoreHealth), false);
        tier1EPowerUps[2] = new PowerUp("more movement speed", new Action(Powerups.ActivateEMoreMovementSpeed), false);

        tier2EPowerUps[0] = new PowerUp("larger projectiles", new Action(Powerups.ActivateELargerProjectiles), false);
        tier2EPowerUps[1] = new PowerUp("faster projectile fire rate", new Action(Powerups.ActivateEFasterProjectileFireRate), false);
        tier2EPowerUps[2] = new PowerUp("less card knockback", new Action(Powerups.ActivateELessCardKnockback), false);

        tier3EPowerUps[0] = new PowerUp("faster attack speed", new Action(Powerups.ActivateEFasterAttackSpeed), false);
        tier3EPowerUps[1] = new PowerUp("more dice summoned", new Action(Powerups.ActivateEMoreDiceSummoned), false);
        tier3EPowerUps[2] = new PowerUp("more pokerchips damage", new Action(Powerups.ActivateEPokerChipsDamage), false);
    }

    private void CreateOptions()
    {
        ShufflePowerUps();

        tier1Options[0] = new Option(1, tier1PPowerUps[0], tier1EPowerUps[0]);
        tier1Options[1] = new Option(1, tier1PPowerUps[1], tier1EPowerUps[1]);
        tier1Options[2] = new Option(1, tier1PPowerUps[2], tier1EPowerUps[2]);

        tier2Options[0] = new Option(1, tier2PPowerUps[0], tier2EPowerUps[0]);
        tier2Options[1] = new Option(1, tier2PPowerUps[1], tier2EPowerUps[1]);
        tier2Options[2] = new Option(1, tier2PPowerUps[2], tier2EPowerUps[2]);

        tier3Options[0] = new Option(1, tier3PPowerUps[0], tier3EPowerUps[0]);
        tier3Options[1] = new Option(1, tier3PPowerUps[1], tier3EPowerUps[1]);
        tier3Options[2] = new Option(1, tier3PPowerUps[2], tier3EPowerUps[2]);
    }

    //Shuffles all PowerUps in the Arrays
    private void ShufflePowerUps()
    {
        Shuffle(tier1PPowerUps);
        Shuffle(tier2PPowerUps);
        Shuffle(tier3PPowerUps);
        Shuffle(tier1EPowerUps);
        Shuffle(tier2EPowerUps);
        Shuffle(tier3EPowerUps);
    }
    static void Shuffle(PowerUp[] array)
    {
        System.Random rng = new System.Random();
        int n = array.Length;
        while (n > 1)
        {
            int k = rng.Next(n--);
            PowerUp temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }
    #endregion

    #region pause and unpause
    private GameObject player;
    public void Pause()
    {
        //deactivate coinshooting
        ShootCoinScript coinScript = UnityEngine.Object.FindObjectOfType<ShootCoinScript>();
        coinScript.ableToShoot = false;

        //remove all projectiles
        GameObject[] projectileArray = GameObject.FindGameObjectsWithTag("Projectile");

        foreach (GameObject projectile in projectileArray)
        {
            Destroy(projectile);
        }

        //prohibit player from moving
        player = GameObject.Find("Player");
        player.SetActive(false);
    }

    public void Unpause()
    {
        //activate coinshooting
        ShootCoinScript coinScript = UnityEngine.Object.FindObjectOfType<ShootCoinScript>();
        coinScript.ableToShoot = true;

        //allow player to move
        player.SetActive(true);
        player.GetComponent<PlayerMovement>().ReturnToSpawn();
    }

    #endregion
}
