using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goldScript : MonoBehaviour
{
    public int goldValue;
    public SpriteRenderer sprite;

    public static event Action onGoldPickup;

    public void Start()
    {
        //StartCoroutine(timeAvailable());
        //sprite = GetComponent<SpriteRenderer>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Increasing gold score by: " + goldValue);
            PlayerPrefs.SetInt("Player Gold", PlayerPrefs.GetInt("Player Gold") + goldValue);
            onGoldPickup?.Invoke();
            Destroy(gameObject);
        }
    }

    private void Despawn() {
        Destroy(gameObject);
    }

    
}
