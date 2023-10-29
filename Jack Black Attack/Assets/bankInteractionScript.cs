using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class bankInteractionScript : MonoBehaviour
{
    // Banking stuff
    private int goldValue;
    private int balance;

    // Bank input
    public TMP_InputField withdrawField;
    public TextMeshProUGUI withdrawFeedback;

    public TMP_InputField depositField;
    public TextMeshProUGUI depositFeedback;

    public TextMeshProUGUI balanceOutput;
    public TextMeshProUGUI playerGold;

    public Canvas myCanvas;

    public void exit()
    {
        myCanvas.gameObject.SetActive(false);
    }

    public void Start()
    {
        goldValue = PlayerPrefs.GetInt("Player Gold"); 
        balance = PlayerPrefs.GetInt("stored gold"); 
        balanceOutput.SetText("Balance: " + balance);
        playerGold.SetText("Player Gold: " + goldValue);
    }

    public void Deposit()
    {
        goldValue = PlayerPrefs.GetInt("Player Gold"); 
        balance = PlayerPrefs.GetInt("stored gold"); 

        string depositAmountText = depositField.text;

        if (string.IsNullOrEmpty(depositAmountText))
        {
            depositField.text = ("");
            depositFeedback.SetText("Enter value...");
        }
        else if (!int.TryParse(depositAmountText, out int amount))
        {
            depositField.text = ("");
            depositFeedback.SetText("Not a number - Enter value");
        }
        else if(amount <= goldValue)
        {
            depositField.text = ("");
            depositFeedback.SetText("Deposited: " + amount + " - Enter value");
            balance += amount;
            goldValue -= amount;
            PlayerPrefs.SetInt("Player Gold", goldValue);
            PlayerPrefs.SetInt("stored gold", balance);
        } else {
            depositField.text = ("");
            depositFeedback.SetText("Failed to deposit - Enter value");
        }
        balanceOutput.SetText("Balance: " + balance);
        playerGold.SetText("Player Gold: " + goldValue);
    }

    public void Withdraw()
    {
        goldValue = PlayerPrefs.GetInt("Player Gold"); 
        balance = PlayerPrefs.GetInt("stored gold"); 

        string withdrawAmountText = withdrawField.text;

        if (string.IsNullOrEmpty(withdrawAmountText))
        {
            withdrawField.text = ("");
            withdrawFeedback.SetText("Enter value...");      
        }
        else if (!int.TryParse(withdrawAmountText, out int amount))
        {
            withdrawField.text = ("");
            withdrawFeedback.SetText("Not a number - Enter value");
        }
        else if (amount <= balance)
        {
            withdrawField.text = ("");       
            withdrawFeedback.SetText("Withdrew: " + amount + " - Enter value");
            balance -= amount;
            goldValue += amount;         
            PlayerPrefs.SetInt("Player Gold", goldValue);
            PlayerPrefs.SetInt("stored gold", balance);
        }
        else
        { 
            withdrawField.text = ("");
            withdrawFeedback.SetText("Failed to withdraw - Enter value");
        }
        balanceOutput.SetText("Balance: " + balance);
        playerGold.SetText("Player Gold: " + goldValue);
    }
}