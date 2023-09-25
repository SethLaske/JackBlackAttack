using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JaniceEnemy : BaseEnemy
{
    // Start is called before the first frame update
    void Start()
    {
        EnemyStart();
    }


    // Update is called once per frame
    void Update()
    {
        if (CanSeePlayer() == true)
        {
            //Do these things

            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
            //Move towards the player

            if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
            { Debug.Log("Attack"); }
            //Debug.Log("Attack") if the enemy can attack the player

            transform.up = player.transform.position - transform.position;
            //rotate using ApplyRotation()
        }
    }
}
