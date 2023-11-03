using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public float speed = 6.0f;
    public float lifetime = 2.0f;
    public float damage = 2.5f;
    private bool hitSomething = false;

    void Start()
    {
        StartCoroutine(Expiration());
    }

    IEnumerator Expiration()
    {
        yield return new WaitForSeconds(lifetime);

        if (!hitSomething)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject other = collider.gameObject;
        BaseEnemy enemyEntity = collider.GetComponent<BaseEnemy>();

        if (enemyEntity != null)
        {
            enemyEntity.TakeDamage(damage, transform.position);
            hitSomething = true;
        }

        if (other.CompareTag("Player") || other.CompareTag("Spikes") || other.CompareTag("Projectile") || other.CompareTag("Gold"))
        {
            return;
        }

        Destroy(gameObject);
    }
}
