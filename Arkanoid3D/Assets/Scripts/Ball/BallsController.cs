using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsController : MonoBehaviour
{
    public Ball m_ball;
    List<Ball> m_ballsOnMap;

    public GameObject platform;

    bool m_isGameStart = false;
    int m_ballsCount;

    const int MAX_BALLS_COUNT = 64;
    const float START_FORCE = 6500;
    public Vector3 ON_PLATFORM_POS = new Vector3(0, 0, 0.6f);

    void Awake()
    {
        m_ballsOnMap = new List<Ball>();
    }
    public void Reset()
    {
        CreateOnlyOneBall();
        m_isGameStart = false;
    }
    public void StartPlaying(bool isGameStart)
    {
        m_isGameStart = isGameStart;

        if (m_isGameStart)
        {
            foreach (Ball ball in m_ballsOnMap)
            {
                ball.SetForce(new Vector3(0, 0, START_FORCE));
            }
        }
    }

    void CreateOnlyOneBall()
    {
        ClearBalls();
        Ball newBall = Instantiate(m_ball, Vector3.zero, Quaternion.identity);
        newBall.transform.SetParent(transform);
        m_ballsOnMap.Add(newBall);
        m_ballsCount = GetBallsCount();
    }

    void FixedUpdate()
    {
        UpdateBalls();
    }
    void UpdateBalls()
    {
        if (!m_isGameStart)
        {
            Vector3 platformPosition = platform.transform.position;
            Vector3 posOnPlatform = platformPosition + ON_PLATFORM_POS;

            SetPosition(posOnPlatform);
        }

        m_ballsCount = GetBallsCount();

        CheckBallsExist();
    }
    void CheckBallsExist()
    {
        List<Ball> toDelete = new List<Ball>();

        foreach(Ball ball in m_ballsOnMap)
        {
            if (!ball.IsLive())
            {
                toDelete.Add(ball);
            }
        }

        foreach(Ball ball in toDelete)
        {
            ball.DestroyBall();
            m_ballsOnMap.Remove(ball);
        }

        toDelete.Clear();
    }

    void SetPosition(Vector3 position)
    {
        foreach (Ball ball in m_ballsOnMap)
        {
            ball.SetPosition(position);
        }
    }
    public void SetFireMode(bool isFireModeOn)
    {
        foreach(Ball ball in m_ballsOnMap)
        {
            ball.SetFireMode(isFireModeOn);
        }
    }

    public int GetBallsCount()
    {
        return m_ballsOnMap.Count;
    }
    public int GetCriticalDemage()
    {
       return m_ball.GetCriticalDemage();
    }

    public void PauseBalls(bool isFreeze)
    {
        if (m_ballsOnMap != null)
        {
            foreach (Ball ball in m_ballsOnMap)
            {
                ball.Pause(isFreeze);
            }
        }
    }
    public bool DoubleAll()
    {
        if (m_ballsCount * 2 <= MAX_BALLS_COUNT)
        {
            List<Ball> toDouble = new List<Ball>();

            foreach (Ball ball in m_ballsOnMap)
            {
                Ball newBall = ball.CreateDublicate();
                toDouble.Add(newBall);
            }

            foreach (Ball dublicateBall in toDouble)
            {
                dublicateBall.transform.SetParent(transform);
                m_ballsOnMap.Add(dublicateBall);
            }

            toDouble.Clear();
            m_ballsCount = 2 * m_ballsCount;

            return true;
        }

        return false;
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
