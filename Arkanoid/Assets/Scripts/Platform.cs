using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    public float m_maxOffset;
    Vector3 m_startPosition;

    public GameObject m_rightTurret;
    public GameObject m_leftTurret;

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
        m_rightTurret.SetActive(m_isFireMode);
        m_leftTurret.SetActive(m_isFireMode);
    }
    public void Fire()
    {
        if (m_isFireMode)
        {
            Vector3 leftTurretPos = m_leftTurret.transform.position;
            Vector3 rightTurretPos = m_rightTurret.transform.position;

            Quaternion rotation = Quaternion.AngleAxis(90, Vector3.left);

            Bullet leftBullet = Instantiate(m_bullet, leftTurretPos, rotation);
            Bullet rightBullet = Instantiate(m_bullet, rightTurretPos, rotation);
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
