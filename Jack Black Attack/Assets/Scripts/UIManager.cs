using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider playerHealthBar;
    [SerializeField] private TextMeshProUGUI goldText;
    //private PlayerDungeonController player;
    private void Start()
    {
        PlayerDungeonController.onTakeDamage += SetPlayerHealthBar;
        InitializePlayerHealthBar(FindObjectOfType<PlayerDungeonController>().HP);

        goldScript.onGoldPickup += UpdateGoldText;
        UpdateGoldText();
    }

    public void InitializePlayerHealthBar(float maxHealth) {
        playerHealthBar.maxValue = maxHealth;
        playerHealthBar.value = maxHealth;
    }

    public void SetPlayerHealthBar(float currentHealth) {
        playerHealthBar.value = currentHealth;
    }

    private void UpdateGoldText() {
        goldText.text = "" + PlayerPrefs.GetInt("Player Gold");
    }
  
}
