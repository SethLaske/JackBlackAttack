using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RicWeapon : Weapon
{
    //Edit the base and charged attack so they can damage the enemy in the room.
    //There are plenty of ways to do this, but the trick is to get the enemies BaseEnemy or Entity component and then use .TakeDamage(float damage) on it

    protected override void BaseAttackOne()
    {
        Debug.Log("Regular Attack");
    }

    protected override void ChargedAttackOne()
    {
        Debug.Log("Charged Attack");
    }
}
