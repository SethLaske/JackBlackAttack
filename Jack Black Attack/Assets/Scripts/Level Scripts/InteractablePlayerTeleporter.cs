using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePlayerTeleporter : MonoBehaviour
{
    [SerializeField] private Transform newLocation;
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {

            collision.gameObject.transform.position = newLocation.position;
            BlackjackManager.Instance.NewHand();
        }
    }
}
