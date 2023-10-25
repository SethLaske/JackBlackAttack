using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootState : StateMachineBehaviour
{

    Vector3 enemyPos;

    public GameObject shootPrefab;

    public Animator anim;

    private float stateStartTime;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("movePosition");
        anim = animator;
        enemyPos = animator.transform.position;
        shootAtPlayer();
        stateStartTime = Time.time;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Time.time - stateStartTime >= 4.0f)
        {
            animator.SetTrigger("movePosition");

        }
    }

    public void shootAtPlayer()
    {
        GameObject shot = Instantiate(shootPrefab, enemyPos, Quaternion.identity);

    }

}
