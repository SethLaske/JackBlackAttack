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
        PlayerPrefs.SetInt("stored gold", 50); // TEMPORARY FOR BANK STUFF, DELETE LATER
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
            StartCoroutine(RevertTextAfterDelay(depositField, depositFeedback));
        }
        else if (!int.TryParse(depositAmountText, out int amount))
        {
            depositField.text = ("");
            depositFeedback.SetText("Not a number...");
            StartCoroutine(RevertTextAfterDelay(depositField, depositFeedback));
        }
        else if (amount <= goldValue)
        {
            depositField.text = ("");
            depositFeedback.SetText("Deposited: " + amount);
            StartCoroutine(RevertTextAfterDelay(depositField, depositFeedback));
            balance += amount;
            goldValue -= amount;
            PlayerPrefs.SetInt("Player Gold", goldValue);
            PlayerPrefs.SetInt("stored gold", balance);
        }
        else
        {
            depositField.text = ("");
            depositFeedback.SetText("Failed to deposit...");
            StartCoroutine(RevertTextAfterDelay(depositField, depositFeedback));
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
            withdrawFeedback.SetText("Not a number...");
            StartCoroutine(RevertTextAfterDelay(withdrawField, withdrawFeedback));
        }
        else if (amount <= balance)
        {
            withdrawField.text = ("");
            withdrawFeedback.SetText("Withdrew: " + amount);
            StartCoroutine(RevertTextAfterDelay(withdrawField, withdrawFeedback));
            balance -= amount;
            goldValue += amount;
            PlayerPrefs.SetInt("Player Gold", goldValue);
            PlayerPrefs.SetInt("stored gold", balance);
        }
        else
        {
            withdrawField.text = ("");
            withdrawFeedback.SetText("Failed to withdraw...");
            StartCoroutine(RevertTextAfterDelay(withdrawField, withdrawFeedback));
        }
        balanceOutput.SetText("Balance: " + balance);
        playerGold.SetText("Player Gold: " + goldValue);
    }

    IEnumerator RevertTextAfterDelay(TMP_InputField clear, TextMeshProUGUI buttonText)
    {
        yield return new WaitForSeconds(1.5f);
        clear.text = ("");
        buttonText.SetText("Enter value...");
    }
}