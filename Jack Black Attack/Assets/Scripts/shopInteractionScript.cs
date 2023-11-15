using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class shopInteractionScript : MonoBehaviour
{
    public Canvas myCanvas;

    //Buttons
    public Button boomerang;
    public Button poolStick;
    public Button trident;

    //Prices

    public int boomerangPrice;
    public int poolStickPrice;
    public int tridentPrice;

    //Price display

    public TextMeshProUGUI displayBoomerangPrice;
    public TextMeshProUGUI displayPoolStickPrice;
    public TextMeshProUGUI displaytridentPrice;

    //Current player gold 
    private int goldValue;
    public TextMeshProUGUI playerGold;

    private int bankedGold;
    public TextMeshProUGUI bankValue;

    //Display owned weapons

    public TextMeshProUGUI ownBoomerang;
    public TextMeshProUGUI ownPoolStick;
    public TextMeshProUGUI ownTrident;
    void Start()
    {
        goldValue = PlayerPrefs.GetInt("Player Gold");
        playerGold.SetText("Player Gold: " + goldValue);
        bankedGold = PlayerPrefs.GetInt("stored gold");
        bankValue.SetText("Banked Gold: " + bankedGold);
    }


    void Update()
    {

    }

    public void exit()
    {
        myCanvas.gameObject.SetActive(false);
    }

    public void purchaseBoomerang()
    {

    }

    public void purchasePoolStick()
    {

    }

    public void purchaseTrident()
    {

    }
}
