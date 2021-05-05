using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManagerScript : MonoBehaviour
{
    //Variables
    public int enemiesToSpawn;
    public float spawnIncreaseMultiplier;

    //SpawnWeights
    public float diceSpawnWeight;
    public float chipstackSpawnWeight;
    public float cardSpawnWeight;


    //GameObjects
    [SerializeField] private GameObject[] spawnerList;

    private void Awake()
    {
        Powerups.ResetPowerupValues();
    }

    // Start is called before the first frame update
    void Start()
    {
        Powerups.ResetPowerupValues();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //calculates amount of enemies to spawn and spreads them among the spawners
    public void Spawn()
    {
        //calculating enemies
        float weightSum = diceSpawnWeight + chipstackSpawnWeight + cardSpawnWeight;
        float dicePct = diceSpawnWeight / weightSum;
        float chipstackPct = chipstackSpawnWeight / weightSum;
        float cardPct = cardSpawnWeight / weightSum;

        int diceToSpawn = (int) (enemiesToSpawn * dicePct);
        float remainder = (enemiesToSpawn * dicePct) - (int)(enemiesToSpawn * dicePct);
        int chipstacksToSpawn = (int) (enemiesToSpawn * chipstackPct + remainder);
        remainder += (enemiesToSpawn * chipstackPct) - (int)(enemiesToSpawn * chipstackPct);
        int cardsToSpawn =(int) (enemiesToSpawn * cardPct + remainder);

        Debug.Log("Spawning");

        if (spawnerList.Length > 0)
        {
            //reset Variables in Spawner
            foreach (GameObject spawner in spawnerList)
            {
                spawner.GetComponent<SpawnerScript>().ResetToSpawnVariables();
            }

            //spread enemies among the spawners
            int spawnerCounter = 0;
            while (diceToSpawn > 0)
            {
                spawnerList[spawnerCounter % spawnerList.Length].GetComponent<SpawnerScript>().DiceToSpawn++;
                diceToSpawn--;
                spawnerCounter++;
            }
            while (chipstacksToSpawn > 0)
            {
                spawnerList[spawnerCounter % spawnerList.Length].GetComponent<SpawnerScript>().ChipstacksToSpawn++;
                chipstacksToSpawn--;
                spawnerCounter++;
            }
            while (cardsToSpawn > 0)
            {
                spawnerList[spawnerCounter % spawnerList.Length].GetComponent<SpawnerScript>().CardsToSpawn++;
                cardsToSpawn--;
                spawnerCounter++;
            }

            //spawn enemies
            foreach (GameObject spawner in spawnerList)
            {
                spawner.GetComponent<SpawnerScript>().Spawn();
            }
        }

    }

    public void IncreaseEnemies()
    {
        enemiesToSpawn = (int) (enemiesToSpawn * spawnIncreaseMultiplier);
    }
}
