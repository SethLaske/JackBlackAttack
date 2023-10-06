using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDisplay : MonoBehaviour
{
    public GameObject CardSlot;
    private void Start()
    {
        ShoeManager.Instance.OnCardDraw += DisplayCard;
    }
    private void DisplayCard()
    {
        
    }
}
