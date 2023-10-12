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
    public int coinCount;
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
        Debug.Log("RESTART");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
   }
   IEnumerator waitDeathUi()
    {   
        yield return new WaitForSeconds(deathMenuDelay);
        Setup(coinCount);
        GameOverMenu.SetActive(true); //game over menu pops up after 3 seconds but player is frozen immediately
    }
  
}
