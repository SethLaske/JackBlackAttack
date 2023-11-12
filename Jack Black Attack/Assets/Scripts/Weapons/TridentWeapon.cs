using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class TridentWeapon : Weapon
{

    [SerializeField] private float attackDuration;
    [SerializeField] private Collider2D attackBox;
    [SerializeField] private GameObject throwableTrident;
    private Rigidbody2D rb;
    private bool hasWeapon = true;
    private void Start()
    {
        weaponAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    protected override void BaseAttackOne()
    {
        if (activeAttack) return;
        StartCoroutine(PerformAttack());

    }
    private IEnumerator PerformAttack()
    {
        weaponAnimator.enabled = true;
        weaponAnimator.SetTrigger("Attack");
        activeAttack = true;
        attackBox.enabled = true;
        yield return new WaitForSeconds(attackDuration);
        attackBox.enabled = false;
        activeAttack = false;
        weaponAnimator.enabled = false;
    }

    protected override void ChargedAttackOne()
    {
        ThrowWeapon();
    }

    private void ThrowWeapon()
    {
        hasWeapon = false;
        ThrowableTrident _thrownTrident = Instantiate(throwableTrident, transform.position, transform.parent.rotation).GetComponent<ThrowableTrident>();
        _thrownTrident.tridentParent = this;
    }

    public void PickUpWeapon(Transform _parent)
    {
        hasWeapon = true;
        transform.SetParent(_parent);
    }
}
