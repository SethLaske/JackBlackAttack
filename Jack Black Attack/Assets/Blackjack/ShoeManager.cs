using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoeManager : MonoBehaviour
{
    //Number of decks in play
    public int ShoeSize = 1;

    //Holds a reference to all cards
    private ScriptableCard[] cards;

    //Cards that are currently in the shoe
    private List<ScriptableCard> shoe;
    private int numberCardsInShoe;

    private void Start()
    {
        InititalizeCards();
        ShuffleShoe();
    }

    //Gets all cards from resources folder
    //Clears card count
    //Sets number of cards in current shoe to 0
    private void InititalizeCards()
    {
        numberCardsInShoe = 0;
        cards = Resources.LoadAll<ScriptableCard>("ScriptableCards");
        ClearCardCount();
    }

    //Sets the numInDeck of each card to 0
    private void ClearCardCount()
    {
        foreach (var card in cards)
        {
            card.NumInDeck = 0;
        }
    }

    // Draws a random card from the shoe and then removes it from the shoe
    public bool DrawCard(out ScriptableCard _cardDrawn)
    {
        _cardDrawn = null;

        if(numberCardsInShoe <= 0)
        {
            return false;
        }


        int _index = Random.Range(0, shoe.Count);
        _cardDrawn = shoe[_index];
        shoe.RemoveAt(_index);
        _cardDrawn.NumInDeck--;
        numberCardsInShoe--;

        Debug.Log(_cardDrawn);
        Debug.Log(_cardDrawn.NumInDeck);
        Debug.Log(numberCardsInShoe);
        return true;
    }

    public void DrawCard()
    {
        if (numberCardsInShoe <= 0)
        {
            return;
        }
        int _index = Random.Range(0, shoe.Count);
        ScriptableCard _cardDrawn = shoe[_index];
        shoe.RemoveAt(_index);
        _cardDrawn.NumInDeck--;
        numberCardsInShoe--;
        Debug.Log("Card Drawn: " + _cardDrawn);
        Debug.Log("Number of this card left: " + _cardDrawn.NumInDeck);
        Debug.Log("Number of cards left in the shoe: " + numberCardsInShoe);
    }

    // Add a numberOfDecks to the shoe (Shoe needs to be reshuffled for cards to be in play)
    private void AddDecks(int numberOfDecks)
    {
        ShoeSize += numberOfDecks;
    }

    // Reshuffles all cards into a new shoe
    public void ShuffleShoe()
    {
        shoe = new List<ScriptableCard>();
        numberCardsInShoe = 0;
        foreach (var card in cards)
        {
            card.NumInDeck = ShoeSize;
            for (int i = 0; i < card.NumInDeck; i++)
            {
                shoe.Add(card);
                numberCardsInShoe++;
            }
        }
    }
}
