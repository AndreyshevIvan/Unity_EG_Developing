using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHearth : MonoBehaviour
{
    bool m_isDead = false;
    Animation m_deadAnim;

    Vector3 m_startPosition;
    Quaternion m_startRotation;

    const string KILL_CLIP_NAME = "HearthKill";
    const string RISE_CLIP_NAME = "HearthRise";

    void Start()
    {
        m_deadAnim = GetComponent<Animation>();
        m_startPosition = gameObject.transform.position;
        m_startRotation = gameObject.transform.rotation;
    }

    private void FixedUpdate()
    {
        if (m_isDead && !m_deadAnim.isPlaying)
        {
            gameObject.SetActive(false);
        }
    }

    public void Rise()
    {
        gameObject.transform.position = m_startPosition;
        gameObject.transform.rotation = m_startRotation;
        gameObject.SetActive(true);
        m_isDead = false;
        m_deadAnim.Play(RISE_CLIP_NAME);
    }
    public void Kill()
    {
        m_isDead = true;
        m_deadAnim.Play(KILL_CLIP_NAME);
    }

    public bool IsDead()
    {
        return m_isDead;
    }
}