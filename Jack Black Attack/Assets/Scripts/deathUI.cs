using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class deathUI : MonoBehaviour
{
    public GameObject GameOverMenu;
    public TMP_Text coinsText;
    public float deathMenuDelay;

    public void Setup(int coins) //call setup with coin input later... unfinished
    {
        coinsText.text = "Coins: " + coins.ToString();
    }
    private void OnEnable()
    {
        PlayerDungeonController.onPlayerDeath += EnableGameOverMenu;
    }
    private void OnDisable()
    {
        PlayerDungeonController.onPlayerDeath -= EnableGameOverMenu;
    }
   public void EnableGameOverMenu()
   {
        StartCoroutine(waitDeathUi());
        //GameOverMenu.SetActive(true);
        //load seperate scene here if wanted
   }

   public void restartLevel()
   {
        Time.timeScale = 1;
        SceneManager.LoadScene("Start"); //Scene may need to be changed
   }
   IEnumerator waitDeathUi()
    {   
        yield return new WaitForSeconds(deathMenuDelay);
        Time.timeScale = 0;
        Setup(PlayerPrefs.GetInt("Player Gold"));
        GameOverMenu.SetActive(true); //game over menu pops up after delay seconds but player is frozen immediately
    }
  
}
