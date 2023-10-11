using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTile : MonoBehaviour
{
    //Reference to level manager
    private LevelManager lm;

    //Animation for spawning in an enemy
    public Animator anim;

    //Pick-up-able item to add to gold "score"
    public GameObject goldPrefab;

    public BaseEnemy spawnedEnemy; //converted to spawn table

    //Can probably just be set to find the level manager on start
    public void SetLevelManager(LevelManager lm) {
        this.lm = lm;
    }

    public void SpawnEnemy() { 
        //decide what enemy to spawn
        spawnedEnemy = Instantiate(spawnedEnemy, transform.position, Quaternion.identity);
        spawnedEnemy.SetSpawnTile(this);
    }

    public void EnemyDied() {
        Debug.Log("Spawn Tile Triggered");
        spawnGold();
        //Trigger room manager that there is one fewer enemy now
        lm.EnemyDied();
    }

    //Method to spawn in gold
    //Called when enemy dies
    public void spawnGold()
    {
        Vector3 spawnPos = gameObject.transform.position;

        GameObject goldSpawned = Instantiate(goldPrefab, spawnPos + Vector3.up, Quaternion.identity);

    }

}
