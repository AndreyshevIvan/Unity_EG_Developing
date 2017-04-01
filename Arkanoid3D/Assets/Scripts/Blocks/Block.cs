using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : LivingBody
{
    public int m_points = 0;
    public Material m_material;

    void Awake()
    {
        PersonalAwake();
    }
    protected virtual void PersonalAwake() { }

    void FixedUpdate()
    {
        UpdateTexture();
    }
    void UpdateTexture()
    {
        if (m_health != m_startHealth && m_health > (float)m_startHealth / 3)
        {
            // low demaged
        }
        else
        {
            // large demaged
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
