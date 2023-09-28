using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goldScript : MonoBehaviour
{
    public int goldValue;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Increasing gold score by: " + goldValue);
            Destroy(gameObject);
        }
    }
}
