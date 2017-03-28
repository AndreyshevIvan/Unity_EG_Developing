using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    public float m_maxOffset;
    Vector3 m_startPosition;

    public GameObject m_guns;
    public GameObject m_bullets;
    public Transform m_bulletsParent;

    public Bullet m_bullet;
    bool m_isFireMode = false;

    float m_distance;
    float m_heightOnFloor;
    float m_fireColdown = 0;

    const float FIRE_COLDOWN = 0.15f;
    const float GUNS_OFFSET = 0.9f;
    const float PLATFORM_SPEED = 20;

    public void Awake()
    {
        m_startPosition = transform.position;
        m_distance = (transform.position - Camera.main.transform.position).magnitude;

        m_heightOnFloor = transform.position.y;
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
        if (m_fireColdown >= FIRE_COLDOWN)
        {
            Vector3 leftTurretPos = m_guns.transform.position - new Vector3(GUNS_OFFSET, 0, 0);
            Vector3 rightTurretPos = m_guns.transform.position + new Vector3(GUNS_OFFSET, 0, 0);
            Quaternion rotation = Quaternion.AngleAxis(90, Vector3.left);

            Bullet bulletLeft = Instantiate(m_bullet, leftTurretPos, rotation);
            Bullet bulletRight = Instantiate(m_bullet, rightTurretPos, rotation);

            bulletLeft.transform.SetParent(m_bulletsParent);
            bulletRight.transform.SetParent(m_bulletsParent);

            m_fireColdown = 0;
        }
    }
    public void UpdatePlatform()
    {
        UpdateColdowns();
        HandleEvents();
    }
    private void UpdateColdowns()
    {
        if (m_fireColdown < FIRE_COLDOWN)
        {
            m_fireColdown += Time.deltaTime;
        }
    }
    public void HandleEvents()
    {
        HandleMoveing();
        HandleFire();
    }
    void HandleFire()
    {
        if (m_isFireMode & Input.GetMouseButton(0))
        {
            Fire();
        }
    }
    void HandleMoveing()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = m_distance;

        Vector3 newPosition = new Vector3(0, m_heightOnFloor, m_startPosition.z);
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
}
