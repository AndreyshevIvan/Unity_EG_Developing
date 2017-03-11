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

    public void Awake()
    {
        m_startPosition = gameObject.transform.position;
        SetAttackMode(false);
    }
    public void Reset()
    {
        gameObject.transform.position = m_startPosition;
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
            Vector3 platformPosition = gameObject.transform.position;

            Vector3 platformSize = gameObject.transform.localScale;
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
    void HandleMoveing()
    {
        float movement = 0;
        Vector3 currentPos = gameObject.transform.position;
        float platformWidth = gameObject.transform.localScale.x;

        bool isLeftMovAllowed = (currentPos.x - platformWidth) > -m_maxOffset;
        bool isRightMovAllowed = (currentPos.x + platformWidth) < m_maxOffset;

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
    void HandleFire()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Fire();
        }
    }

    public void UpdatePlatform()
    {

    }
}
