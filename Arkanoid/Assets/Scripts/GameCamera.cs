using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCamera : MonoBehaviour
{

    public float m_speed = 90;
    public Vector3 m_anchor;

    public float m_maxRotation;
    public float m_minRotation;

    Quaternion m_startRotation;
    Vector3 m_startPosition;

    public void Awake()
    {
        m_startRotation = gameObject.transform.rotation;
        m_startPosition = gameObject.transform.position;

        ResetOptions();
    }
    void ResetOptions()
    {

    }

    void FixedUpdate()
    {
        HandleEvents();
        UpdateCamera();
    }
    void HandleEvents()
    {
        if (Input.GetKey(KeyCode.S))
        {
            RotateForward();
        }
        else if (Input.GetKey(KeyCode.W))
        {
            RotateBack();
        }
        else if (Input.GetKey(KeyCode.R))
        {
            SetStartRotation();
        }
    }
    void UpdateCamera()
    {

    }

    float GetCameraRotation()
    {
        Vector3 rotation = gameObject.transform.eulerAngles;

        return rotation.x;
    }

    void RotateForward()
    {
        if (IsRotationAllowed(m_speed))
        {
            gameObject.transform.RotateAround(m_anchor, Vector3.right, m_speed);
        }
    }
    void RotateBack()
    {
        if (IsRotationAllowed(-m_speed))
        {
            gameObject.transform.RotateAround(m_anchor, Vector3.right, -m_speed);
        }
    }
    void SetStartRotation()
    {
        gameObject.transform.rotation = m_startRotation;
        gameObject.transform.position = m_startPosition;
    }

    bool IsRotationAllowed(float angle)
    {
        float futureRotation = GetCameraRotation() + angle;

        if (futureRotation > m_minRotation && futureRotation < m_maxRotation)
        {
            return true;
        }

        return false;
    }
}
