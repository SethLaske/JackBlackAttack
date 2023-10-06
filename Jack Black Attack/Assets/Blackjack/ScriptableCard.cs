using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScriptableCard : ScriptableObject
{
    public Suit CardSuit;

    // cardFace gives us the number or face card that a card is (NOT THE VALUE OF THE CARD WITHIN BLACKJACK)
    // cardFace key: 
    // Ace = 1
    // Numbers 2-10 = 2-10
    // Jack = 11
    // Queen = 12
    // King = 13
    public Face CardFace;

    // This is the actual value of the card within blackjack
    // ex: King = 10 
    public int CardValue;

    public int NumInDeck;

    public Sprite CardSprite;

    public enum Suit
    {
        Club,
        Spade, 
        Heart, 
        Diamond
    }
    public enum Face
    {
        Ace,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King
    }
}
