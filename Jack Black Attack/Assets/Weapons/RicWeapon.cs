using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RicWeapon : Weapon
{
    public Collider2D attackBox;
    public GameObject projectilePrefab;
    public float damage = 10.0f;
    public float attackDuration = 0.2f;
    public float projectileSpeed = 6.0f;
    public float projectileLifetime = 2.0f;
    public Transform LaunchOffset;

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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ShootProjectile();
        }
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

    private void ShootProjectile()
    {

        GameObject projectile = Instantiate(projectilePrefab, LaunchOffset.position, transform.rotation);
        Debug.Log("Projectile");

        ProjectileBehavior projectileBehavior = projectile.GetComponent<ProjectileBehavior>();
        if (projectileBehavior != null)
        {
            //projectileBehavior.speed = projectileSpeed;
            projectileBehavior.lifetime = projectileLifetime;
            projectileBehavior.damage = damage;
        }

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = transform.rotation * new Vector3(0, projectileSpeed, 0);
    }
}