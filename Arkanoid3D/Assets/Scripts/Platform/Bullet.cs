using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody body;

    public int m_demage = 1;
    const float START_FORCE = 1500;

    private void Awake()
    {
        body.AddForce(new Vector3(0, 0, START_FORCE));
    }

    private void OnTriggerEnter(Collider other)
    {
        LivingBody collideBody = other.GetComponent<LivingBody>();

        if (collideBody != null)
        {
            collideBody.AddDemage(m_demage);
        }

        Destroy(gameObject);
    }
}
