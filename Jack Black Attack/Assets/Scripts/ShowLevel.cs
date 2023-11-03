using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLevel : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject playerObject = null;
    public CinemachineVirtualCamera virtualCamera;
    public Transform cameraTransform;
    [SerializeField]
    private float xStart = 0.0f;
    [SerializeField]
    private float xEnd = 0.0f;
    [SerializeField]
    private float yStart = 0.0f;
    [SerializeField]
    private float yEnd = 0.0f;
    [SerializeField]
    private float cameraSpeed = 0.0f;
    private Boolean move = true;

    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        cameraTransform = transform;
        virtualCamera = cameraTransform.GetComponent<CinemachineVirtualCamera>();
        cameraTransform.position = new Vector3(xStart, yStart, -10);
    }
    // Update is called once per frame
    void Update()
    {
        if (cameraTransform.transform.position.y > yEnd && move == true)
        {
            cameraDown();
        }
        else
        {
            move = false;
            cameraTransform.position = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y, -10);
            //virtualCamera.Priority = 8;
            virtualCamera.enabled = false;
        }
    }

    private void cameraDown()
    {
        cameraTransform.Translate(new Vector3(xEnd, cameraSpeed, 0));
    }
}
