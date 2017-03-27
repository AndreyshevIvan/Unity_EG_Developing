using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    public float m_maxOffset;
    Vector3 m_startPosition;

    public GameObject m_guns;
    public GameObject m_bullets;

    public Bullet m_bullet;
    bool m_isFireMode = false;

    public float m_speed = 20;

    float m_distance;
    float m_height;

    public void Awake()
    {
        m_startPosition = transform.position;
        m_distance = (transform.position - Camera.main.transform.position).magnitude;

        m_height = transform.position.y;
        SetAttackMode(false);
    }
    public void Reset()
    {
        transform.position = m_startPosition;
        SetAttackMode(false);
    }

    public void SetAttackMode(bool isModeOn)
    {
        m_isFireMode = isModeOn;
        m_guns.SetActive(m_isFireMode);
    }
    public void Fire()
    {
        if (m_isFireMode)
        {
            m_guns.GetComponentInChildren<PlatformGuns>().Fire();

            Vector3 platformPosition = transform.position;

            Vector3 platformSize = transform.localScale;
            Vector3 offsetFromCenter = new Vector3(platformSize.x / 2.0f, 0, 0);

            Vector3 leftTurretPos = m_guns.transform.position - offsetFromCenter;
            Vector3 rightTurretPos = m_guns.transform.position + offsetFromCenter;

            leftTurretPos.y = platformPosition.y;
            rightTurretPos.y = platformPosition.y;

            Quaternion rotation = Quaternion.AngleAxis(90, Vector3.left);

            Instantiate(m_bullet, leftTurretPos, rotation);
            Instantiate(m_bullet, rightTurretPos, rotation);
        }
    }

    public void HandleEvents()
    {
        HandleMoveing();
        HandleFire();
    }
    void HandleFire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }
    void HandleMoveing()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = m_distance;

        Vector3 newPosition = new Vector3(0, m_height, m_startPosition.z);
        newPosition.x = Camera.main.ScreenToWorldPoint(mousePosition).x;
        if (newPosition.x < m_maxOffset && newPosition.x > -m_maxOffset)
        {
            transform.position = newPosition;
        }
        else if (newPosition.x >= m_maxOffset)
        {
            transform.position = new Vector3(m_maxOffset, newPosition.y, newPosition.z);
        }
        else if (newPosition.x <= m_maxOffset)
        {
            transform.position = new Vector3(-m_maxOffset, newPosition.y, newPosition.z);
        }
    }

    public void UpdatePlatform()
    {

    }
}
