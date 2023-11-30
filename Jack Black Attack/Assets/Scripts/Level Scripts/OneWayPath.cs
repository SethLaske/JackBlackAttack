using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPath : MonoBehaviour
{
    [SerializeField] private Collider2D blockArea;
    [SerializeField] private Vector2 correctDirection;
    [SerializeField] private GameObject ropes;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (player != null && collision.gameObject != player) {
            return;
        }
        if (Vector3.Dot(transform.position - collision.transform.position, correctDirection) > 0)
        {
            blockArea.enabled = false;
            ropes.SetActive(false);
        }
        else {
            blockArea.enabled = true;
            ropes.SetActive(true);
        }
    }
}
