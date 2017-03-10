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

    public void SetDemage(int demage)
    {
        if (!m_isImmortal)
        {
            m_health -= demage;
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
        return (m_isImmortal || (m_health > 0));
    }

    protected void SetImmortal(bool isImmortal)
    {
        m_isImmortal = isImmortal;
    }
}
