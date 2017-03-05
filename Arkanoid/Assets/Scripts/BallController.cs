using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

    GameObject m_ball;
    Rigidbody m_ballBody;
    GameObject m_platform;
    public float m_onPlatformOffset;
    Vector3 m_force = new Vector3(500, 0, 500);
    Vector3 m_pausePosition;

    bool m_isBallStarted = false;

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
        StopBall();
        m_ballBody.AddForce(m_force);
    }

    public void StartBall()
    {
        m_ballBody.WakeUp();
    }
    public void StopBall()
    {
        m_ballBody.Sleep();
    }

    public void HandleBallEvents()
    {
        if (!m_isBallStarted && Input.GetKey(KeyCode.S))
        {
            m_isBallStarted = true;
            StartBall();
        }

        UpdateBall();
    }
    private void UpdateBall()
    {
        if (!m_isBallStarted)
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
