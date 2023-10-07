using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpionAnimatorManager : MonoBehaviour
{
    private Scorpion scorpion;
    void Start()
    {
        scorpion = gameObject.GetComponentInParent<Scorpion>();
    }

    public void UsePincer() {
        scorpion.UsePincerAttack();
    }


    public void EndAttack() {
        scorpion.ReturnToIdle();
    }
}
