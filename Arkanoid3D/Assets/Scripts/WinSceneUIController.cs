using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WinSceneUIController : MonoBehaviour
{

    public InfoController m_info;
    public Text m_pointsUI;

    int m_points = 0;
    float m_addingPointsSpeed = 0.02f;
    int m_speedInPersents = 70;
    float m_addingTime = 0;
    int m_pointsPerOneAdd = 1;

    private void Awake()
    {
        m_points = m_info.GetLastLevelPoints();
        SetPointsPerAdd();
    }

    private void FixedUpdate()
    {
        UpdatePoints(m_points);
    }

    public void UpdatePoints(int points)
    {
        int currPoints = int.Parse(m_pointsUI.text);
        int pointsToAdd = points - currPoints;

        m_addingTime += Time.deltaTime;

        if (m_addingTime >= m_addingPointsSpeed && pointsToAdd != 0)
        {
            if (pointsToAdd >= m_pointsPerOneAdd)
            {
                AddPointsToText(m_pointsPerOneAdd);
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
        int currPoints = int.Parse(m_pointsUI.text);
        int newPoints = currPoints + points;

        m_pointsUI.text = newPoints.ToString();
    }
    void SetPointsPerAdd()
    {
        m_pointsPerOneAdd = m_points / m_speedInPersents;
    }
}
