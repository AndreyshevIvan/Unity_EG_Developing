using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHearth : MonoBehaviour
{
    bool m_isDead = false;
    Animation m_deadAnim;

    void Start()
    {
        m_deadAnim = GetComponent<Animation>();
    }

    private void FixedUpdate()
    {
        if (m_isDead && !m_deadAnim.isPlaying)
        {
            Destroy(gameObject);
        }
    }

    public void Kill()
    {
        m_isDead = true;
        m_deadAnim.Play();
    }
}