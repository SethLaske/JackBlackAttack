using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealerParticles : MonoBehaviour
{
    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;

    public int damage;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

        Debug.Log("Collision with " + other.name);
        if (other.gameObject.tag == "Player") {
            other.GetComponent<PlayerDungeonController>()?.TakeDamage(damage * numCollisionEvents);
            Debug.Log("Damaging the player: " + damage * numCollisionEvents);
        }
    }
}
