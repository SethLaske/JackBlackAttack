using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Entity
{
    protected Rigidbody2D rb;

    [SerializeField] protected float moveSpeed;

    [SerializeField] private float accelerationPercent;
    [SerializeField] private float frictionPercent;

    [SerializeField] protected Transform directionalArrow;
    protected void InitializeCharacter()
    {
        InitializeEntity();
        rb = GetComponent<Rigidbody2D>();
    }


    //Added an organic and snap velocity field. This could be replaced by recombining the two methods, and adding a bool to determine snap or smooth

    protected void OrganicVelocity(Vector2 desiredVelocity)
    {
        //Debug.Log("The desired Velocity is: " + desiredVelocity);
        //if (desiredVelocity == null) { return; }

        if (desiredVelocity.x != 0)
        {
            desiredVelocity.x = rb.velocity.x + desiredVelocity.x * accelerationPercent * moveSpeed;
        }
        else
        {
            desiredVelocity.x = rb.velocity.x * (1 - frictionPercent);
        }

        if (desiredVelocity.y != 0)
        {
            desiredVelocity.y = rb.velocity.y + desiredVelocity.y * accelerationPercent * moveSpeed;
        }
        else
        {
            desiredVelocity.y = rb.velocity.y * (1 - frictionPercent);
        }

        if (Mathf.Abs(desiredVelocity.x) < .1f) desiredVelocity.x = 0;
        if (Mathf.Abs(desiredVelocity.y) < .1f) desiredVelocity.y = 0;

        if (desiredVelocity.magnitude > moveSpeed)
        {
            desiredVelocity = desiredVelocity.normalized * moveSpeed;
        }


        rb.velocity = desiredVelocity;
    }

    protected void SnapVelocity(Vector2 desiredVelocity)
    {
        rb.velocity = desiredVelocity;
    }


    protected void OrganicRotation(Vector3 directionToFace) { 
        //TODO, add a rotation speed field and slowly turn
    }

    protected void SnapRotation(Vector3 directionToFace)
    {
        if (directionToFace == Vector3.zero) return;

        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, directionToFace);
        directionalArrow.rotation = targetRotation;
    }


}
