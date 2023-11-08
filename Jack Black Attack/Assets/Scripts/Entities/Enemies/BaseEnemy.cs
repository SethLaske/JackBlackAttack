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
     private float HPcomparison;
    private Color hurtColor = Color.white;
    public Material hurtMat;
    private Color defaultColor;
    private Material defaultMat;
    SpriteRenderer sr;
    public GameObject spriteObject; //scorpions sprite renderer is in a different object. Just drag 

    protected void EnemyStart() {
        InitializeCharacter();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerDungeonController>();
        sr = spriteObject.GetComponent<SpriteRenderer>();
        HPcomparison = HP;
        defaultMat = sr.material;
        defaultColor = sr.color;
    }


    protected bool CanSeePlayer() {
        if (player == null) {
            return false;
        }


        float distanceToPlayer = (player.transform.position - transform.position).magnitude;

        float angleToPlayer = Vector3.Angle(player.transform.position - transform.position, directionalArrow.up);
        
        //A third check will eventually be needed to ensure clean line of sight to player
        //And if any form of invisibility/stealth is ever used, additional checks might also be added

        if (distanceToPlayer < viewDistance && angleToPlayer < fieldOfViewDegrees/2)
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

    public void SetSpawnTile(SpawnTile tile) {
        spawnTile = tile;
    }

    protected virtual void OnDrawGizmos()
    {
        Quaternion initialRotation = directionalArrow.rotation;

        
        Gizmos.color = Color.magenta;


        Gizmos.DrawLine(transform.position, transform.position + (initialRotation * Quaternion.Euler(0f, 0f, -1 * (fieldOfViewDegrees/2)) * Vector3.up).normalized * viewDistance);
        Gizmos.DrawLine(transform.position, transform.position + (initialRotation * Quaternion.Euler(0f, 0f, 1 * (fieldOfViewDegrees / 2)) * Vector3.up).normalized * viewDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + viewDistance * (initialRotation * Quaternion.Euler(0f, 0f, 0f) * Vector3.up));

        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    public void damageFlash()
    {
        if (HP < HPcomparison ) //means the eney took damage
        {
            HPcomparison = HP;
            sr.material = hurtMat;
            sr.color = hurtColor;
            StartCoroutine(waitReset());
        }
    }
    IEnumerator waitReset()
    {
         yield return new WaitForSeconds(.15f);
        Debug.Log("ColorReset");
        sr.material = defaultMat;
        sr.color = defaultColor;
    }


    // freezes all movement, maybe move to baseEnemies script...
    private void DisableEnemyMovement()
    {
        StartCoroutine(waitDisableMovement());
        //moveSpeed = 0;
        
    }
     private void EnableEnemyMovement()
    {
        //no code for now
    }
    private void OnEnable()
    {
        PlayerDungeonController.onPlayerDeath += DisableEnemyMovement;
    }
    private void OnDisable()
    {
        PlayerDungeonController.onPlayerDeath -= DisableEnemyMovement;
    }

    IEnumerator waitDisableMovement()
    {   
        yield return new WaitForSeconds(2);
        moveSpeed = 0; //enemies dont freeze right away
        //this freezes movement but not attacks
    }

}
