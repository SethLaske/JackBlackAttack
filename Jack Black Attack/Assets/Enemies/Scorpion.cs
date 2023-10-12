using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScorpionState
{
    Idle,
    TailAttack,
    PincerAttack
}

public class Scorpion : BaseEnemy
{
    [SerializeField] private float pincerDamage;

    [SerializeField] private float tailRange;
    [SerializeField] private float tailPullSpeed;

    private Animator animator;

    private ScorpionState state;

    private float lastDirection = 1;

    void Start()
    {
        EnemyStart();
        animator = gameObject.GetComponentInChildren<Animator>();
        state = ScorpionState.Idle;
        SnapRotation(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)));    //Face a rando direction at the start
    }


    // Update is called once per frame
    void Update()
    {

        if (state == ScorpionState.PincerAttack)
        {
            SnapVelocity(Vector2.zero);
            return;
        }
        
        if (state == ScorpionState.TailAttack) {
            player.transform.position = Vector3.MoveTowards(player.transform.position, transform.position, tailPullSpeed * Time.deltaTime);
            Debug.Log("Tail Attack");
        }


        if (CanSeePlayer() == true)
        {
            Vector3 vectorToPlayer = player.transform.position - transform.position;

            SnapRotation(vectorToPlayer);


            if (vectorToPlayer.magnitude < attackRange)
            {
                state = ScorpionState.PincerAttack;
                SnapVelocity(Vector3.zero);
                //Attack
                
                animator.SetBool("PincerAttack", true);




            }
            else if (vectorToPlayer.magnitude < tailRange)
            {
                state = ScorpionState.TailAttack;
                OrganicVelocity(Vector3.zero);
                animator.SetBool("TailAttack", true);
                
            }
            else
            {
                ReturnToIdle();

                OrganicVelocity(vectorToPlayer);
            }

        }
        else {
            //Wander
            float direction = Mathf.Sign(Random.Range(-1, 16));     //1 in 15 chance to turn in a different direction
            if (direction != 0) lastDirection = direction * lastDirection;
            float rotation = Random.Range(0, 100);
            if (rotation > 3) {
                rotation = 0;
            }

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + .5f * (directionalArrow.rotation * Vector3.up).normalized, .25f);
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject != gameObject)
                {
                    rotation = Random.Range(-1, 2) * 30;
                    Debug.Log("Something else infront");

                    break;
                }
            }

            Vector3 wanderMove = (directionalArrow.rotation * Quaternion.Euler(0f, 0f, lastDirection * direction * rotation) * Vector3.up).normalized * (moveSpeed/2);

            OrganicVelocity(wanderMove);
            SnapRotation(wanderMove);
        }
    }

    public void UsePincerAttack() {
        Debug.Log("Using Attack");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + attackRange * (directionalArrow.rotation * Vector3.up).normalized, .25f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject == player.gameObject)
            {
                player.TakeDamage(pincerDamage);

            }
        }
    }

    public void ReturnToIdle() {
        state = ScorpionState.Idle;
        animator.SetBool("TailAttack", false);
        animator.SetBool("PincerAttack", false);
    }


    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, tailRange);
    }


}
