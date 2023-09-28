using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class BlackjackManager : MonoBehaviour
{
    [SerializeField]
    private Hand playerHand;
    [SerializeField]
    private Hand dealerHand;

    private const int BUST_LIMIT = 21; // Maximum value that you can have before busting
    private const int DEALER_HIT_LIMIT = 16; // Maximum value that dealer can hit on

    private void Start()
    {
        InitializeHands();
    }

    //Deals a new hand to the player


    public void NewHand()
    {
        DealDealerHand();
        DealPlayerHand();

        Debug.Log(dealerHand.cards[0]);
    }

    public void DealPlayerHand()
    {
        ClearHand(playerHand);
        GetNewCard(playerHand);
        GetNewCard(playerHand);
    }

    //Deals a new hand to the dealer
    public void DealDealerHand()
    {
        ClearHand(dealerHand);
        GetNewCard(dealerHand);
        GetNewCard(dealerHand);
    }
    //Creates player and dealer hand instances
    private void InitializeHands()
    {
        playerHand = new Hand();

        dealerHand = new Hand();
    }

    //Gives hand a new card
    private void GetNewCard(Hand _hand)
    {
        ScriptableCard _newCard = ShoeManager.Instance.DrawCard();
        _hand.cards.Add(_newCard);
        _hand.handValue += _newCard.CardValue;

        // Keeps track of soft aces to (because aces can be 11 or 1)
        if(_newCard.CardFace == ScriptableCard.Face.Ace)
        {
            _hand.numSoftAces++;
        }
    }


    //Hit this hand
    private void Hit(Hand _hand)
    {
        GetNewCard(_hand);
        if(_hand.handValue <= BUST_LIMIT) { return; }

        // Player busts with a soft ace
        if(_hand.handValue > BUST_LIMIT && _hand.numSoftAces > 0)
        {
            _hand.numSoftAces--;
            _hand.handValue -= 10;
            return;
        }

        Debug.Log("Player Busted");
        return;
    }

    public void PlayerStand()
    {
        DealerTurn();
    }
    public void PlayerHit()
    {
        Hit(playerHand);
    }

    private void DealerTurn()
    {

        // Dealer will hit on 16 and stand on 17
        while (dealerHand.handValue <= DEALER_HIT_LIMIT) 
        {
            Hit(dealerHand);
        }
    }

    private void ClearHand(Hand _hand)
    {
        _hand.cards.Clear();
        _hand.handValue = 0;
        _hand.numSoftAces = 0;
    }
}


[Serializable]
public class Hand
{
    public int numSoftAces;             // A soft ace is an ace that is worth 11
    public int handValue;
    public List<ScriptableCard> cards;
    

    public Hand()
    {
        cards = new List<ScriptableCard>();
        numSoftAces = 0;
        handValue = 0;
    }
}
