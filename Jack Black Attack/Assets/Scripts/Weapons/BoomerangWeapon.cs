using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangWeapon : Weapon
{
    public Collider2D attackBox;
    public BoomerangProjectile projectilePrefab;
    public float AOEDamage = 10.0f;

    public float projectileAttackCooldown;
    
    public Transform LaunchOffset;

    protected override void BaseAttackOne()
    {
        allowAttackOne = false;
        ShootProjectile();

        StartCoroutine(ProjectileCooldown());

        //Debug.Log("Base Attack");
    }

    IEnumerator ProjectileCooldown() {
        yield return new WaitForSeconds(projectileAttackCooldown);
        allowAttackOne = true;
    }

    protected override void ChargedAttackOne()
    {
        //StartCoroutine(PerformAttack());
        allowAttackOne = false;
        weaponAnimator.SetTrigger("AttackOne");
        //Debug.Log("Charge Attack");
        activeAttack = true;
    }

    public void EndChargeAttackOne() {
        allowAttackOne = true;
        activeAttack = false;
    }
    

    private void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject other = collider.gameObject;
        Entity enemyEntity = collider.GetComponent<Entity>();
        if (enemyEntity != null && attackBox.enabled)
        {
            enemyEntity.TakeDamage(AOEDamage);
        }
    }

    private void ShootProjectile()
    {

        BoomerangProjectile projectile = Instantiate(projectilePrefab, LaunchOffset.position, transform.rotation);

        projectile.thrower = transform;
        projectile.initialDirection = transform.up;

        projectile.StartBoomerang();

        
    }
}