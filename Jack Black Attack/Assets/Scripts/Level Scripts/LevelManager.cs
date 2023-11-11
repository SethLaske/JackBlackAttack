using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private RoomGenerator roomGenerator;
    private int enemyCount;
    public bool hasSpawned = false;

   
    //public GameObject door;

    [SerializeField] private GameObject victoryGate;
    [SerializeField] private GameObject defeatGate;

    public static int cardNumber;
    public static string cardSuit;
    public event Action onWaveComplete;
    // Start is called before the first frame update
    void Start()
    {
        //door.SetActive(false);
        //pass in card suit and number from given card, example rn is 4 of hearts
        //cardSuit = "Hearts";
        //cardNumber = 4; 
        //roomGenerator.StartRoomGeneration(Random.Range(1, 14));
    }

    public void StartLevel() {
        victoryGate.SetActive(true);
        defeatGate.SetActive(true);
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
                //SpawnDoor();
            }
        }
    }

    public void PlayerVictory() {
        victoryGate.SetActive(false);
    }

    public void PlayerPush() {
        defeatGate.SetActive(false);
    }

    public void PlayerDefeat() {
        defeatGate.SetActive(false);
    }

    public void ResetGates()
    {
        defeatGate.SetActive(true);
        victoryGate.SetActive(true);
    }


    /*public void SpawnDoor()
    {
        door.SetActive(true);
    }*/
}
