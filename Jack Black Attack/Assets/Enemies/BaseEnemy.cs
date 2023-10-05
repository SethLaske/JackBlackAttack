using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : Character
{
    protected PlayerDungeonController player;

    //private Rigidbody2D rb;
    [SerializeField] protected SpawnTile spawnTile;
    [SerializeField] protected float viewDistance;
    [SerializeField] protected float fieldOfViewDegrees;

    //[SerializeField] protected float moveSpeed;
    [SerializeField] protected float attackRange;

    //[SerializeField] private float accelerationPercent; 
    //[SerializeField] private float frictionPercent;

    //[SerializeField] private Transform directionalArrow;

    protected void EnemyStart() {
        InitializeCharacter();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerDungeonController>();
        
    }


    protected bool CanSeePlayer() {
        if (player == null) {
            return false;
        }


        float distanceToPlayer = (player.transform.position - transform.position).magnitude;

        float angleToPlayer = Vector3.Angle(player.transform.position - transform.position, directionalArrow.up);
        
        //A third check will eventually be needed to ensure clean line of sight to player
        //And if any form of invisibility/stealth is ever used, additional checks might also be added

        if (distanceToPlayer < viewDistance && angleToPlayer < fieldOfViewDegrees)
        {
            return true;
        }
        else {
            return false;
        }
    }

    
    public override void Die()
    {
        //Trigger spawn tile that the enemy died
        if (spawnTile != null) {
            spawnTile.EnemyDied();
        } 

        base.Die();
    }

    
}
