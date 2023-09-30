using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    private SpriteRenderer sr;
    private Collider2D hitbox;

    [SerializeField] private float trapDamage;
    [SerializeField] private float resetDuration;

    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        hitbox = gameObject.GetComponent<Collider2D>();

        StartCoroutine(ResetTrap());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character character = collision.GetComponent<Character>();

        if (character != null) {
            character.TakeDamage(trapDamage);
            StartCoroutine(ResetTrap());
        }
    }

    IEnumerator ResetTrap() {
        sr.enabled = false;
        hitbox.enabled = false;

        yield return new WaitForSeconds(resetDuration);

        sr.enabled = true;
        hitbox.enabled = true;
    }
}
