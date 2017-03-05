using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{

    public Camera m_camera;
    public float m_speed = 1;

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

    public void HandleCameraEvents()
    {
        UpdatePositionData();

        if (Input.GetKey(KeyCode.W))
        {
            RotateForward();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            RotateBack();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
    }

    void UpdatePositionData()
    {
        m_posX = m_camera.transform.position.x;
        m_posY = m_camera.transform.position.y;
        m_posZ = m_camera.transform.position.z;

        m_rotX = m_camera.transform.rotation.x;
        m_rotY = m_camera.transform.rotation.y;
    }

    private void RotateForward()
    {

    }
    private void RotateBack()
    {

    }
    private void RotateLeft()
    {

    }
    private void RotateRight()
    {

    }
}
