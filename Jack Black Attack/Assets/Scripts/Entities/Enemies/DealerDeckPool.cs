using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealerDeckPool : MonoBehaviour
{

    [SerializeField] private int waveSize;
    [SerializeField] private GameObject deck;
    [SerializeField] private Queue<GameObject> deckPool;
    private GameObject deckParent;

    private void Awake()
    {
        deckPool = new Queue<GameObject>();
        deckParent = new GameObject("Deck Pool");

        SpawnMoreDecks();
    }

    public GameObject GetDeckFromPool() {
        if (deckPool.Count <= 0) {
            //Spawn more 
            SpawnMoreDecks();
        }

        GameObject topDeck = deckPool.Dequeue();
        topDeck.SetActive(true);
        return topDeck;
    }

    public void AddDeckToPool(GameObject deck) {
        deck.SetActive(false);
        deck.transform.SetParent(deckParent.transform);
        deckPool.Enqueue(deck);
    }

    private void SpawnMoreDecks() {
        Debug.Log("Spawning a new wave of decks");
        for (int i = 0; i < waveSize; i++) {
            GameObject newDeck = Instantiate(deck, deckParent.transform);
            newDeck.SetActive(false);
            deckPool.Enqueue(newDeck);
        }
    }

    public void DestroyDeckPool() {
        Destroy(deckParent);
        
    }


}
