using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCoinScript : MonoBehaviour
{
    public bool ableToShoot;
    public int coinCount = 0;
    public float shootCooldown = 2;
    private float coinTime;

    //Gameobject
    public GameObject coin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //We will disable ableToShoot when we are transitioning to the next round
        //Re-enable ableToShoot when the next round begins
        if (ableToShoot && (coinCount > 0) && coinTime >= shootCooldown)
        {
            coinTime = 0f;
            ShootCoins();
        }
        coinTime += Time.deltaTime;
    }

    void ShootCoins()
    {
        for (int i = 1; i <= coinCount; i++)
        {
            Instantiate(coin, transform.position, Quaternion.Euler(0f, 0f, Random.Range(1,360)));
        }
    }

    public void UpdateCoinCount(int extraCoin)
    {
        coinCount += extraCoin;
    }
}
