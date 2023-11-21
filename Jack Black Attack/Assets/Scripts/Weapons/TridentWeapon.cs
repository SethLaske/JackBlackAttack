using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class TridentWeapon : Weapon
{

    [SerializeField] private float attackDuration;
    [SerializeField] private Collider2D attackBox;
    [SerializeField] private Collider2D pickupBox;
    [SerializeField] private float throwForce = 20f;
    [SerializeField] private float throwDistance = 0.25f;
    [SerializeField] private float damage = 10f;
    private Rigidbody2D rb;
    private bool hasWeapon = true;
    private Transform parent;
    private void Start()
    {
        parent = transform.parent;
        weaponAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        if(parent != null)
        {
            weaponAnimator.enabled = true;
        }
    }
    protected override void BaseAttackOne()
    {
        if (activeAttack || !hasWeapon) return;
        StartCoroutine(PerformAttack());
        SoundManager.Instance.PlaySound(SoundManager.Sounds.TridentStab);

    }
    private IEnumerator PerformAttack()
    {
        weaponAnimator.SetTrigger("Attack");
        activeAttack = true;
        attackBox.enabled = true;
        yield return new WaitForSeconds(attackDuration);
        attackBox.enabled = false;
        activeAttack = false;
    }

    protected override void ChargedAttackOne()
    {
        if (activeAttack || !hasWeapon) return;
        StartCoroutine(ThrowWeapon());
        SoundManager.Instance.PlaySound(SoundManager.Sounds.TridentThrow);
    }

    private IEnumerator ThrowWeapon()
    {
        weaponAnimator.enabled = false;
        hasWeapon = false;
        transform.SetParent(null);
        attackBox.enabled = true;
        rb.velocity = transform.rotation * new Vector2(0, throwForce);
        yield return new WaitForSeconds(throwDistance);
        rb.velocity = Vector2.zero;
        attackBox.enabled = false;
        pickupBox.enabled = true;
    }

    public void PickUpWeapon()
    {
        hasWeapon = true;
        weaponAnimator.enabled = true;
        pickupBox.enabled = false;
        transform.SetParent(parent);
        transform.localPosition = equippedPosition;
        transform.localRotation = Quaternion.identity;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasWeapon && pickupBox.enabled == true && collision.CompareTag("Player"))
        {
            PickUpWeapon();
        }

        Entity enemyEntity = collision.GetComponent<Entity>();
        if (enemyEntity != null && attackBox.enabled && !collision.CompareTag("Player"))
        {
            enemyEntity.TakeDamage(damage);
        }
    }
}
