using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    protected bool m_isImmortal = false;
    public int m_health;
    int m_startHealth;
    public int m_points = 0;
    int m_demageCount = 0;

    public Material m_lowDemageMaterial;
    public Material m_hardDemageMaterial;

    void Awake()
    {
        PersonalAwake();
        m_startHealth = m_health;
    }
    protected virtual void PersonalAwake() { }

    void OnCollisionEnter(Collision collision)
    {
        CollisionEvents();

        if (!IsImmortal())
        {
            m_demageCount++;
        }
    }
    protected virtual void CollisionEvents() { }

    void FixedUpdate()
    {
        if (m_health < m_startHealth)
        {
            SetMaterial(m_lowDemageMaterial);
        }
    }

    public int GetPoints()
    {
        return m_points;
    }
    public int GetDmgCount()
    {
        return m_demageCount;
    }
    public void SetDmgCount(int count)
    {
        m_demageCount = count;
    }
    public void SetDemage(int dmg)
    {
        if (!IsImmortal())
        {
            m_health -= dmg;
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

        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }
    protected virtual void DestroyEvents() { }
}
