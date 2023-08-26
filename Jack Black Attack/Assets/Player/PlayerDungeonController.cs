using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDungeonController : Entity
{
    // Start is called before the first frame update
    private Vector2 inputDirection;

    private Rigidbody2D rigidbody;
    private Vector2 setVelocityVector;

    [SerializeField] private float moveSpeed;

    [SerializeField] private float accelerationPercent;
    [SerializeField] private float frictionPercent;

    void Start()
    {
        InitializeEntity();
        inputDirection = Vector2.zero;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckUserInput();
        //check surroundings
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    private void CheckUserInput() {
        inputDirection.x = Input.GetAxisRaw("Horizontal");
        inputDirection.y = Input.GetAxisRaw("Vertical");

        //Check for attacks/other inputs
    }

    private void ApplyMovement() {
        //X Velocity
        if (inputDirection.x != 0)
        {
            setVelocityVector.x = rigidbody.velocity.x + inputDirection.x * accelerationPercent * moveSpeed * (1 - frictionPercent);
        }
        else {
            setVelocityVector.x = rigidbody.velocity.x * (1 - frictionPercent);
        }

        //Y Velocity
        if (inputDirection.y != 0)
        {
            setVelocityVector.y = rigidbody.velocity.y + inputDirection.y * accelerationPercent * moveSpeed * (1 - frictionPercent);
        }
        else
        {
            setVelocityVector.y = rigidbody.velocity.y * (1 - frictionPercent);
        }
        
        if (Mathf.Abs(setVelocityVector.x) < .1f) setVelocityVector.x = 0;
        if (Mathf.Abs(setVelocityVector.y) < .1f) setVelocityVector.y = 0;

        if (setVelocityVector.magnitude > moveSpeed) {
            setVelocityVector = setVelocityVector.normalized * moveSpeed;
        }


        rigidbody.velocity = setVelocityVector;
    }
}
