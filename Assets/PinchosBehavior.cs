using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchosBehavior : MonoBehaviour
{
    public float damage = 1f;
    public float bounceForce = 5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
           EnergyTracker  energyTracker = collision.gameObject.GetComponent<EnergyTracker>();
        if (energyTracker != null)
        {
            energyTracker.consumeEnergy();
        }

        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 bounceDirection = (collision.transform.position - transform.position).normalized;
            rb.AddForce(bounceDirection * bounceForce, ForceMode2D.Impulse);
        }
    }
}
