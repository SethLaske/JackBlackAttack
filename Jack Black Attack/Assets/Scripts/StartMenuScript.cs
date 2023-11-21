using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuScript : MonoBehaviour
{
    public GameObject Controls;
    public GameObject MainMenu;
    public GameObject player;
    //public Scene Hub;

    private void Awake()
    {
        backToMenu();
        ResetPrefabs();

    }

    public void openHub()
    {
        SceneManager.LoadScene("Hub");
    }

    private void ResetPrefabs() {
        PlayerPrefs.SetInt("Player Gold", 50);
        PlayerPrefs.SetString("ChosenWeapon", "PoolStick");
        PlayerPrefs.SetInt("stored gold", 50);

        PlayerPrefs.SetInt("boomerang", 0);
        PlayerPrefs.SetInt("pool stick", 1);
        PlayerPrefs.SetInt("trident", 0);
    }

    public void displayControls()
    {
        MainMenu.SetActive(false);
        Controls.SetActive(true);
        player.SetActive(true);
    }
    public void backToMenu()
    {
        MainMenu.SetActive(true);
        Controls.SetActive(false);
        player.SetActive(false);
    }
}
