using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected Animator weaponAnimator;

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

   /* public virtual void BaseAttackTwo()
    {
    }

    public virtual void ChargedAttackTwo()
    {
    }*/
}
