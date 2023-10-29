using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goToCorner : StateMachineBehaviour
{
    public float moveSpeed = 3.0f; 
    public Vector3 nearestCorner;
    public Vector3 spawnPos;

    private bool isMoving = false;
    public Vector3 closestVector;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        spawnPos = animator.transform.position;
        nearestCorner = findNearestCorner();
        isMoving = true;

        Debug.Log("spawnPos: " + spawnPos);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (isMoving)
        {
            float step = moveSpeed * Time.deltaTime;
            animator.transform.position = Vector3.MoveTowards(animator.transform.position, nearestCorner, step);

            if (Vector3.Distance(animator.transform.position, nearestCorner) < 0.01f)
            {
                animator.SetTrigger("shoot");
                isMoving = false;
            }
        }
    }

    private Vector3 findNearestCorner()
    {

        Vector3 closestVector = Droideka.vectorList[0];
        float closestDistance = Vector3.Distance(spawnPos, closestVector);

        foreach (Vector3 vector in Droideka.vectorList)
        {
            float distance = Vector3.Distance(spawnPos, vector);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestVector = vector;
            }
        }

        return closestVector;
    }
}
