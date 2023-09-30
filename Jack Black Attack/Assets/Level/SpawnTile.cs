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

    public void Start()
    {
        StartCoroutine(spawnGoldExample());
    }

    //Method to spawn in gold
    public void spawnGold()
    {
        //Debug.Log("Spawning Gold");

        int randInt = Random.Range(1, 2);
        Vector3 spawnPos = gameObject.transform.position;

        if (randInt % 2 == 0)
        {
            spawnPos.x += randInt;
            spawnPos.y -= randInt;
        } else
        {
            spawnPos.x -= randInt;
            spawnPos.y += randInt;
        }

        GameObject goldSpawned = Instantiate(goldPrefab, spawnPos, Quaternion.identity);

    }

    public IEnumerator spawnGoldExample()
    {
        int i = 0;
        while (i < 5)
        {
            spawnGold();
            yield return new WaitForSeconds(3f);
            i += 1;
        }    

    }
}
