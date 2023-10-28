using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class Item : MonoBehaviour
{

    public enum InteractionType {NONE, PickUp, Examine, Door};
    public InteractionType type; 
    public string nextScene;

    private void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;
        gameObject.layer = 10;
    }

    public void Interact()
    {
        switch(type)
        {
            case InteractionType.PickUp:
                Debug.Log("PICK UP");
                break;
            case InteractionType.Examine:
                Debug.Log("EXAMINE");
                break;
            case InteractionType.Door:
                SceneManager.LoadScene(nextScene);
                break;
            default:
                Debug.Log("NULL ITEM");
                break;
        }
    }
}
