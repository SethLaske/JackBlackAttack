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
