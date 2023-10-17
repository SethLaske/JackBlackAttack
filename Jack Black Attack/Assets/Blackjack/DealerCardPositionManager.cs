using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DealerCardPositionManager : MonoBehaviour
{
    public Transform FirstTrans;
    public float CardSpace = 50f;
    public List<GameObject> Cards;
    public GameObject blankCard;
    public Sprite backOfCard;
    public TextMeshProUGUI handValueText;


    private void Start()
    {
        BlackjackManager.Instance.OnDealerCardDraw += DisplayCard;
        BlackjackManager.Instance.OnDealerInitialDraw += DisplayFirstCards;
    }
    private void DisplayCard()
    {
        handValueText.text = BlackjackManager.Instance.dealerHand.handValue.ToString();

        gameObject.transform.DestroyChildren();

        Cards.Clear();

        List<ScriptableCard> _tempCards = BlackjackManager.Instance.GetDealerHand();

        foreach (ScriptableCard card in _tempCards)
        {
            GameObject _tempCard = Instantiate(blankCard, gameObject.transform);
            _tempCard.GetComponent<UnityEngine.UI.Image>().sprite = card.CardSprite;
            Cards.Add(_tempCard);
        }

        for (int i = 0; i < Cards.Count; i++)
        {
            Cards[i].transform.position = new Vector3(FirstTrans.position.x, FirstTrans.position.y - CardSpace * i, 0);
        }
    }

    private void DisplayFirstCards()
    {
        handValueText.text = "?";

        gameObject.transform.DestroyChildren();

        Cards.Clear();

        List<ScriptableCard> _tempCards = BlackjackManager.Instance.GetDealerHand();

        int _index = 0;
        foreach (ScriptableCard card in _tempCards)
        {

            GameObject _tempCard = Instantiate(blankCard, gameObject.transform);
            if(_index != 1)
            {
                _tempCard.GetComponent<UnityEngine.UI.Image>().sprite = card.CardSprite;
            }
            else
            {
                _tempCard.GetComponent<UnityEngine.UI.Image>().sprite = backOfCard;
            }
            _index++;

            Cards.Add(_tempCard);
        }
        

        for (int i = 0; i < Cards.Count; i++)
        {
            Cards[i].transform.position = new Vector3(FirstTrans.position.x, FirstTrans.position.y - CardSpace * i, 0);
        }
    }
}
