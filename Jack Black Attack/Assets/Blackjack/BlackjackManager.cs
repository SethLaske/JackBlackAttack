using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class BlackjackManager : MonoBehaviour
{
    public static BlackjackManager Instance;

    [SerializeField] public Hand playerHand { get; private set; }
    [SerializeField] public Hand dealerHand { get; private set; }

    [SerializeField] private TextMeshProUGUI endText;
    [SerializeField] private RoomGenerator roomGenerator;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameObject hitButton;
    [SerializeField] private GameObject standButton;
    [SerializeField] private GameObject newHandButton;



    private const int BUST_LIMIT = 21; // Maximum value that you can have before busting
    private const int DEALER_HIT_LIMIT = 16; // Maximum value that dealer can hit on

    public bool handIsActive = false;

    public event Action OnPlayerCardDraw;

    public event Action OnDealerInitialDraw;
    public event Action OnDealerCardDraw;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        InitializeHands();
        levelManager.onWaveComplete += OnWaveComplete;
    }

    //Deals a new hand to the player


    public void NewHand()
    {
        endText.text = "";

        if(ShoeManager.Instance.needShuffle)
        {
            ShoeManager.Instance.ShuffleShoe();
            Debug.Log("Shoe Shuffled");
        }

        DealDealerHand();

        DealPlayerHand();

        handIsActive = true;

        DisableButtons();
        
    }

    public void DealPlayerHand()
    {
        playerHand.busted = false;

        ClearHand(playerHand);
        GetNewCard(playerHand);
        roomGenerator.SpawnTiles(playerHand.cards[0].CardFormation);
        OnPlayerCardDraw(); //Displays cards

    }
    //Deals a new hand to the dealer
    public void DealDealerHand()
    {
        dealerHand.busted = false;

        ClearHand(dealerHand);
        GetNewCard(dealerHand);
        GetNewCard(dealerHand);
        OnDealerInitialDraw();

    }
    //Creates player and dealer hand instances
    private void InitializeHands()
    {
        playerHand = new Hand();

        dealerHand = new Hand();
    }

    //Gives hand a new card
    private ScriptableCard GetNewCard(Hand _hand)
    {
        ScriptableCard _newCard = ShoeManager.Instance.DrawCard();
        _hand.cards.Add(_newCard);
        _hand.handValue += _newCard.CardValue;

        // Keeps track of soft aces to (because aces can be 11 or 1)
        if(_newCard.CardFace == ScriptableCard.Face.Ace)
        {
            _hand.numSoftAces++;
        }

        return _newCard;
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

        _hand.busted = true;

        if(_hand == playerHand)
        {
            EndGame(EndStates.PlayerBust);
        }
        else
        {
            EndGame(EndStates.DealerBust);
        }

        return;
    }

    public void PlayerStand()
    {
        DealerTurn();
    }
    public void PlayerHit()
    {
        if(!handIsActive)
        {
            Debug.Log("Please Start a new Hand");
            return;
        }
        Hit(playerHand);
        roomGenerator.SpawnTiles(playerHand.cards[playerHand.cards.Count - 1].CardFormation); // Spawns the room of the last card drawn
        OnPlayerCardDraw();
        DisableButtons();
    }

    private void RevealDealerCard()
    {
        OnDealerCardDraw();    // TEMP SOLUTION FIX LATER
    }

    private void DealerTurn()
    {
        OnDealerCardDraw();

        // Dealer will hit on 16 and stand on 17
        while (dealerHand.handValue <= DEALER_HIT_LIMIT) 
        {
            Hit(dealerHand);
            OnDealerCardDraw();
        }

        if(playerHand.handValue > dealerHand.handValue && playerHand.busted == false)
        {
            EndGame(EndStates.PlayerWin);
        }
        if(dealerHand.handValue > playerHand.handValue && dealerHand.busted == false)
        {
            EndGame(EndStates.DealerWin);
        }
        if(dealerHand.handValue == playerHand.handValue)
        {
            EndGame(EndStates.Push);
        }
    }

    private void ClearHand(Hand _hand)
    {
        _hand.cards.Clear();
        _hand.handValue = 0;
        _hand.numSoftAces = 0;
    }

    private void EndGame(EndStates _endState)
    {
        handIsActive = false;

        

        switch(_endState)
        {
            default:
                Debug.Log("No End State Determined");
                endText.text = "No End State Determined";
                break;

            case EndStates.PlayerBlackjack:
                Debug.Log("PlayerBlackjack");
                endText.text = "Player Blackjack";
                levelManager.SpawnDoor();
                RevealDealerCard();
                break;
            case EndStates.PlayerWin:
                Debug.Log("PlayerWin");
                endText.text = "Player Win";
                levelManager.SpawnDoor();
                break;
            case EndStates.PlayerBust:
                Debug.Log("PlayerBust");
                endText.text = "Player Bust";
                RevealDealerCard();
                break;
            case EndStates.DealerWin:
                Debug.Log("DealerWin");
                endText.text = "Dealer Win";
                levelManager.SpawnDoor();
                break;
            case EndStates.DealerBust:
                Debug.Log("DealerBust");
                endText.text = "Dealer Bust";
                levelManager.SpawnDoor();
                break;
            case EndStates.Push:
                Debug.Log("Push");
                endText.text = "Push";
                levelManager.SpawnDoor();
                break;
        }
    }

    private void OnWaveComplete()
    {
        if(playerHand.cards.Count < 2)
        {
            PlayerHit();
            return;
        }

        hitButton.SetActive(true);
        standButton.SetActive(true);
    }

    private void DisableButtons()
    {
        //Disbale Buttons
        hitButton.SetActive(false);
        standButton.SetActive(false);
        newHandButton.SetActive(false);
    }


    public List<ScriptableCard> GetPlayerHand()
    {
        return playerHand.cards;
    }

    public List<ScriptableCard> GetDealerHand()
    {
        return dealerHand.cards;
    }
    private enum EndStates
    {
        PlayerBlackjack, PlayerWin, PlayerBust, DealerWin, DealerBust, Push
    }

}




[Serializable]
public class Hand
{
    public int numSoftAces;             // A soft ace is an ace that is worth 11
    public int handValue;
    public List<ScriptableCard> cards;
    public bool busted;
    

    public Hand()
    {
        cards = new List<ScriptableCard>();
        numSoftAces = 0;
        handValue = 0;
        busted = false;
    }
}
