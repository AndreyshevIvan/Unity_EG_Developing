using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{

    public Camera m_camera;
    public float m_speed = 1;
    public GameObject m_anchorPoint;

    public Vector3 m_maxRotation;
    public Vector3 m_minRotation;

    float m_rotationRadius;

    float m_posX;
    float m_posY;
    float m_posZ;

    float m_rotX;
    float m_rotY;

    public void Init(float rotationRadius)
    {
        m_rotationRadius = rotationRadius;

        ResetOptions();
    }
    public void ResetOptions()
    {

    }

    public void HandleEventsAndUpdate()
    {
        HandleEvents();
        UpdateCamera();
    }
    void HandleEvents()
    {
        if (Input.GetKey(KeyCode.W))
        {
            RotateForward();
        }
        else if (Input.GetKey(KeyCode.S))
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
        UpdatePositionData();
    }
    void UpdatePositionData()
    {
        m_posX = m_camera.transform.position.x;
        m_posY = m_camera.transform.position.y;
        m_posZ = m_camera.transform.position.z;

        m_rotX = m_camera.transform.rotation.x;
        m_rotY = m_camera.transform.rotation.y;
    }

    Vector3 GetCameraRotation()
    {
        Vector3 rotation = m_anchorPoint.transform.localEulerAngles;

        return rotation;
    }

    void RotateForward()
    {
        if (IsVerticalRotationAllowed(Vector3.left))
        {
            m_anchorPoint.transform.Rotate(Vector3.left);
        }
    }
    void RotateBack()
    {
        if (IsVerticalRotationAllowed(Vector3.right))
        {
            m_anchorPoint.transform.Rotate(Vector3.right);
        }
    }
    void SetStartRotation()
    {
        m_anchorPoint.transform.rotation = Quaternion.identity;
    }

    bool IsVerticalRotationAllowed(Vector3 rotation)
    {
        Vector3 futureRotation = GetCameraRotation() + rotation;

        float xRot = 360 - futureRotation.x;

        if (xRot > m_maxRotation.x && xRot < 360 + m_minRotation.x)
        {
            return false;
        }

        return true;
    }
}
