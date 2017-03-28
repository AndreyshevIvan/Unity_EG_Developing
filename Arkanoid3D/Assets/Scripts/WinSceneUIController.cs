using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WinSceneUIController : MonoBehaviour
{

    public InfoController m_info;
    public Text m_scoreField;

    int m_points = 0;
    float m_addingTime = 0;
    int m_pointsPerAdd = 1;

    const float ADDING_POINTS_SPEED = 0.02f;
    const int SPEED_IN_PERCENTS = 70;

    private void Awake()
    {
        m_scoreField.text = "0";
        m_points = m_info.GetLastLevelPoints();
        SetPointsPerAdd();
    }

    private void FixedUpdate()
    {
        UpdatePoints(m_points);
    }

    public void UpdatePoints(int points)
    {
        int currPoints = int.Parse(m_scoreField.text);
        int pointsToAdd = points - currPoints;

        m_addingTime += Time.deltaTime;

        if (m_addingTime >= ADDING_POINTS_SPEED && pointsToAdd != 0)
        {
            if (pointsToAdd >= m_pointsPerAdd)
            {
                AddPointsToText(m_pointsPerAdd);
            }
            else
            {
                AddPointsToText(pointsToAdd);
            }

            m_addingTime = 0;
        }
    }
    void AddPointsToText(int points)
    {
        int currPoints = int.Parse(m_scoreField.text);
        int newPoints = currPoints + points;

        m_scoreField.text = newPoints.ToString();
    }
    void SetPointsPerAdd()
    {
        m_pointsPerAdd = m_points / SPEED_IN_PERCENTS;
    }
}
