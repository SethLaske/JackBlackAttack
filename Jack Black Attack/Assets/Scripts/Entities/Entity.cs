using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float HP;
    protected float maxHealth = 0f;
    private bool IsDead = false;    //have had previous issues of entities dying twice
    
    protected bool isMovementEnabled = true;

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
    public virtual bool TakeDamage(float damage, Vector3 attackPosition, float knockbackForce) {

        
        TakeDamage(damage);

        if (isMovementEnabled)
        {
            isMovementEnabled = false;
            // Calculate the knockback direction from the Scorpion to the player
            Vector2 direction = (transform.position - attackPosition).normalized;

            // Calculate the knockback force
            Vector2 knockback = direction * knockbackForce;

            // Apply the knockback
            GetComponent<Rigidbody2D>().AddForce(knockback, ForceMode2D.Impulse);

            StartCoroutine(EnableKnockback());
            //transform.position += 2 * (transform.position - attackPosition);

        }
        return true;
    }

    IEnumerator EnableKnockback()
    {
        yield return new WaitForSeconds(0.1f);
        isMovementEnabled = true;
    }

    //Changed by different units
    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
