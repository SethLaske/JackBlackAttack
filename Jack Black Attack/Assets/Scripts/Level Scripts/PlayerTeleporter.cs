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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.transform.position = newLocation.position;

            //arenaCamera.gameObject.SetActive(false);
            //sideRoomCamera.gameObject.SetActive(true);
            //sideRoomCamera.gameObject.transform.position = newLocation.position;
        }
    }
}
