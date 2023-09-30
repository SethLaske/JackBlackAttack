using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{

    public int numberOfEnemies;
    //determined by level
    private Vector3 spawnLocation;

    public GameObject one;
    public GameObject two;
    public GameObject three;
    public GameObject four;
    public GameObject five;
    public GameObject six;
    public GameObject seven;
    public GameObject eight;
    public GameObject nine;
    public GameObject ten;
    public GameObject eleven;
    public GameObject twelve;
    public GameObject thirteen;

    // Start is called before the first frame update
    public void Start()
    {
        spawnLocation = gameObject.transform.position;
        numberOfEnemies = 2; //LevelManager.cardNumber;
        SpawnTheEnemies(numberOfEnemies);
    }

    public void SpawnTheEnemies(int enemyCount)
    {
        switch (enemyCount)
        {
            case 1:
                Instantiate(one, spawnLocation, Quaternion.identity);
                break;
            case 2:
                Debug.Log("Spawned Card of 2");
                Instantiate(two, spawnLocation, Quaternion.identity);
                break;
            case 3:
                Instantiate(three, spawnLocation, Quaternion.identity);
                break;
            case 4:
                Instantiate(four, spawnLocation, Quaternion.identity);
                break;
            case 5:
                Instantiate(five, spawnLocation, Quaternion.identity);
                break;
            case 6:
                Instantiate(six, spawnLocation, Quaternion.identity);
                break;
            case 7:
                Instantiate(seven, spawnLocation, Quaternion.identity);
                break;
            case 8:
                Instantiate(eight, spawnLocation, Quaternion.identity);
                break;
            case 9:
                Instantiate(nine, spawnLocation, Quaternion.identity);
                break;
            case 10:
                Instantiate(ten, spawnLocation, Quaternion.identity);
                break;
            case 11:
                Instantiate(eleven, spawnLocation, Quaternion.identity);
                break;
            case 12:
                Instantiate(twelve, spawnLocation, Quaternion.identity);
                break;
            case 13:
                Instantiate(thirteen, spawnLocation, Quaternion.identity);
                break;
            default:
                // Handle cases other than 1-13 (if needed)
                break;
        }
    }

}
