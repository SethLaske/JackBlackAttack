using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class bankInteractionScript : MonoBehaviour
{
    // Banking stuff
    private int goldValue;
    private float balance;

    // Bank input
    public TMP_InputField withdrawField;
    public TMP_Text withdrawFeedback;

    public TMP_InputField depositField;
    public TMP_Text depositFeedback;

    public Canvas myCanvas;

    public void exit()
    {
        myCanvas.gameObject.SetActive(false);
    }

    public void Start()
    {
        goldValue = PlayerPrefs.GetInt("Player Gold"); 
        balance = PlayerPrefs.GetFloat("stored gold"); 
        Debug.Log(goldValue);
    }

    public void Deposit()
    {
        goldValue = PlayerPrefs.GetInt("Player Gold"); 
        balance = PlayerPrefs.GetFloat("stored gold"); 

        Debug.Log("Deposit");
        
        string depositAmountText = depositField.text;

        if (string.IsNullOrEmpty(depositAmountText))
        {
            depositFeedback.text = "Please enter a number.";
        }
        else if (!float.TryParse(depositAmountText, out float amount))
        {
            depositFeedback.text = "Invalid input. Please enter a valid number.";
        }
        else
        {
            depositFeedback.text = "Amount entered: " + amount;
            balance += amount;
            PlayerPrefs.SetFloat("stored gold", balance);
        }
        depositField.text = depositFeedback.text;
        Debug.Log(balance);
    }

    public void Withdraw()
    {
        goldValue = PlayerPrefs.GetInt("Player Gold"); 
        balance = PlayerPrefs.GetFloat("stored gold"); 

        Debug.Log("Withdraw");

        string withdrawAmountText = withdrawField.text;
        Debug.Log(withdrawAmountText);
        if (string.IsNullOrEmpty(withdrawAmountText))
        {
            withdrawFeedback.text = "Please enter a number.";
        }
        else if (!float.TryParse(withdrawAmountText, out float amount))
        {
            withdrawFeedback.text = "Invalid input. Please enter a valid number.";
        }
        else
        {
            withdrawFeedback.text = "Amount entered: " + amount;
            if (amount <= balance)
            {
                balance -= amount;
                PlayerPrefs.SetFloat("stored gold", balance);
            }
            else
            {
                withdrawFeedback.text = "Insufficient balance.";
            }
        }
        withdrawField.text = withdrawFeedback.text;
    }
}