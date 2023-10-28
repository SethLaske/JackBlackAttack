using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider playerHealthBar;
    [SerializeField] private TextMeshProUGUI goldText;
    public GameObject HealthBar;
    //private PlayerDungeonController player;
    private void Start()
    {
         PlayerPrefs.SetInt("Player Gold", 0);
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
    private void DisableHealthBar()
    {
        HealthBar.SetActive(false);
    }
     private void EnableHealthBar()
    {
        HealthBar.SetActive(true);
    }
    private void OnEnable()
    {
        PlayerDungeonController.onPlayerDeath += DisableHealthBar;
    }
    private void OnDisable()
    {
        PlayerDungeonController.onPlayerDeath -= DisableHealthBar;
    }
  
}
