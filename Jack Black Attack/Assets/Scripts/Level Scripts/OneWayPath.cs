using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPath : MonoBehaviour
{
    [SerializeField] private Collider2D blockArea;
    [SerializeField] private Vector2 correctDirection;
    [SerializeField] private GameObject ropes;

    private void OnTriggerStay2D(Collider2D collision)
    {
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
