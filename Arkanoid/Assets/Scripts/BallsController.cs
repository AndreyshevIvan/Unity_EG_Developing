using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsController : MonoBehaviour
{
    public Ball m_ball;
    ArrayList m_ballsOnMap;

    public Vector3 m_initPosition;

    public float m_onPlatformOffset;
    public Vector3 m_startForce = new Vector3(500, 0, 500);

    bool m_isBallsPaused = false;
    bool m_isGameStart = false;

    int m_ballsLayer;

    const int m_minBallsCount = 1;
    const int m_maxBallsCount = 64;
    int m_ballsCount;

    void Awake()
    {
        m_ballsLayer = m_ball.GetLayer();
        m_ballsOnMap = new ArrayList();
        CreateOnlyOneBall();
        Physics.IgnoreLayerCollision(m_ballsLayer, m_ballsLayer);

        Reset();
    }
    public void Reset()
    {
        CreateOnlyOneBall();
        m_ballsCount = m_minBallsCount;
        m_isGameStart = false;
    }

    void CreateOnlyOneBall()
    {
        ClearBalls();
        Ball newBall = Instantiate(m_ball, Vector3.zero, Quaternion.identity);
        m_ballsOnMap.Add(newBall);
        gameObject.AddComponent<Ball>();
        m_ballsCount = m_minBallsCount;
    }

    void FixedUpdate()
    {
        HandleEvents();
        UpdateBalls();
    }
    void HandleEvents()
    {
        if (!m_isGameStart && Input.GetKey(KeyCode.Space))
        {
            m_isGameStart = true;

            foreach (Ball ball in m_ballsOnMap)
            {
                ball.SetForce(m_startForce);
            }
        }

        if (m_isGameStart && Input.GetKeyDown(KeyCode.F))
        {
            DoubleAll();
        }
    }
    void UpdateBalls()
    {
        if (!m_isGameStart)
        {
            MagnetToPlatform();
        }
    }

    public void FreezeAll(bool isFreeze)
    {
        foreach (Ball ball in m_ballsOnMap)
        {
            ball.SetFreeze(isFreeze);
        }
    }
    void MagnetToPlatform()
    {
        foreach (Ball ball in m_ballsOnMap)
        {
            Vector3 platformPos = m_initPosition;

            ball.SetPosition(platformPos);
        }
    }
    void DoubleAll()
    {
        if (m_ballsCount < m_maxBallsCount)
        {
            ArrayList toDouble = new ArrayList();

            foreach (Ball ball in m_ballsOnMap)
            {
                toDouble.Add(ball.GetDublicate());
            }

            foreach (Ball dublicateBall in toDouble)
            {
                m_ballsOnMap.Add(dublicateBall);
            }

            toDouble.Clear();
            m_ballsCount = 2 * m_ballsCount;
        }
    }

    public void ClearBalls()
    {
        if (m_ballsOnMap != null && m_ballsOnMap.Capacity != 0)
        {
            foreach (Ball ball in m_ballsOnMap)
            {
                ball.DestroyBall();
            }

            m_ballsOnMap.Clear();
        }
    }
}
