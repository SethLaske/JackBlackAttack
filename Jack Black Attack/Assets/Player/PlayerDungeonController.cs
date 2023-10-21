using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDungeonController : Character
{
    // Start is called before the first frame update
    private Vector2 inputDirection;

    //private Rigidbody2D rb;
    //private Vector2 setVelocityVector;

    //[SerializeField] private float moveSpeed;

    //[SerializeField] private float accelerationPercent; //used for the player to speed up
    //[SerializeField] private float frictionPercent;     //used for the player to slow to 0

    //[SerializeField] private Transform directionalArrow;
    private Vector3 mousePos;
    private Vector3 directionToFace;

    [SerializeField] private bool useMouseToRotate;

    public Weapon activeWeapon;
    private float attackOneTimer;
    private bool isBlocking;
    private bool isHoldingShield;
    private float shieldHoldDuration = 5.0f;
    private bool shieldOnCooldown = false;
    private float shieldCooldownDuration = 10.0f;
    private float timeShieldHeld = 0.0f;


    // Roll Variables
    [SerializeField] private bool roll = false;
    [SerializeField] private bool canRoll = true;
    [SerializeField] private float rollDistance;
    [SerializeField] private float rollCooldown;
    [SerializeField] private float rollDuration;
    private Vector2 rollDirection;
    
    public bool alive;
    public static event Action onPlayerDeath;
    void Start()
    {
        alive = true;//can set to false above any script we want to disable after death, like check attacks
        InitializeCharacter();
        inputDirection = Vector2.zero;
        rollDirection = Vector2.zero;
        isBlocking = false;
        //rb = GetComponent<Rigidbody2D>();
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
        RotateArrow();
    }

    private void CheckUserInput()
    {
        inputDirection.x = Input.GetAxisRaw("Horizontal");
        inputDirection.y = Input.GetAxisRaw("Vertical");


        Vector3 mousePosition = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10f));
        //Check for attacks/other inputs

        // Roll
        if (Input.GetKeyDown(KeyCode.LeftShift) && canRoll == true && inputDirection != Vector2.zero)
        {
            rollDirection = inputDirection.normalized;
            Debug.Log("Roll Enabled");
            StartCoroutine(Roll());
        }
        // Block
        if (Input.GetMouseButtonDown(1) && !shieldOnCooldown)
        {
            isBlocking = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            isBlocking = false;
            isHoldingShield = false;
            timeShieldHeld = 0.0f;
        }
        if (isBlocking)
        {
            isHoldingShield = true;
            timeShieldHeld += Time.deltaTime;
        }
        if (timeShieldHeld >= shieldHoldDuration)
        {
            isBlocking = false;
            isHoldingShield = false;
            StartCoroutine(ShieldCooldown());
        }
    }

    private void ApplyMovement() {
        if (!isBlocking)
        {
            OrganicVelocity(inputDirection);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
        //Debug.Log("The user input direction is: " + inputDirection);
        OrganicVelocity(inputDirection);
        /*//X Velocity
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


        rb.velocity = setVelocityVector;*/
        if (roll)
        {
            rb.velocity = rollDirection * (rollDistance/rollDuration);
        }
    }

    private void RotateArrow() {
        if (useMouseToRotate == true)
        {
            directionToFace = mousePos - transform.position;


        }
        else {
            if (Input.GetKey(KeyCode.LeftShift) == false) { 
                directionToFace = rb.velocity;
            }
        }

        /*if (directionToFace == Vector3.zero) return;

        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, directionToFace);
        directionalArrow.rotation = targetRotation;*/
        SnapRotation(directionToFace);

    }

    private void CheckAttacks() {
        if (activeWeapon == null) {
            return;
        }
        if (!isBlocking)
        {

            if (Input.GetMouseButtonDown(0))
            {
                attackOneTimer = 0;
            }

            if (Input.GetMouseButton(0))
            {
                attackOneTimer += Time.deltaTime;
            }

            if (Input.GetMouseButtonUp(0))
            {
                activeWeapon.UseAttackOne(attackOneTimer);
                attackOneTimer = 0;
            }
        }
    }
 
    private IEnumerator Roll()
    {
        canRoll = false;
        roll = true;
        yield return new WaitForSeconds(rollDuration); // Roll duration of 0.08
        roll = false;
        yield return new WaitForSeconds(rollCooldown); // Roll Cooldown of 2 Seconds
        canRoll = true;
    }


    public override bool TakeDamage(float damage)
    {

        if (gameObject.tag == "Player" && !isBlocking && roll == false)
        {
            HP -= damage;
            if (HP <= 0)
            {
                Die();
            }
            return true;
        }
        else if (isBlocking)
        {
            return false;
        }
        else
        {
            return false;
        }
    }
    private IEnumerator ShieldCooldown()
    {
        shieldOnCooldown = true;
        yield return new WaitForSeconds(shieldCooldownDuration);
        shieldOnCooldown = false;
    }

    public override void Die()
    {
        Debug.Log("YOU DIED");
        onPlayerDeath?.Invoke();

    }

     private void DisablePlayerMovement()
    {
        alive = false;
        moveSpeed = 0;
    }
     private void EnablePlayerMovement()
    {
        alive = true;
    }
    private void OnEnable()
    {
        PlayerDungeonController.onPlayerDeath += DisablePlayerMovement;
    }
    private void OnDisable()
    {
        PlayerDungeonController.onPlayerDeath -= DisablePlayerMovement;
    }
}
