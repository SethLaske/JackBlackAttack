using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuScript : MonoBehaviour
{
    public GameObject Controls;
    public GameObject MainMenu;
    //public Scene Hub;
    public void openHub()
    {
        PlayerPrefs.SetInt("Player Gold", 50);
        PlayerPrefs.SetString("ChosenWeapon", "PoolStick");
        PlayerPrefs.SetInt("stored gold", 50);

        //Reset all other player prefs here

        SceneManager.LoadScene("Hub");
    }
    public void displayControls()
    {
        MainMenu.SetActive(false);
        Controls.SetActive(true);
    }
    public void backToMenu()
    {
        MainMenu.SetActive(true);
        Controls.SetActive(false);
    }
}
