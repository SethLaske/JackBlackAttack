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
    private GameObject drop;

    private BaseEnemy spawnedEnemy; //converted to spawn table
    [SerializeField] private Transform enemySpawnPosition;

    //Can probably just be set to find the level manager on start
    public void SetLevelManager(LevelManager lm) {
        this.lm = lm;
    }

    public void SetEnemy(BaseEnemy enemy) {
        spawnedEnemy = enemy;

        if (enemy.dropTable != null) {
            drop = enemy.dropTable.GetDrop();
        }
    }

    public void ActivateSpawnSequence() {
        //Rise platform
        spawnedEnemy = Instantiate(spawnedEnemy, enemySpawnPosition);
        spawnedEnemy.SetSpawnTile(this);
        spawnedEnemy.enabled = false;
        foreach (SpriteRenderer sr in spawnedEnemy.GetComponentsInChildren<SpriteRenderer>()) { 
            sr.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        }

        foreach (CircleCollider2D cc in spawnedEnemy.GetComponentsInChildren<CircleCollider2D>())
        {
            cc.enabled = false;
        }


        anim.SetTrigger("Raise");
        
    }

    public void EnableSpawnedEnemies() {
        spawnedEnemy.enabled = true;
        spawnedEnemy.transform.SetParent(null);
        foreach (SpriteRenderer sr in spawnedEnemy.GetComponentsInChildren<SpriteRenderer>())
        {
            sr.maskInteraction = SpriteMaskInteraction.None;
        }
        foreach (CircleCollider2D cc in spawnedEnemy.GetComponentsInChildren<CircleCollider2D>())
        {
            cc.enabled = true;
        }
    }

    public void ActivateDestroySequence() {
        //Lower Platform
        anim.SetTrigger("Lower");
    }
    public void FinishDestroySequence() {
        Destroy(gameObject);
    }

    public void SpawnEnemy() { 
        //decide what enemy to spawn
        spawnedEnemy = Instantiate(spawnedEnemy, transform.position, Quaternion.identity);
        spawnedEnemy.SetSpawnTile(this);
    }

    public void EnemyDied() {
        //Debug.Log("Spawn Tile Triggered");
        spawnDrop();
        //Trigger room manager that there is one fewer enemy now
        lm.EnemyDied();
    }

    //Method to spawn in gold
    //Called when enemy dies
    public void spawnDrop()
    {
        if (drop == null) {
            return;
        }

        Vector3 spawnPos = gameObject.transform.position;
        Instantiate(drop, enemySpawnPosition.position + (.5f * Vector3.up), Quaternion.identity);

    }

}
