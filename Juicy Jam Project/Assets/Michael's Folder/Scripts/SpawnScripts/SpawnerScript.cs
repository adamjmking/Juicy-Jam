using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    //Variables
    public float spawnDistance;
    public int DiceToSpawn { get; set; }
    public int ChipstacksToSpawn { get; set; }
    public int CardsToSpawn { get; set; }

    //GameObjects
    public GameObject dice;
    public GameObject card;
    public GameObject chipstack;

    //Components
    private Transform spawnerTransform;

    // Start is called before the first frame update
    void Start()
    {
        spawnerTransform = GetComponent<Transform>();
        GetComponent<SpriteRenderer>().enabled = false;
        ResetToSpawnVariables();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {
        //spawn Dice
        for(int i = 0; i < DiceToSpawn; i++)
        {
            Instantiate(dice, GetRandomPosInSpawnArea(), spawnerTransform.rotation);
        }

        //spawn Cards
        for (int i = 0; i < CardsToSpawn; i++)
        {
            Instantiate(card, GetRandomPosInSpawnArea(), spawnerTransform.rotation);
        }

        //spawn ChipStacks
        for (int i = 0; i < ChipstacksToSpawn; i++)
        {
            Instantiate(chipstack, GetRandomPosInSpawnArea(), spawnerTransform.rotation);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(GetComponent<Transform>().position, new Vector3(spawnDistance*2, spawnDistance*2));
    }

    public void ResetToSpawnVariables()
    {
        DiceToSpawn = 0;
        ChipstacksToSpawn = 0;
        CardsToSpawn = 0;
    }

    public Vector3 GetRandomPosInSpawnArea()
    {
        float x = Random.Range(spawnerTransform.position.x - spawnDistance, spawnerTransform.position.x + spawnDistance);
        float y = Random.Range(spawnerTransform.position.y - spawnDistance, spawnerTransform.position.y + spawnDistance);
        return new Vector3(x, y);
    }
}
