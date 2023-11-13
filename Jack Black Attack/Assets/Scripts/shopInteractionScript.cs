using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class shopInteractionScript : MonoBehaviour
{
    public Canvas myCanvas;
    void Start()
    {

    }


    void Update()
    {

    }

    public void exit()
    {
        myCanvas.gameObject.SetActive(false);
    }
}
