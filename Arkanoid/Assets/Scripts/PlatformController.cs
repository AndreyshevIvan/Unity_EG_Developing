using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{

    GameObject m_platform;
    Vector3 m_startPosition;

    public float m_speed = 20;
    float m_maxOffset = 0;

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

    public void HandleEventsAndUpdate()
    {
        HandleEvents();
        UpdatePlatform();
    }
    void HandleEvents()
    {
        Vector3 movement = Vector3.zero;
        Vector3 currentPos = m_platform.transform.position;
        float platformHalfWidth = m_platform.transform.localScale.x / 2.0f;

        bool isLeftMovAllowed = (currentPos.x - platformHalfWidth) > -m_maxOffset;
        bool isRightMovAllowed = (currentPos.x + platformHalfWidth) < m_maxOffset;

        if (Input.GetKey(KeyCode.LeftArrow) && isLeftMovAllowed)
        {
            movement = new Vector3(-m_speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow) && isRightMovAllowed)
        {
            movement = new Vector3(m_speed * Time.deltaTime, 0, 0);
        }

        m_platform.transform.position = currentPos + movement;
    }
    void UpdatePlatform()
    {

    }
}
