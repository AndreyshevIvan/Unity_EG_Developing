using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{

    public GameObject m_platform;
    Rigidbody m_body;
    public Vector3 m_startPosition;

    public float m_speed = 20;

    private void Awake()
    {
        m_body = m_platform.GetComponent<Rigidbody>();
    }
    public void Init()
    {
        Reset();
    }
    public void Reset()
    {
        m_body.position = m_startPosition;
    }

    public void HandlePlatfomEvents()
    {
        float dt = Time.deltaTime;
        Vector3 movement = Vector3.zero;
        Vector3 currentPos = m_platform.transform.position;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            movement = new Vector3(-m_speed * dt, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            movement = new Vector3(m_speed * dt, 0, 0);
        }

        m_body.MovePosition(currentPos + movement);
    }
}
