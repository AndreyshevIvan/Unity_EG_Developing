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
        float movement = 0;
        Vector3 currentPos = gameObject.transform.position;
        float platformHalfWidth = gameObject.transform.localScale.x / 2.0f;

        bool isLeftMovAllowed = (currentPos.x - platformHalfWidth) > -m_maxOffset;
        bool isRightMovAllowed = (currentPos.x + platformHalfWidth) < m_maxOffset;

        if (isLeftMovAllowed && Input.GetKey(KeyCode.LeftArrow))
        {
            movement = -m_speed * Time.deltaTime;
        }
        if (isRightMovAllowed && Input.GetKey(KeyCode.RightArrow))
        {
            movement = m_speed * Time.deltaTime;
        }

        gameObject.transform.position = currentPos + (new Vector3(movement, 0, 0));
    }
    void UpdatePlatform()
    {

    }
}
