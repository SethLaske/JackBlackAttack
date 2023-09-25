using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScriptableCard : ScriptableObject
{
    public Suit CardSuit;

    // cardRank gives us the number or face card that a card is (NOT THE VALUE OF THE CARD WITHIN BLACKJACK)
    // cardRank key: 
    // Ace = 1
    // Numbers 2-10 = 2-10
    // Jack = 11
    // Queen = 12
    // King = 13
    public int CardRank;

    // This is the actual value of the card within blackjack
    // ex: King = 10 
    public int CardValue;

    public int NumInDeck;

    public enum Suit
    {
        Club, Spade, Heart, Diamond
    }
}
