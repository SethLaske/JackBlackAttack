using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveToNextPos : StateMachineBehaviour
{
    public Vector3 moveGoal;
    private bool hasReachedGoal = false;
    public float moveSpeed = 6.0f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("shoot");
        moveGoal = Droideka.vectorList[Random.Range(0, 3)];
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!hasReachedGoal)
        {
            float step = moveSpeed * Time.deltaTime;
            animator.transform.position = Vector3.MoveTowards(animator.transform.position, moveGoal, step);

            if (Vector3.Distance(animator.transform.position, moveGoal) < 0.001f)
            {
                hasReachedGoal = true;
                // Set trigger to return to "shootState" once target position is reached
                animator.SetTrigger("shoot");
            }
        }
    }

    // This method is called when the animation transition is about to start
    // Here, you can reset the flag for the next cycle
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hasReachedGoal = false;
    }
}
