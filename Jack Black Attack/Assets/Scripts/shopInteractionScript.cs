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
        else if (PlayerPrefs.GetInt("boomerang") == 1 && PlayerPrefs.GetString("ChosenWeapon").Equals("Boomerang"))
        {
            displayBoomerangPrice.SetText("Boomerang - " + " sold out");
            boomerang.interactable = false;
            ownBoomerang.SetText("EQUIPPED");
        }
        else
        {
            displayBoomerangPrice.SetText("Boomerang - " + " sold out");
            boomerang.interactable = true;
            ownBoomerang.SetText("SWAP");
        }

        if (PlayerPrefs.GetInt("pool stick") == 1 && PlayerPrefs.GetString("ChosenWeapon").Equals("PoolStick"))
        {
            displayPoolStickPrice.SetText("Pool Stick - " + " sold out");
            ownPoolStick.SetText("EQUIPPED");
            poolStick.interactable = false;
        }
        else
        {
            displayPoolStickPrice.SetText("Pool Stick - " + " sold out");
            poolStick.interactable = true;
            ownPoolStick.SetText("SWAP");
        }

        if (PlayerPrefs.GetInt("trident") == 0)
        {
            displayTridentPrice.SetText("Trident - " + tridentPrice + " gold");
        }
        else if (PlayerPrefs.GetInt("trident") == 1 && PlayerPrefs.GetString("ChosenWeapon").Equals("Trident"))
        {
            displayTridentPrice.SetText("Trident - " + " sold out");
            trident.interactable = false;
            ownTrident.SetText("EQUIPPED");
        }
        else
        {
            displayTridentPrice.SetText("Trident - " + " sold out");
            trident.interactable = true;
            ownTrident.SetText("SWAP");
        }
    }
    public void exit()
    {
        myCanvas.gameObject.SetActive(false);
    }

    public void purchaseBoomerang()
    {
        int price = boomerangPrice;
        if (PlayerPrefs.GetInt("boomerang") == 1 && !(PlayerPrefs.GetString("ChosenWeapon").Equals("Boomerang")))
        {
            PlayerPrefs.SetString("ChosenWeapon", "Boomerang");
            ownBoomerang.SetText("EQUIPPED");
        }
        else if (goldValue >= boomerangPrice)
        {
            PlayerPrefs.SetString("ChosenWeapon", "Boomerang");
            goldValue -= boomerangPrice;
            PlayerPrefs.SetInt("boomerang", 1);
            boomerang.interactable = false;
            ownBoomerang.SetText("OWNED");
        }
        else if (goldValue + bankedGold >= boomerangPrice)
        {
            PlayerPrefs.SetString("ChosenWeapon", "Boomerang");
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
        if (PlayerPrefs.GetInt("pool stick") == 1)
        {
            PlayerPrefs.SetString("ChosenWeapon", "PoolStick");
            ownPoolStick.SetText("EQUIPPED");
            poolStick.interactable = false;
        }
        updateDisplay();
    }

    public void purchaseTrident()
    {
        int price = tridentPrice;
        if (PlayerPrefs.GetInt("trident") == 1 && !(PlayerPrefs.GetString("ChosenWeapon").Equals("Trident")))
        {
            PlayerPrefs.SetString("ChosenWeapon", "Trident");
            ownTrident.SetText("EQUIPPED");
        }
        else if (goldValue >= tridentPrice)
        {
            PlayerPrefs.SetString("ChosenWeapon", "Trident");
            goldValue -= tridentPrice;
            PlayerPrefs.SetInt("trident", 1);
            trident.interactable = false;
            ownTrident.SetText("OWNED");
        }
        else if (goldValue + bankedGold >= tridentPrice)
        {
            PlayerPrefs.SetString("ChosenWeapon", "Trident");
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
