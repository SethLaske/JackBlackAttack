using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerCardPositionManager : MonoBehaviour
{
    public Transform FirstTrans;
    public float CardSpace = 50f;
    public List<GameObject> Cards;
    public GameObject blankCard;
    public TextMeshProUGUI handValueText;


    private void Start()
    {
        BlackjackManager.Instance.OnPlayerCardDraw += DisplayCard;
    }
    private void DisplayCard()
    {
        handValueText.text = BlackjackManager.Instance.playerHand.handValue.ToString();

        gameObject.transform.DestroyChildren();

        Cards.Clear();

        List<ScriptableCard> _tempCards = BlackjackManager.Instance.GetPlayerHand();

        foreach (ScriptableCard card in _tempCards)
        {
            GameObject _tempCard = Instantiate(blankCard, gameObject.transform);
            _tempCard.GetComponent<UnityEngine.UI.Image>().sprite = card.CardSprite;
            Cards.Add(_tempCard);
        }

        for(int i = 0; i < Cards.Count; i++) 
        {
            Cards[i].transform.position = new Vector3(FirstTrans.position.x, FirstTrans.position.y - CardSpace * i, 0);
        }
    }
}
