using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbstractPlayer : MonoBehaviour
{
    public Text m_UIpoints;

    public float m_addingPointsSpeed = 0.01f;
    int m_pointsPerOneAdd = 1;
    float m_addingTime = 0;

    int m_points;
    int m_pointsToAdd;

    private void Awake()
    {

    }

    private void FixedUpdate()
    {
        AddingPoints();
    }

    public void AddPoints(int points)
    {
        m_points += points;
        m_pointsToAdd += points;
    }

    void AddingPoints()
    {
        m_addingTime += Time.deltaTime;

        if (m_addingTime >= m_addingPointsSpeed && m_pointsToAdd != 0)
        {
            if (m_pointsToAdd >= m_pointsPerOneAdd)
            {
                AddPointsToString(m_pointsPerOneAdd);
                m_pointsToAdd -= m_pointsPerOneAdd;
            }
            else
            {
                AddPointsToString(m_pointsToAdd);
                m_pointsToAdd = 0;
            }

            m_addingTime = 0;
        }
    }

    void AddPointsToString(int points)
    {
        int currPoints = int.Parse(m_UIpoints.text);
        int newPoints = currPoints + points;

        m_UIpoints.text = newPoints.ToString();
    }
}
