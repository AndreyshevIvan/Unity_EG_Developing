using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    protected bool m_isImmortal = false;
    public int m_health;

    public Material m_demageMaterial;

    private void OnCollisionEnter(Collision collision)
    {
        if(!IsImmortal())
        {
            Debug.Log("Health down");
            m_health--;
        }

        CollisionEvents();
    }
    protected virtual void CollisionEvents() { }

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
