using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTile : MonoBehaviour
{
    //Reference to level manager
    public LevelManager lm;

    //Animation for spawning in an enemy
    public Animator anim;

    //Pick-up-able item to add to gold "score"
    public GameObject goldPrefab;

    public void EnemyDied() {
        Debug.Log("Spawn Tile Triggered");
        spawnGold();
        //Trigger room manager that there is one fewer enemy now
    }

    //Method to spawn in gold
    //Called when enemy dies
    public void spawnGold()
    {
        Vector3 spawnPos = gameObject.transform.position;

        GameObject goldSpawned = Instantiate(goldPrefab, spawnPos, Quaternion.identity);

    }

}
