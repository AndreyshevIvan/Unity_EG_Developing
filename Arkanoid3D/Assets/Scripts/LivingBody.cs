using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingBody : MonoBehaviour
{
    public int m_health;
    protected int m_startHealth;
    bool m_isImmortal = false;

    private void Awake()
    {
        m_startHealth = m_health;
    }

    public void AddDemage(int demage)
    {
        if (!m_isImmortal)
        {
            m_health -= demage;

            if (m_health < 0)
            {
                m_health = 0;
            }
        }
    }
    public void AddHealth(int health, bool isAddToStartToo)
    {
        if (isAddToStartToo)
        {
            m_startHealth += health;
        }
        m_health += health;
    }

    public virtual bool IsLive()
    {
        return (IsImmortal() || (m_health > 0));
    }
    public bool IsImmortal()
    {
        return m_isImmortal;
    }

    public int GetHealth()
    {
        return m_health;
    }

    protected void SetImmortal(bool isImmortal)
    {
        m_isImmortal = isImmortal;
    }
}
