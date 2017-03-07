using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    Vector3 m_force;
    Vector3 m_freezePosition;
    bool m_isFreeze = false;
    int m_bitMask;

    private void Awake()
    {

    }

    public Ball GetDublicate()
    {
        Ball dublicate = Instantiate(this, GetPosition(), Quaternion.identity);
        dublicate.SetInvertForce();

        return dublicate;
    }
    public Vector3 GetPosition()
    {
        Vector3 position = gameObject.transform.position;

        return position;
    }
    Rigidbody GetRigidbody()
    {
        return gameObject.GetComponent<Rigidbody>();
    }
    public Vector3 GetForce()
    {
        return m_force;
    }
    public int GetLayer()
    {
        return gameObject.layer;
    }

    public void SetFreeze(bool isFreeze)
    {
        m_freezePosition = GetPosition();
        m_isFreeze = isFreeze;
    }
    public void SetPosition(Vector3 newPosition)
    {
        gameObject.transform.position = newPosition;
    }
    public void SetForce(Vector3 force)
    {
        m_force = force;
        GetRigidbody().AddForce(m_force);
    }
    void SetInvertForce()
    {
        Vector3 velocity = GetRigidbody().velocity;
        Vector3 force = Vector3.zero;

        Debug.Log(velocity);

        if (velocity.x > 0 && velocity.z > 0)
        {
            force = new Vector3(-500, 0, 500);
        }
        else if (velocity.x < 0 && velocity.z > 0)
        {
            force = new Vector3(500, 0, 500);
        }
        else if (velocity.x < 0 && velocity.z < 0)
        {
            force = new Vector3(500, 0, -500);
        }
        else if (velocity.x > 0 && velocity.z < 0)
        {
            force = new Vector3(-500, 0, -500);
        }
        SetForce(force);
    }

    private void FixedUpdate()
    {
        if (m_isFreeze)
        {
            SetPosition(m_freezePosition);
        }
    }

    public void DestroyBall()
    {
        DestroyEvents();

        Destroy(gameObject);
    }
    void DestroyEvents() { }
}
