using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDungeonController : Entity
{
    // Start is called before the first frame update
    private Vector2 inputDirection;

    private Rigidbody2D rb;
    private Vector2 setVelocityVector;

    [SerializeField] private float moveSpeed;

    [SerializeField] private float accelerationPercent; //used for the player to speed up
    [SerializeField] private float frictionPercent;     //used for the player to slow to 0

    [SerializeField] private Transform directionalArrow;
    private Vector3 mousePos;
    private Vector3 directionToFace;

    [SerializeField] private bool useMouseToRotate;

    public Weapon activeWeapon;
    private float attackOneTimer;

    void Start()
    {
        InitializeEntity();
        inputDirection = Vector2.zero;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckUserInput();
        CheckAttacks();
        //check surroundings
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        ApplyRotation();
    }

    private void CheckUserInput() {
        inputDirection.x = Input.GetAxisRaw("Horizontal");
        inputDirection.y = Input.GetAxisRaw("Vertical");


        Vector3 mousePosition = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10f));
        //Check for attacks/other inputs
    }

    private void ApplyMovement() {
        //X Velocity
        if (inputDirection.x != 0)
        {
            setVelocityVector.x = rb.velocity.x + inputDirection.x * accelerationPercent * moveSpeed;
        }
        else {
            setVelocityVector.x = rb.velocity.x * (1 - frictionPercent);
        }

        //Y Velocity
        if (inputDirection.y != 0)
        {
            setVelocityVector.y = rb.velocity.y + inputDirection.y * accelerationPercent * moveSpeed;
        }
        else
        {
            setVelocityVector.y = rb.velocity.y * (1 - frictionPercent);
        }
        
        if (Mathf.Abs(setVelocityVector.x) < .1f) setVelocityVector.x = 0;
        if (Mathf.Abs(setVelocityVector.y) < .1f) setVelocityVector.y = 0;

        if (setVelocityVector.magnitude > moveSpeed) {
            setVelocityVector = setVelocityVector.normalized * moveSpeed;
        }


        rb.velocity = setVelocityVector;
    }

    private void ApplyRotation() {
        if (useMouseToRotate == true)
        {
            directionToFace = mousePos - transform.position;


        }
        else {
            if (Input.GetKey(KeyCode.LeftShift) == false) { 
                directionToFace = rb.velocity;
            }
        }

        if (directionToFace == Vector3.zero) return;

        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, directionToFace);
        directionalArrow.rotation = targetRotation;


    }

    private void CheckAttacks() {
        if (activeWeapon == null) {
            return;
        }

        if (Input.GetMouseButtonDown(0)) {
            attackOneTimer = 0;
        }

        if (Input.GetMouseButton(0)) {
            attackOneTimer += Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(0)) {
            activeWeapon.UseAttackOne(attackOneTimer);
            attackOneTimer = 0;
        }
    }
}
