using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float attackOneChargeTime;
    public float attackTwoChargeTime;

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
