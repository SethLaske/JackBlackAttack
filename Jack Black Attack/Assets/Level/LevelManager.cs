using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private RoomGenerator roomGenerator;
    private int enemyCount;
    public bool hasSpawned = false;

    public GameObject door;
    public Transform doorLocation;

    public static int cardNumber;
    public static string cardSuit;
    public event Action onWaveComplete;
    // Start is called before the first frame update
    void Start()
    {
        door.SetActive(false);
        //pass in card suit and number from given card, example rn is 4 of hearts
        //cardSuit = "Hearts";
        //cardNumber = 4; 
        //roomGenerator.StartRoomGeneration(Random.Range(1, 14));
    }

    // Update is called once per frame
    /* void Update()
     {
         //constantly updates the number of enemies in the room
         enemyCount = GameObject.FindGameObjectsWithTag("enemy").Length;
         if (enemyCount == 0 && !hasSpawned)
         {
             SpawnDoor();
             hasSpawned = true;
         }
     }*/

    public void SetEnemyCount(int count) {
        enemyCount = count;
    }

    public void EnemyDied() {
        enemyCount--;
        if (enemyCount == 0)
        {
            roomGenerator.ClearTiles();
            onWaveComplete();
            hasSpawned = true;

            if(!BlackjackManager.Instance.handIsActive)
            {
                SpawnDoor();
            }
        }
    }


    public void SpawnDoor()
    {
        door.SetActive(true);
    }
}
