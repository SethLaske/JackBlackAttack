using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class InteractablePlayerTeleporter : MonoBehaviour
{
    [SerializeField] private Transform newLocation;
    [SerializeField] private CinemachineVirtualCamera arenaCamera;
    [SerializeField] private CinemachineVirtualCamera sideRoomCamera;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            collision.transform.position = newLocation.position;
            BlackjackManager.Instance.NewHand();

            UpdateCameras(newLocation.position);
        }
    }

   

    private void UpdateCameras(Vector3 newPosition)
    {
        arenaCamera.Priority = 10;
        sideRoomCamera.Priority = 10;
        arenaCamera.gameObject.transform.position = newLocation.position;
    }
}
