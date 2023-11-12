using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected Animator weaponAnimator;
    public Vector3 equippedPosition;
    public string weaponName;

    public bool activeAttack = false;

    public float attackOneChargeTime;
    public bool allowAttackOne;
    public float attackTwoChargeTime;
    public bool allowAttackTwo;

    private void Start()
    {
        weaponAnimator = GetComponent<Animator>();
    }
    public void UseAttackOne(float chargedTime) {
        if (chargedTime > attackOneChargeTime)
        {
            ChargedAttackOne();
        }
        else {
            BaseAttackOne();
        }
    }
    protected virtual void BaseAttackOne() 
    { 
    }

    protected virtual void ChargedAttackOne()
    {
    }

    public void SetAsItem() {
        Vector3 weaponWorldPos = transform.position;
        transform.SetParent(null);
        transform.position = weaponWorldPos;
        gameObject.layer = 10;         //I AM AWARE I SHOULD NOT HARD CODE THIS, but it is the current layer for items that default doesn't interact with
        GetComponent<CircleCollider2D>().enabled = true;
    }

    public void SetAsHeldWeapon() {
        transform.localPosition = equippedPosition;
        transform.localRotation = Quaternion.identity;
        gameObject.layer = 0;           //I AM AWARE I SHOULD NOT HARD CODE THIS, but it is the current layer for items that default doesn't interact with
        GetComponent<CircleCollider2D>().enabled = false;
    }
   /* public virtual void BaseAttackTwo()
    {
    }

    public virtual void ChargedAttackTwo()
    {
    }*/
}
