using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsAnnouncement : MonoBehaviour
{
    public Text m_message;

    public float m_speed = 24;
    public float m_maxDuration = 2;
    float m_currDuration = 0;

    void FixedUpdate()
    {
        float dt = Time.deltaTime;

        m_currDuration += dt;

        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x, pos.y + m_speed * dt, pos.z);

        if(m_currDuration > m_maxDuration)
        {
            DestroyAnnounce();
        }
    }

    public void SetPoints(uint points)
    {
        m_message.text = "+" + points.ToString();
    }

    public void DestroyAnnounce()
    {
        Destroy(gameObject);
    }
}
