using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{

    GameObject m_platform;
    Vector3 m_startPosition;

    public float m_speed = 20;
    private float m_maxOffset = 0;

    public void Init(GameObject platform, float floorWidth)
    {
        m_platform = platform;
        m_maxOffset = floorWidth / 2;
        m_startPosition = m_platform.transform.position;

        Reset();
    }
    public void Reset()
    {
        m_platform.transform.position = m_startPosition;
    }

    public void HandlePlatfomEvents()
    {
        float dt = Time.deltaTime;
        Vector3 movement = Vector3.zero;
        Vector3 currentPos = m_platform.transform.position;

        if (Input.GetKey(KeyCode.LeftArrow) && currentPos.x > -m_maxOffset)
        {
            movement = new Vector3(-m_speed * dt, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow) && currentPos.x < m_maxOffset)
        {
            movement = new Vector3(m_speed * dt, 0, 0);
        }

        m_platform.transform.position = currentPos + movement;
    }
}
