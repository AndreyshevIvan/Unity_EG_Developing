using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

    GameObject m_ball;
    Rigidbody m_ballBody;
    GameObject m_platform;
    public float m_onPlatformOffset;

    bool m_isBallMov = false;

    public void Init(GameObject ball, GameObject platform)
    {
        m_ball = ball;
        m_platform = platform;
        m_ballBody = m_ball.GetComponent<Rigidbody>();
        Reset();
    }
    public void Reset()
    {
        MagnetToPlatform();
    }

    public void StartBall()
    {
        m_isBallMov = true;

        m_ballBody.AddForce(new Vector3(500, 0, 500));
    }
    public void StopBall()
    {
        m_isBallMov = false;
    }

    public void HandleBallEvents()
    {
        if (!m_isBallMov && Input.GetKey(KeyCode.S))
        {
            StartBall();
        }

        UpdateBall(m_isBallMov);
    }
    private void UpdateBall(bool isBallMov)
    {
        if (isBallMov)
        {

        }
        else
        {
            MagnetToPlatform();
        }
    }
    void MagnetToPlatform()
    {
        float platformPosX = m_platform.transform.position.x;
        float platformPosY = m_platform.transform.position.y;
        float platformPosZ = m_platform.transform.position.z;

        m_ballBody.position = new Vector3(platformPosX, platformPosY, platformPosZ + m_onPlatformOffset);
    }

}
