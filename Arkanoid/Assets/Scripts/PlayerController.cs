using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject m_wall;

    public UIController m_UIController;
    public BallsController m_ballsController;

    float m_wallDuration = 15;
    float m_currWallDuration = 0;

    int m_points;
    int m_ballsCount;

    int m_life;

    public void Start()
    {
        m_life = 3;
        SetWallActive(false);
    }
    public void ResetToNextLife()
    {
        SetWallActive(false);
    }

    void FixedUpdate()
    {
        UpdateData();

        UpdateWall();

        UpdateUI();
    }
    void UpdateData()
    {
        m_ballsCount = m_ballsController.GetBallsCount();
    }
    void UpdateUI()
    {
        m_UIController.UpdateWall(m_currWallDuration, m_wallDuration);
        m_UIController.UpdatePoints(m_points);
        m_UIController.UpdateLife(m_life);
        m_UIController.UpdateBalls(m_ballsCount);
    }

    public void MuliplyBalls()
    {
        m_ballsController.DoubleAll();
    }

    public void SetWallActive(bool isActive)
    {
        m_wall.SetActive(isActive);
    }
    void UpdateWall()
    {
        if (m_wall.active)
        {
            m_currWallDuration += Time.deltaTime;

            if (m_currWallDuration >= m_wallDuration)
            {
                m_currWallDuration = 0;
                SetWallActive(false);
            }
        }
    }
    

    public bool IsPlayerLive()
    {
        return (m_life > 0);
    }
    public void AddLife()
    {
        m_life++;
    }
    public void ReduceLife()
    {
        m_life--;
    }

    public void AddPoints(int points)
    {
        m_points += points;
    }
    public void ReducePoints(int reducePoints)
    {
        m_points -= reducePoints;
    }
}
