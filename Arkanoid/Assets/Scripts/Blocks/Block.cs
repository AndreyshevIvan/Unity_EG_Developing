using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : LivingBody
{

    public int m_points = 0;

    public Material m_lowDemageMaterial;
    public Material m_hardDemageMaterial;

    void Awake()
    {
        m_startHealth = m_health;
        PersonalAwake();
    }
    protected virtual void PersonalAwake() { }

    void FixedUpdate()
    {
        if (m_health < m_startHealth)
        {
            gameObject.GetComponent<MeshRenderer>().material = m_hardDemageMaterial;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material = m_lowDemageMaterial;
        }
    }

    public int GetPoints()
    {
        return m_points;
    }

    public virtual void DestroyBlock()
    {
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }
}
