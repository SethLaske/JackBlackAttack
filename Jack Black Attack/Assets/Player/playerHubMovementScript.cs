using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHubMovementScript : MonoBehaviour
{

    public float moveSpeed = 5.0f;
    public Rigidbody2D rb;
    private Vector2 moveDirection;

    public Transform detectionPoint;
    private const float detectionRadius = 0.2f;
    public LayerMask detectionlayer;

    //Cacjed Trigger Object
    public GameObject detectedObject;

    void Start()
    {

    }

    void Update()
    {
        ProcessInputs();

        if(DetectObject())
        {
            if(InteractInput())
            {
                detectedObject.GetComponent<Item>().Interact();
            }
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    bool InteractInput()
    {
        return Input.GetKeyDown(KeyCode.E);
    }

    bool DetectObject()
    {
        Collider2D obj = Physics2D.OverlapCircle(detectionPoint.position, detectionRadius, detectionlayer);
        if(obj == null)
        {
            detectedObject = null;
            return false;
        } else {
            detectedObject = obj.gameObject;
            return true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(detectionPoint.position, detectionRadius);
    }
}
