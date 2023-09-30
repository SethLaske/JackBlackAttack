using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goldScript : MonoBehaviour
{
    public int goldValue;
    public SpriteRenderer sprite;

    public void Start()
    {
        StartCoroutine(timeAvailable());
        sprite = GetComponent<SpriteRenderer>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Increasing gold score by: " + goldValue);
            Destroy(gameObject);
        }
    }

    public IEnumerator timeAvailable()
    {
        yield return new WaitForSeconds(8f);
        sprite.enabled = false;
        yield return new WaitForSeconds(1f);
        sprite.enabled = true;
        yield return new WaitForSeconds(1f);
        sprite.enabled = false;
        yield return new WaitForSeconds(1f);
        sprite.enabled = true;
        yield return new WaitForSeconds(1f);
        sprite.enabled = false;
        yield return new WaitForSeconds(.5f);
        sprite.enabled = true;
        yield return new WaitForSeconds(.5f);
        sprite.enabled = false;
        yield return new WaitForSeconds(.5f);
        sprite.enabled = true;
        yield return new WaitForSeconds(.5f);
        sprite.enabled = false;
        yield return new WaitForSeconds(.5f);
        sprite.enabled = true;
        yield return new WaitForSeconds(.5f);
        sprite.enabled = false;
        yield return new WaitForSeconds(.25f);
        sprite.enabled = true;
        yield return new WaitForSeconds(.25f);
        sprite.enabled = false;
        yield return new WaitForSeconds(.25f);
        sprite.enabled = true;
        yield return new WaitForSeconds(.25f);
        sprite.enabled = false;
        yield return new WaitForSeconds(.125f);
        sprite.enabled = true;
        yield return new WaitForSeconds(.125f);
        sprite.enabled = false;
        yield return new WaitForSeconds(.125f);
        sprite.enabled = true;
        yield return new WaitForSeconds(.125f);
        sprite.enabled = false;
        yield return new WaitForSeconds(.0625f);
        Destroy(gameObject);


    }
}
