using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RicWeapon : Weapon
{
    public Collider2D attackBox;
    public float damage = 10.0f;
    public float attackDuration = 0.2f;

    protected override void BaseAttackOne()
    {
        StartCoroutine(PerformAttack());
        Debug.Log("Base Attack");
    }

    protected override void ChargedAttackOne()
    {
        StartCoroutine(PerformAttack());
        Debug.Log("Charge Attack");
    }

    private IEnumerator PerformAttack()
    {
        attackBox.enabled = true;
        yield return new WaitForSeconds(attackDuration);
        attackBox.enabled = false;

        Debug.Log("Attack Finished");
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject other = collider.gameObject;
        Entity enemyEntity = collider.GetComponent<Entity>();
        if (enemyEntity != null && attackBox.enabled)
        {
            enemyEntity.TakeDamage(damage);
        }
    }
}