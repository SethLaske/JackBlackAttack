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
        if (CanSeePlayer() == true) {
            //Do these things

            //Move towards the player

            //Debug.Log("Attack") if the enemy can attack the player

            //rotate using ApplyRotation()

            //Heres a tip:
            Vector3 vectorToPlayer = player.transform.position - transform.position;
        }
    }

    
}
