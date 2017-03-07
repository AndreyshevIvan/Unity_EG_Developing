using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    protected bool m_isImmortal = false;
    public int m_health;
    int m_startHealth;

    public Material m_lowDemageMaterial;
    public Material m_hardDemageMaterial;

    private void Awake()
    {
        m_startHealth = m_health;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!IsImmortal())
        {
            m_health -= 1;
        }

        CollisionEvents();
    }
    protected virtual void CollisionEvents() { }

    private void FixedUpdate()
    {
        if (m_health < m_startHealth)
        {
            SetMaterial(m_lowDemageMaterial);
        }
    }

    void SetMaterial(Material newMaterial)
    {
        GetComponent<MeshRenderer>().material = newMaterial;
    }

    public bool IsLive()
    {
        if(!IsImmortal())
        {
            return (m_health > 0);
        }

        return false;
    }
    bool IsImmortal()
    {
        return m_isImmortal;
    }

    public virtual void DestroyBlock()
    {
        DestroyEvents();

        Destroy(gameObject);
    }
    protected virtual void DestroyEvents() { }
}
