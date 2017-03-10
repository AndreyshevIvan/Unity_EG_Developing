using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameObject m_basicCollider;
    public GameObject m_demageCollider;

    public Material m_fireMaterial;
    public Material m_basicMaterial;

    public float m_criticalPosition;
    public int m_demage;
    Vector3 m_force;
    Vector3 m_freezePosition;
    bool m_isFreeze = false;

    public int m_basicLayer;
    public int m_fireLayer;

    public Ball GetDublicate()
    {
        Ball dublicate = Instantiate(this, GetPosition(), Quaternion.identity);
        Vector3 parentVelocity = GetRigidbody().velocity;
        dublicate.SetRandomForce(GetForce());

        return dublicate;
    }
    public Vector3 GetPosition()
    {
        Vector3 position = gameObject.transform.position;

        return position;
    }
    Rigidbody GetRigidbody()
    {
        return gameObject.GetComponent<Rigidbody>();
    }
    public Vector3 GetForce()
    {
        return m_force;
    }
    public int GetCriticalDemage()
    {
        return m_demage * 2;
    }
    public int GetBasicLayer()
    {
        return m_basicCollider.layer;
    }
    public int GetFireballLayer()
    {
        return m_fireLayer;
    }

    public void SetFreeze(bool isFreeze)
    {
        m_freezePosition = GetPosition();
        m_isFreeze = isFreeze;
    }
    public void SetPosition(Vector3 newPosition)
    {
        gameObject.transform.position = newPosition;
    }
    public void SetForce(Vector3 force)
    {
        m_force = force;
        GetRigidbody().AddForce(force);
    }
    void SetRandomForce(Vector3 parentForce)
    {
        Vector3 force = new Vector3(-parentForce.x, 0, -parentForce.z);

        SetForce(force);
    }
    public void SetFireMode(bool isFireModeOn)
    {
        if (isFireModeOn)
        {
            m_basicCollider.layer = m_fireLayer;
            gameObject.GetComponent<MeshRenderer>().material = m_fireMaterial;
        }
        else
        {
            m_basicCollider.layer = m_basicLayer;
            gameObject.GetComponent<MeshRenderer>().material = m_basicMaterial;
        }
    }

    void FixedUpdate()
    {
        if (m_isFreeze)
        {
            SetPosition(m_freezePosition);
        }
    }
    public bool IsLive()
    {
        Vector3 currPos = gameObject.transform.position;

        return (currPos.z >= m_criticalPosition);
    }

    public void DestroyBall()
    {
        DestroyEvents();

        Destroy(gameObject);
    }
    void DestroyEvents() { }
}
