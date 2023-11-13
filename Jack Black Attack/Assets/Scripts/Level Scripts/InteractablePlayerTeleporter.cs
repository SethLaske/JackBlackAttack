using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePlayerTeleporter : MonoBehaviour
{
    [SerializeField] private Transform newLocation;
    private GameObject player;

    private void Start()
    {
        player = null;
    }
    /*private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {

            collision.gameObject.transform.position = newLocation.position;
            BlackjackManager.Instance.NewHand();
        }
    }*/

    private void Update()
    {
        if (player != null && Input.GetKeyDown(KeyCode.E))
        {

            player.transform.position = newLocation.position;
            BlackjackManager.Instance.NewHand();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            player = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            player = null;
        }
    }
}
