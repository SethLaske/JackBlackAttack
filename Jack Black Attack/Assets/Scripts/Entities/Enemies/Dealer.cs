using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DealerState
{
    Idle,       //Approaching Player
    Flee,       //Running from player
    Shuffle,    //Shuffling new deck
    MeleeAttack,    //Using 1 deck for melee attack
    RangeAttack,    //Using 2 decks for range attack
    FanAttack       //Using 4 decks for fan attack
}


public class Dealer : BaseEnemy
{
    private Animator animator;
    private DealerState state;

    [Header("Deck Stats")]
    [SerializeField] private List<GameObject> decks;
    [SerializeField] private int deckCount;
    private const int maxDeckCount = 4;
    [SerializeField] private float deckDistanceFromCenter;  //Might change this based on player distance (wider when player is further)
    [SerializeField] private float deckRotationSpeed;   //Might change to reflect the HP (faster at low HP)
    [SerializeField] private DealerDeckPool deckPool;
    [SerializeField] private Transform deckParent;

    private float chargeTimer;

    [Header("Flee Stats")]
    private Vector3 fleeDestination;
    [SerializeField] private float playerCutoffAngle;

    [Header("Shuffle Stats")]
    [SerializeField] private float shuffleTime;

    [Header("Melee Stats")]
    [SerializeField] private int meleeDeckCost;
    [SerializeField] private float meleeChargeTime;
    [SerializeField] private float meleeDamage;

    [Header("Ranged Stats")]
    [SerializeField] private int rangedDeckCost;
    [SerializeField] private float rangedChargeTime;
    //[SerializeField] private float meleeDamage;
    [SerializeField] private ParticleSystem rangedAttack;

