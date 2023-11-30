using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;


public class PlayerTeleporter : MonoBehaviour
{
    [SerializeField] private Transform newLocation;

    [SerializeField] private CinemachineVirtualCamera arenaCamera;
    [SerializeField] private CinemachineVirtualCamera sideRoomCamera;
    [SerializeField] private GameObject Pointer;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.transform.position = newLocation.position;
            Pointer.SetActive(false);

            UpdateCameras(newLocation.position);
        }
    }

    private void UpdateCameras(Vector3 newPosition)
    {   
        arenaCamera.Priority = 9;
        sideRoomCamera.Priority = 10;
        sideRoomCamera.gameObject.transform.position = newPosition;
    }
}
