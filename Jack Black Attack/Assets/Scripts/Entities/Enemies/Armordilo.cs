using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArmordiloState
{
    Rolling,
    Stuned,
    Searching
}

public class Armordilo : BaseEnemy
{
    private Animator animator;

    private ArmordiloState state;

    [SerializeField] private bool rolling;

    [SerializeField] private int bounces;

    [SerializeField] private float collisionDamage;

    // Start is called before the first frame update
    void Start()
    {
        EnemyStart();
        animator = gameObject.GetComponentInChildren<Animator>();
        SnapRotation(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)));
        state = ArmordiloState.Searching;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == ArmordiloState.Searching)
        {
            Search();
            return;
        }
        else if (state == ArmordiloState.Rolling)
        {
            Rolling();
            return;
        }
        else if (state == ArmordiloState.Stuned) {
            Stunned();
            return;
        }
    }

    private void Search() {
        if (CanSeePlayer() == true) {
            //Trigger rolling
            SnapVelocity((player.transform.position - transform.position).normalized * moveSpeed);
            bounces = 3;
        }

        //Look back and forth for a bit then roll
    }

    private void Rolling() { 
        //Slowly pick up speed

        //Roll and bounce off of stuff until the count hits 0, then its stunned
    }

    private void Stunned() { 
        //Just be stunned then turn around
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Entity entity = collision.GetComponent<Entity>();

        if (entity != null) {
            entity.TakeDamage(collisionDamage);
        }

        bounces--;
        if (bounces == 0) {
            //Transition to stunned
            SnapVelocity(Vector2.zero);
        }



        rb.velocity = rb.velocity * -1;

    }

}