    void Start()
    {
        EnemyStart();
        animator = gameObject.GetComponentInChildren<Animator>();
        SnapRotation(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)));    //Face a rando direction at the start
        state = DealerState.Idle;

        AddDeck();
        decks[0].transform.RotateAround(transform.position, Vector3.forward, 180);
        AddDeck();

    }

    
    void Update()
    {
        damageFlash();

        switch (state) {
            case (DealerState.Idle):
                Idle();
                break;
            case (DealerState.Flee):
                Flee();
                break;
            case (DealerState.Shuffle):
                Shuffle();
                break;
            case (DealerState.MeleeAttack):
                Melee();
                break;
            case (DealerState.RangeAttack):
                Ranged();
                break;
        }
    }

    private void FixedUpdate()
    {
        SpinDeck();
    }

    private void EnterIdle() {
        state = DealerState.Idle;
    }
    private void Idle() {
        if (decks.Count <= 0) {
            EnterFlee();
            return;
        }

        if (CanSeePlayer())
        {
            OrganicVelocity(player.transform.position - transform.position);
            SnapRotation(player.transform.position - transform.position);

            if (Vector2.Distance(transform.position, player.transform.position) < attackRange)
            {
                EnterMelee();
            }
            else {
                EnterRanged();
            }
        }

        else if (decks.Count < 4) {
            EnterShuffle();
            return;
        }
    }

    private void SpinDeck() {
        for (int i = 0; i < decks.Count; i++) {
            float angleToNextDeck = 1;

            if (decks.Count > 1) {
                //angleToNextDeck = Vector2.Angle(decks[i].transform.position - transform.position, decks[(i+1)%decks.Count].transform.position - transform.position);
                angleToNextDeck = Mathf.Atan2(decks[i].transform.localPosition.y, decks[i].transform.localPosition.x) - Mathf.Atan2(decks[i].transform.localPosition.y, decks[i].transform.localPosition.x);
                if (angleToNextDeck < 0) {
                    angleToNextDeck += 360;
                }
            }

            //Debug.Log("The angle for card: " + i + " to the next card is: " + angleToNextDeck);
            decks[i].transform.RotateAround(transform.position, Vector3.forward, Mathf.Max(angleToNextDeck/(360/decks.Count), 1) * deckRotationSpeed * Time.deltaTime);
            //decks[i].transform.RotateAround(transform.position, Vector3.forward, deckRotationSpeed * Time.deltaTime); 
        }    
    }

    // Will only be called when the deck is actually subtracted in an attack
    private void EnterFlee() {
        state = DealerState.Flee;

        GameObject randomEnemy = FindObjectOfType<BaseEnemy>().gameObject;

        fleeDestination = new Vector3();

        if (randomEnemy != null)
        {
            fleeDestination = randomEnemy.transform.position;
        }
        else {
            fleeDestination = Vector3.zero;

            int randomValue = (Random.Range(0, 2) == 0) ? 1 : -1;
            if (CanSeePlayer()) {

                Vector2 direction = player.transform.position - transform.position;
                direction = new Vector2(randomValue * direction.y, randomValue * -1 * direction.x);

                RaycastHit2D hit = Physics2D.Raycast(player.transform.position, direction);

                // Check if the ray hits something
                if (hit.collider != null)
                {
                    // Get the position where the ray makes contact
                    fleeDestination = hit.point - direction.normalized * -1;
                }

               
            }
        }

        //Need more enemies and cases for when this is the last enemy. Just using this to test for now
        //fleeDestination = Vector3.zero;

        //Debug.Log("Dealer entering flee to: " + fleeDestination);

        SnapVelocity((fleeDestination - transform.position).normalized * moveSpeed * 1.5f);
        SnapRotation((fleeDestination - transform.position));
    
    }

    private void Flee() {
        if (Vector2.Distance(fleeDestination, transform.position) < 1) {
            
            if (decks.Count <= 0) {
                EnterShuffle();
                return;
            }

            
            EnterIdle();
            return;
        }

        //Debug.Log("Angle between player line and flee line: " + Vector2.Angle(fleeDestination - transform.position, player.transform.position - transform.position));
        if (Vector2.Angle(fleeDestination - transform.position, player.transform.position - transform.position) > playerCutoffAngle) {
            EnterFlee();
            return;
        }

        SnapVelocity(fleeDestination.normalized * moveSpeed * 1.5f);
        SnapRotation(fleeDestination);
    }

    private void EnterShuffle() {
        //Debug.Log("Starting to shuffle");
        state = DealerState.Shuffle;

        chargeTimer = shuffleTime;

        SnapVelocity(Vector2.zero);
        //SnapRotation(-1 * (player.transform.position - transform.position));
    }

    private void Shuffle() {
        SnapVelocity(Vector2.zero);
        if (isChargeDone()) {

            AddDeck();
            EnterIdle();
            return;
        }
    }

    private void EnterMelee() {
        if (decks.Count < meleeDeckCost) {
            //Cant melee, return to idle
            EnterIdle();
            return;
        }

        //Debug.Log("Starting to melee");
        state = DealerState.MeleeAttack;

        chargeTimer = meleeChargeTime;

        SnapVelocity(Vector2.zero);
        SnapRotation(1 * (player.transform.position - transform.position));
    }

    private void Melee() {
        if (isChargeDone())
        {

            RemoveDeck();
            EndMelee();
        }
        SnapVelocity(Vector2.zero);
    }

    private void EndMelee() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + attackRange * (directionalArrow.rotation * Vector3.up).normalized, 1f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject == player.gameObject)
            {
                //player.TakeDamage(pincerDamage);
                // Knockback TakeDamage
                player.TakeDamage(meleeDamage, transform.position, 0);

            }
        }
        SoundManager.Instance.PlaySound(SoundManager.Sounds.DealerAttack);




        EnterIdle();
    }

    private void EnterRanged()
    {
        if (decks.Count < rangedDeckCost)
        {
            //Cant melee, return to idle
            EnterIdle();
            return;
        }

        //Debug.Log("Starting to melee");
        state = DealerState.RangeAttack;

        chargeTimer = rangedChargeTime;

        SnapVelocity(Vector2.zero);
        SnapRotation(1 * (player.transform.position - transform.position));
    }

    private void Ranged()
    {
        if (isChargeDone())
        {

            RemoveDeck();
            RemoveDeck();
            EndRanged();
        }
        SnapVelocity(Vector2.zero);
    }

    private void EndRanged()
    {
        rangedAttack.Play();
        SoundManager.Instance.PlaySound(SoundManager.Sounds.DealerAttack);



        EnterIdle();
    }

    private void AddDeck() {
        deckCount++;

        GameObject addedDeck = deckPool.GetDeckFromPool();
        addedDeck.transform.SetParent(deckParent);
        addedDeck.transform.rotation = transform.rotation;
        addedDeck.transform.localPosition = new Vector2(0, deckDistanceFromCenter);
        decks.Add(addedDeck);


    }

    private void RemoveDeck() {
        deckCount--;

        if (decks.Count > 0) {
            deckPool.AddDeckToPool(decks[0]);
            decks.RemoveAt(0);
        }
    }

    private bool isChargeDone() {
        chargeTimer -= Time.deltaTime;
        return (chargeTimer <= 0);
    }

    public override bool TakeDamage(float damage)
    {

        //RemoveDeck();

        return base.TakeDamage(damage);
    }

    public override void Die()
    {
        deckPool.DestroyDeckPool();
        base.Die();
    }

}
