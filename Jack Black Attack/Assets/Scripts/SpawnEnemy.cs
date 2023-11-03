using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public int numberOfEnemies;
    // Start is called before the first frame update
    void Start()
    {
        numberOfEnemies = LevelManager.cardNumber;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    //take number of spawnLocations and make a script that spawns an enemy prefab at each location specified
}

