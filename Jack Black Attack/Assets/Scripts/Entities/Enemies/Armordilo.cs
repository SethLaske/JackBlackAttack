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

    [SerializeField] private float degreesToTurn;
    [SerializeField] private float rotationSpeed;

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
            SnapRotation(rb.velocity);
            bounces = 3;

            state = ArmordiloState.Rolling;
        }


        OrganicRotation(Mathf.Sign(degreesToTurn) * rotationSpeed * Time.deltaTime);
        //Look back and forth for a bit then roll
    }

    private void Rolling() {
        //Slowly pick up speed

        //Roll and bounce off of stuff until the count hits 0, then its stunned
        if (rb.velocity == Vector2.zero) {
            GetStunned();
        }
    }

    private void Stunned() {
        //Just be stunned then turn around
        if (Mathf.Abs(degreesToTurn) > 1) {
            OrganicRotation(Mathf.Sign(degreesToTurn) * rotationSpeed * Time.deltaTime);
            degreesToTurn -= Mathf.Sign(degreesToTurn) * rotationSpeed * Time.deltaTime;
            return;
        }

        state = ArmordiloState.Searching;
    }

    private void GetStunned() {
        SnapVelocity(Vector2.zero);

        degreesToTurn = Random.Range(55, 270);
        if (Random.Range(1, 3) % 2 == 1) {
            degreesToTurn *= -1;
        }

        state = ArmordiloState.Stuned;
    }

    private void StartSearching() { 
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Entity entity = collision.GetComponent<Entity>();

        if (entity != null) {
            entity.TakeDamage(collisionDamage);
        }

        bounces--;
        if (bounces <= 0) {
            //Transition to stunned
            GetStunned();
            return;
        }

        float rotationAngle = Random.Range(135f, 235f) * Mathf.Deg2Rad;

        Vector2 newVelocity = new Vector2();
        newVelocity.x = rb.velocity.x * Mathf.Cos(rotationAngle) + rb.velocity.y * -1 * Mathf.Sin(rotationAngle);
        newVelocity.y = rb.velocity.x * Mathf.Sin(rotationAngle) + rb.velocity.y * Mathf.Cos(rotationAngle);

        rb.velocity = new Vector2(newVelocity.x, newVelocity.y);
        SnapRotation(rb.velocity);
    }

}
