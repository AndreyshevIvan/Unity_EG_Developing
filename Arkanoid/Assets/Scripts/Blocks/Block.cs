using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    public GameObject m_body;

    protected bool m_isImmortal = false;
    public int m_health;

    public Block()
    {
        SetImmortal();
    }

    protected virtual void SetImmortal()
    {
        m_isImmortal = false;
    }

    public bool IsLive()
    {
        return (m_health > 0);
    }
    public bool IsImmortal()
    {
        return m_isImmortal;
    }

    public virtual void DestroyBlock()
    {
        Destroy(m_body);
        Destroy(this);
    }
}
