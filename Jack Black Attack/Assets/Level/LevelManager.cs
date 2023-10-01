using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int enemyCount;
    public bool hasSpawned = false;

    public GameObject DoorPrefab;
    public Transform doorLocation;

    public static int cardNumber;
    public static string cardSuit;
    // Start is called before the first frame update
    void Start()
    {
        //pass in card suit and number from given card, example rn is 4 of hearts
        cardSuit = "Hearts";
        cardNumber = 4; 
    }

    // Update is called once per frame
    void Update()
    {
        //constantly updates the number of enemies in the room
        enemyCount = GameObject.FindGameObjectsWithTag("enemy").Length;
        if (enemyCount == 0 && !hasSpawned)
        {
            SpawnDoor();
            hasSpawned = true;
        }
    }

    public void SpawnDoor()
    {
        GameObject door = Instantiate(DoorPrefab, doorLocation.position, Quaternion.identity);
    }
}
