using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float HP;
    protected float maxHealth = 0f;
    private bool IsDead = false;    //have had previous issues of entities dying twice

    protected void InitializeEntity() {   
        maxHealth = HP;
    }

    public virtual bool TakeDamage(float damage)
    {
        HP -= damage;

        if (HP <= 0 && !IsDead)
        {
            IsDead = true;
            Die();
        }

        return true; // Return true if player has taken damage
    }

    //Changed by different units
    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
