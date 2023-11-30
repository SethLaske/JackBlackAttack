using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EndState { 
    Win,
    Lose,
    Push
}


public class LevelManager : MonoBehaviour
{
    [SerializeField] private RoomGenerator roomGenerator;
    private int enemyCount;
    public bool hasSpawned = false;
    public static bool enemyOnScreen;
   
    //public GameObject door;

    [SerializeField] private Gate victoryGate;
    [SerializeField] private Gate defeatGate;

    public static int cardNumber;
    public static string cardSuit;
    public event Action onWaveComplete;
    // Start is called before the first frame update
    [SerializeField] private GameObject dungeonExitTile;
    [SerializeField] private Gate enterHandGate;
    [SerializeField] private GameObject victoryPointer;
    [SerializeField] private GameObject defeatPointer;
    private bool activateExit;
    
 public static bool PointerOn = false;
    

    void Awake()
    {
        activateExit = false;
        dungeonExitTile.SetActive(false);
        victoryPointer.SetActive(false);
        defeatPointer.SetActive(false);
        enterHandGate.CloseGate();
        CheckPlayerGold();
        PointerOn = false;
    }

    public void StartLevel() {
        victoryGate.CloseGate();
        defeatGate.CloseGate();
    }


    public void SetEnemyCount(int count) {
        enemyCount = count;
    }

    public void EnemyDied() {
        enemyCount--;
        
        if (enemyCount == 0)
        {
            PointerOn = false;
            roomGenerator.ClearTiles();
            onWaveComplete();
            hasSpawned = true;

            if(!BlackjackManager.Instance.handIsActive)
            {
                //SpawnDoor();
            }
        }
        else if(enemyCount <= 3)
        {
            PointerOn = true;
        }
        else
        {
            PointerOn = false;
        }
    }

    public void HandFinished(EndState endState) {

        if (activateExit == true) {
            ResetGates();
            dungeonExitTile.SetActive(true);
            return;
        }

        switch (endState) {
            case EndState.Win:
                PlayerVictory();
                break;
            case EndState.Lose:
                PlayerDefeat();
                break;
            case EndState.Push:
                PlayerPush();
                break;
            default:
                PlayerPush();
                break;

        }

        CheckPlayerGold();

    }

    private void CheckPlayerGold() {
        if (PlayerPrefs.GetInt("Player Gold") < BlackjackManager.Instance.betAmount)
        {
            //Debug.Log("Not enough gold to continue");
            enterHandGate.CloseGate();
        }
        else {
            enterHandGate.OpenGate();
        }
    }

    private void PlayerVictory() {
        victoryGate.OpenGate();
        victoryPointer.SetActive(true);
    }

    private void PlayerPush() {
        defeatGate.OpenGate();
        defeatPointer.SetActive(true);
    }

    private void PlayerDefeat() {
        defeatGate.OpenGate();
        defeatPointer.SetActive(true);
    }

    public void ResetGates()
    {
        defeatGate.CloseGate();
        victoryGate.CloseGate();
        victoryPointer.SetActive(false);
        defeatPointer.SetActive(false);
    }

    public void FinalHand() {
        activateExit = true;
    }

}
