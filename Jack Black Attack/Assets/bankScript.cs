using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bankScript : MonoBehaviour
{
    private int goldValue; 
    private int storedGold;

    public void Start(){
        int goldValue = PlayerPrefs.GetInt("Player Gold");
        int storedGold = PlayerPrefs.GetInt("stored gold");
        Debug.Log(goldValue);
    }

    


}
