using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public float speed = 5.5f;
    public float lifetime = 2.0f;
    public float damage = 2.5f;
    void Start()
    {
        StartCoroutine(Expiration());
    }

    IEnumerator Expiration()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
    void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject other = collider.gameObject;
        BaseEnemy enemyEntity = collider.GetComponent<BaseEnemy>();
        if (enemyEntity != null)
        {
            enemyEntity.TakeDamage(damage);
        }
    }
}
