using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableTrident : MonoBehaviour
{
    public TridentWeapon tridentParent;
    [SerializeField] private float throwForce = 20f;
    [SerializeField] private float throwDistance = 0.25f;
    private Rigidbody2D rb;
    private IEnumerator Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(throwForce, 0);
        yield return new WaitForSeconds(throwDistance);
        rb.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            tridentParent.PickUpWeapon(collision.transform);
        }
    }
}
