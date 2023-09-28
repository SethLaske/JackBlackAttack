using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : Entity
{
    protected PlayerDungeonController player;

    private Rigidbody2D rb;
    [SerializeField] protected SpawnTile spawnTile;
    [SerializeField] protected float fieldOfViewDegrees;

    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float attackRange;

    [SerializeField] private float accelerationPercent; 
    [SerializeField] private float frictionPercent;

    [SerializeField] private Transform directionalArrow;

    protected void EnemyStart() {
        InitializeEntity();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerDungeonController>();
        rb = GetComponent<Rigidbody2D>();
    }


    protected bool CanSeePlayer() {
        return (player != null);
    }

    protected void ApplyVelocity(Vector2 desiredVelocity) {
        if (desiredVelocity.x != 0)
        {
            desiredVelocity.x = rb.velocity.x + desiredVelocity.x * accelerationPercent * moveSpeed;
        }
        else
        {
            desiredVelocity.x = rb.velocity.x * (1 - frictionPercent);
        }

        if (desiredVelocity.y != 0)
        {
            desiredVelocity.y = rb.velocity.y + desiredVelocity.y * accelerationPercent * moveSpeed;
        }
        else
        {
            desiredVelocity.y = rb.velocity.y * (1 - frictionPercent);
        }

        if (Mathf.Abs(desiredVelocity.x) < .1f) desiredVelocity.x = 0;
        if (Mathf.Abs(desiredVelocity.y) < .1f) desiredVelocity.y = 0;

        if (desiredVelocity.magnitude > moveSpeed)
        {
            desiredVelocity = desiredVelocity.normalized * moveSpeed;
        }


        rb.velocity = desiredVelocity;
    }

    public override void Die()
    {
        //Trigger spawn tile that the enemy died
        if (spawnTile != null) {
            spawnTile.EnemyDied();
        } 

        base.Die();
    }

    protected void ApplyRotation(Vector3 directionToFace) {
        if (directionToFace == Vector3.zero) return;

        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, directionToFace);
        directionalArrow.rotation = targetRotation;
    }
}
