using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangProjectile : MonoBehaviour
{
    public Transform thrower;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float damage;
    public Vector2 initialDirection;
    [SerializeField] private float throwTime;
    [SerializeField] private float hangTime;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    public void StartBoomerang() {
        StartCoroutine(BoomerangThrown());
    }

    IEnumerator BoomerangThrown () {
        rb.velocity = initialDirection.normalized * moveSpeed;

        yield return new WaitForSeconds(throwTime);

        rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(hangTime);

        rb.velocity = (thrower.position - transform.position).normalized * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject other = collider.gameObject;
        BaseEnemy enemyEntity = collider.GetComponent<BaseEnemy>();

        if (enemyEntity != null)
        {
            enemyEntity.TakeDamage(damage);
            return;
            //hitSomething = true;
        }

        if (other.CompareTag("Player") || other.CompareTag("Spikes") || other.CompareTag("Projectile") || other.CompareTag("Gold"))
        {
            return;
        }

        Destroy(gameObject);
    }
}
