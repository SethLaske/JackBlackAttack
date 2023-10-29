using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JaniceEnemy : BaseEnemy
{
    private bool CanAttack = false;
    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        EnemyStart();
        rb2d = GetComponent<Rigidbody2D>();

    }


    // Update is called once per frame
    void Update()
    {
        if (CanSeePlayer() == true)
        {
            //Do these things

            Vector2 direction = (player.transform.position - transform.position).normalized;
            rb2d.velocity = direction * moveSpeed;
            //Move towards the player

            if (Vector2.Distance(transform.position, player.transform.position) <= attackRange)
            { CanAttack = true;
                rb2d.velocity = Vector2.zero; }
            else
            { CanAttack = false; }
            //Debug.Log("Attack" + CanAttack);
            //Debug.Log("Attack") if the enemy can attack the player

            directionalArrow.transform.up = player.transform.position - transform.position;
            //rotate using ApplyRotation()
        }
    }
}
