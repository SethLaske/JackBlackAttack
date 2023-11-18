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
    public TextMeshProUGUI displayTridentPrice;

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
        updateDisplay();
    }


    void Update()
    {

    }


    public void updateDisplay()
    {
        goldValue = PlayerPrefs.GetInt("Player Gold");
        playerGold.SetText("Player Gold: " + goldValue);
        bankedGold = PlayerPrefs.GetInt("stored gold");
        bankValue.SetText("Banked Gold: " + bankedGold);

        if (PlayerPrefs.GetInt("boomerang") == 0)
        {
            displayBoomerangPrice.SetText("Boomerang - " + boomerangPrice + " gold");
        }
        else
        {
            displayBoomerangPrice.SetText("Boomerang - " + " sold out");
            boomerang.interactable = false;
            ownBoomerang.SetText("OWNED");
        }
        if (PlayerPrefs.GetInt("pool stick") == 0)
        {
            displayPoolStickPrice.SetText("Pool Stick - " + poolStickPrice + " gold");
        }
        else
        {
            displayPoolStickPrice.SetText("Pool Stick - " + " sold out");
            poolStick.interactable = false;
            ownPoolStick.SetText("OWNED");
        }
        if (PlayerPrefs.GetInt("trident") == 0)
        {
            displayTridentPrice.SetText("Trident - " + tridentPrice + " gold");
        }
        else
        {
            displayTridentPrice.SetText("Trident - " + " sold out");
            trident.interactable = false;
            ownTrident.SetText("OWNED");
        }
    }
    public void exit()
    {
        myCanvas.gameObject.SetActive(false);
    }

    public void purchaseBoomerang()
    {
        int price = boomerangPrice;
        if (goldValue >= boomerangPrice)
        {
            PlayerPrefs.SetString("ChosenWeapon", "BoomerangWeapon");
            goldValue -= boomerangPrice;
            PlayerPrefs.SetInt("boomerang", 1);
            boomerang.interactable = false;
            ownBoomerang.SetText("OWNED");
        }
        else if (goldValue + bankedGold >= boomerangPrice)
        {
            PlayerPrefs.SetString("ChosenWeapon", "BoomerangWeapon");
            price -= goldValue;
            PlayerPrefs.SetInt("Player Gold", 0);
            PlayerPrefs.SetInt("stored gold", bankedGold - price);
            PlayerPrefs.SetInt("boomerang", 1);
            boomerang.interactable = false;
            ownBoomerang.SetText("OWNED");
        }


        updateDisplay();
    }

    public void purchasePoolStick()
    {
        int price = poolStickPrice;
        if (goldValue >= poolStickPrice)
        {
            PlayerPrefs.SetString("ChosenWeapon", "RicWeapon");
            goldValue -= poolStickPrice;
            PlayerPrefs.SetInt("pool stick", 1);
            poolStick.interactable = false;
            ownPoolStick.SetText("OWNED");
        }
        else if (goldValue + bankedGold >= poolStickPrice)
        {
            PlayerPrefs.SetString("ChosenWeapon", "RicWeapon");
            price -= goldValue;
            PlayerPrefs.SetInt("Player Gold", 0);
            PlayerPrefs.SetInt("stored gold", bankedGold - price);
            PlayerPrefs.SetInt("pool stick", 1);
            poolStick.interactable = false;
            ownPoolStick.SetText("OWNED");
        }

        updateDisplay();
    }

    public void purchaseTrident()
    {
        int price = tridentPrice;
        if (goldValue >= tridentPrice)
        {
            PlayerPrefs.SetString("ChosenWeapon", "TridentWeapon");
            goldValue -= tridentPrice;
            PlayerPrefs.SetInt("trident", 1);
            trident.interactable = false;
            ownTrident.SetText("OWNED");
        }
        else if (goldValue + bankedGold >= tridentPrice)
        {
            PlayerPrefs.SetString("ChosenWeapon", "TridentWeapon");
            price -= goldValue;
            PlayerPrefs.SetInt("Player Gold", 0);
            PlayerPrefs.SetInt("stored gold", bankedGold - price);
            PlayerPrefs.SetInt("trident", 1);
            trident.interactable = false;
            ownTrident.SetText("OWNED");
        }

        updateDisplay();
    }
}
