using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameObject m_demageCollider;

    public Material m_fireMaterial;
    public Material m_basicMaterial;

    public int m_basicLayer;
    public int m_fireLayer;

    public float m_criticalPosition;

    Rigidbody m_body;
    Vector3 m_saveVelocity = Vector3.zero;

    private void Awake()
    {
        m_body = gameObject.GetComponent<Rigidbody>();
    }

    public Ball CreateDublicate()
    {
        Ball dublicate = Instantiate(this, GetPosition(), Quaternion.identity);
        dublicate.Stop();
        Vector3 parentVelocity = m_body.velocity;

        Vector3 dublicateForce = GetDublicateVelocity(parentVelocity);
        dublicate.SetVelocity(dublicateForce);

        return dublicate;
    }

    public Vector3 GetPosition()
    {
        Vector3 position = transform.position;

        return position;
    }
    public int GetCriticalDemage()
    {
        DemageBallCollider dmgCollider = m_demageCollider.GetComponent<DemageBallCollider>();

        return (dmgCollider.GetFireDemage());
    }
    public int GetFireballLayer()
    {
        return m_fireLayer;
    }
    Vector3 GetDublicateVelocity(Vector3 parentVelocity)
    {
        Vector3 newVelocity = new Vector3(0, 0, 0);

        int randomQuarter = Random.Range(0, 4);
        float randomCos = Random.Range(-1.0f, 1.0f);

        float absoluteVelocity = Mathf.Sqrt(Mathf.Pow(parentVelocity.x, 2) + Mathf.Pow(parentVelocity.z, 2));
        newVelocity.x = absoluteVelocity * randomCos;
        newVelocity.z = Mathf.Sqrt(Mathf.Pow(absoluteVelocity, 2) - Mathf.Pow(newVelocity.x, 2));

        switch (randomQuarter)
        {
            case 0:
                break;

            case 1:
                newVelocity.x = -newVelocity.x;
                break;

            case 2:
                newVelocity.x = -newVelocity.x;
                newVelocity.z = -newVelocity.z;
                break;

            case 3:
                newVelocity.z = -newVelocity.z;
                break;
        }

        return newVelocity;
    }

    public void SetPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }
    public void SetForce(Vector3 force)
    {
        m_body.AddForce(force);
    }
    public void SetFireMode(bool isFireModeOn)
    {
        DemageBallCollider dmgCollider = m_demageCollider.GetComponent<DemageBallCollider>();
        dmgCollider.SetFireMode(isFireModeOn);

        if (isFireModeOn)
        {
            gameObject.layer = m_fireLayer;
            GetComponent<MeshRenderer>().material = m_fireMaterial;
        }
        else
        {
            gameObject.layer = m_basicLayer;
            GetComponent<MeshRenderer>().material = m_basicMaterial;
        }
    }
    public void SetVelocity(Vector3 velocity)
    {
        m_body.velocity = velocity;
    }

    public bool IsLive()
    {
        Vector3 currPos = transform.position;

        return (currPos.z >= m_criticalPosition);
    }

    public void Pause(bool isPause)
    {
        if (isPause)
        {
            m_saveVelocity = m_body.velocity;
            SetVelocity(Vector3.zero);
        }
        else
        {
            SetVelocity(m_saveVelocity);
        }
    }
    public void Stop()
    {
        SetVelocity(Vector3.zero);
    }

    public void DestroyBall()
    {
        DestroyEvents();

        Destroy(gameObject);
    }
    void DestroyEvents() { }
}
