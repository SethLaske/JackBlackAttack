using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RicWeapon : Weapon
{
    public Collider2D attackBox;
    public GameObject projectilePrefab;
    public float damage = 10.0f;
    public AnimationClip swingAnimation;
    public float projectileSpeed = 6.0f;
    public float projectileLifetime = 2.0f;
    public Transform LaunchOffset;

    public bool canAttack = true;
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnDisable()
    {
        SetAsHeldWeapon();
    }

    private void OnEnable()
    {
        canAttack = true;
        Debug.Log("Weapon Enabling");
    }

    protected override void BaseAttackOne()
    {
        if(!activeAttack)
        {
            anim.SetTrigger("Attack");
            StartCoroutine(PerformAttack());
            //Debug.Log("Base Attack");
            SoundManager.Instance.PlaySound(SoundManager.Sounds.PoolStickSwipe);
        }

    }

    protected override void ChargedAttackOne()
    {
        ShootProjectile();
        //StartCoroutine(PerformAttack());
        //Debug.Log("Charge Attack");
    }

    private IEnumerator PerformAttack()
    {
        activeAttack = true;
        attackBox.enabled = true;
        yield return new WaitForSeconds(swingAnimation.length);
        attackBox.enabled = false;
        //Debug.Log("Attack Finished");
        activeAttack = false;
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