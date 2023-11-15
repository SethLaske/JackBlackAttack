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

    [HideInInspector] public Weapon activeWeapon;
    private float attackOneTimer;
    private bool isBlocking;
   private bool isHoldingShield;
    private float shieldHoldDuration = 5.0f;
    private bool shieldOnCooldown = false;
    private float shieldCooldownDuration = 10.0f;
    private float timeShieldHeld = 0.0f;
    [SerializeField] private GameObject shield;

    // Roll Variables
    [SerializeField] private bool roll = false;
    [SerializeField] private bool canRoll = true;
    [SerializeField] private float rollDistance;
    [SerializeField] private float rollCooldown;
    [SerializeField] private float rollDuration;
    private Vector2 rollDirection;
    
    public bool alive;
    public static event Action onPlayerDeath;
    public static event Action<float> onTakeDamage;

    private Animator animator;
    private SpriteRenderer sr;
    void Awake()
    {
        alive = true;//can set to false above any script we want to disable after death, like check attacks
        InitializeCharacter();
        inputDirection = Vector2.zero;
        rollDirection = Vector2.zero;
        isBlocking = false;
        animator = gameObject.GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        shield.SetActive(false);

        InitWeapon();
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
        UpdateAnimator();
    }

    private void CheckUserInput()
    {
        inputDirection.x = Input.GetAxisRaw("Horizontal");
        inputDirection.y = Input.GetAxisRaw("Vertical");


        Vector3 mousePosition = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10f));
        //Check for attacks/other inputs

        // Roll
        if (Input.GetKeyDown(KeyCode.LeftShift) && canRoll == true && inputDirection != Vector2.zero && !isAttacking())
        {
            rollDirection = inputDirection.normalized;
            //Debug.Log("Roll Enabled");
            animator.SetTrigger("Roll");
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


        if (Input.GetKeyDown(KeyCode.E)) {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, .75f);
            foreach (Collider2D collider in colliders)
            {
                Weapon testWeapon = collider.GetComponent<Weapon>();
                if (testWeapon != null && testWeapon != activeWeapon)
                {
                    DropWeapon();
                    AddWeapon(testWeapon);
                    Destroy(collider.gameObject);
                    break;
                }

            }
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

        SnapRotation(directionToFace);

    }

    private void CheckAttacks() {
        if (activeWeapon == null) {
            return;
        }
        if (!isBlocking && !roll)
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

        if (Input.GetKeyDown(KeyCode.K)) {
            StartCoroutine(KillAll());
        }
    }
 
    private IEnumerator Roll()
    {
        canRoll = false;
        roll = true;
        directionalArrow.gameObject.SetActive(false);
        yield return new WaitForSeconds(rollDuration); // Roll duration of 0.08
        directionalArrow.gameObject.SetActive(true);
        roll = false;
        yield return new WaitForSeconds(rollCooldown); // Roll Cooldown of 2 Seconds
        canRoll = true;
    }


    public override bool TakeDamage(float damage)
    {

        if (gameObject.tag == "Player" && !isBlocking && roll == false)
        {
            HP -= damage;
            onTakeDamage?.Invoke(HP);
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

    private void UpdateAnimator() {
        animator.SetInteger("XMovement", (int)inputDirection.x);
        animator.SetInteger("YMovement", (int)inputDirection.y);
        
        if (inputDirection.x < 0)
        {
            sr.flipX = true;
            //Debug.Log("Flipping sprite");
        }
        else {
            sr.flipX = false;
        }

        shield.SetActive(isBlocking);
    }

    IEnumerator KillAll() {
        float radius = 1f;

        while (radius < 100) {

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
            foreach (Collider2D collider in colliders)
            {

                if (collider.gameObject != this.gameObject) {
                    Entity entity = collider.GetComponent<Entity>();
                    if (entity != null) { 
                        entity.TakeDamage(100);
                    }
                }
            }

            radius++;
            yield return null;
        }
    
    }

    private bool isAttacking() {
        if (activeWeapon == null) {
            return false;
        }

        if (activeWeapon.activeAttack == true) {
            return true;
        }

        return false;
    }
    private void InitWeapon() {
        string weaponName = PlayerPrefs.GetString("ChosenWeapon");

        if (!string.IsNullOrEmpty(weaponName))
        {
            GameObject weaponPrefab = Resources.Load<GameObject>("Weapons/" + weaponName);

            if (weaponPrefab == null)
            {
                return;
            }

            Weapon weapon = weaponPrefab.GetComponent<Weapon>();

            if (weapon != null)
            {
                AddWeapon(weapon);
            }
        }
    }
    private void AddWeapon(Weapon newWeapon) {
        if (activeWeapon != null) {
            Destroy(activeWeapon);
        }

        //Instantiating in the case the weapon hasnt been instantiated already
        activeWeapon = Instantiate(newWeapon, directionalArrow);
        activeWeapon.SetAsHeldWeapon();

        PlayerPrefs.SetString("ChosenWeapon", activeWeapon.weaponName);
    }

    private void DropWeapon() {
        if (activeWeapon == null) {
            return;
        }
        activeWeapon.SetAsItem();
        activeWeapon = null;
    }
}
