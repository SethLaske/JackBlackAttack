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

    //Overloading to create knockback
    /// <summary>
    /// Overload TakeDamage to pass in an attack position which can be used to apply knockback or to calculate the shield
    /// </summary>
    public virtual bool TakeDamage(float damage, Vector3 attackPosition) {

        TakeDamage(damage);

        transform.position += 2 * (transform.position - attackPosition);

        return true;
    }

    //Changed by different units
    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
