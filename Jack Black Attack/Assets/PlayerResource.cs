using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResource : MonoBehaviour
{
    public goldScript gold;

    private void Start()
    {
        LoadGold();
    }

    public void SaveGold()
    {
        PlayerPrefs.SetInt("Player Gold", gold.goldValue);
    }

    public void LoadGold()
    {
        gold.goldValue = PlayerPrefs.GetInt("Player Gold");
    }

}
