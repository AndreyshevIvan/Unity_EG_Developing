using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    public float m_maxOffset;
    Vector3 m_startPosition;

    public float m_speed = 20;

    public void Awake()
    {
        m_startPosition = gameObject.transform.position;

        Reset();
    }
    public void Reset()
    {
        gameObject.transform.position = m_startPosition;
    }

    public void HandleEventsAndUpdate()
    {
        HandleEvents();
        UpdatePlatform();
    }
    void HandleEvents()
    {
        Vector3 movement = Vector3.zero;
        Vector3 currentPos = gameObject.transform.position;
        float platformHalfWidth = gameObject.transform.localScale.x / 2.0f;

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

        gameObject.transform.position = currentPos + movement;
    }
    void UpdatePlatform()
    {

    }
}
