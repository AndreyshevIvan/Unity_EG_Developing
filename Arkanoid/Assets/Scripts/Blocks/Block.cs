using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    protected bool m_isImmortal = false;
    public int m_health;
    public int m_points = 0;

    public Material m_lowDemageMaterial;
    public Material m_hardDemageMaterial;

    void Awake()
    {
        PersonalAwake();
    }
    protected virtual void PersonalAwake() { }

    void OnCollisionEnter(Collision collision)
    {
        if (!IsImmortal())
        {
            m_health--;
        }
    }

    void FixedUpdate()
    {

    }

    public int GetPoints()
    {
        return m_points;
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
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }
}
