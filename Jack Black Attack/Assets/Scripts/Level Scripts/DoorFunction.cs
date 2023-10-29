using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorFunction : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            // Temporary script which resets the scene after colliding with Door.
            Debug.Log("Reload Scene");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
