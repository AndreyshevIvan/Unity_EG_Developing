using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUIController : MonoBehaviour
{
    public InfoController m_info;

    public Text m_pointsField;
    public Text m_lifeField;
    public Text m_wallField;
    public Text m_ballsField;
    public Text m_multiplitterField;

    float m_addingPointsSpeed = 0.01f;
    float m_addingTime = 0;
    int m_pointsPerOneAdd = 10;

    private void Awake()
    {
        m_pointsField.text = "0";
        m_lifeField.text = "0";
        m_wallField.text = "0";
        m_ballsField.text = "0";
        m_multiplitterField.text = "0";
    }
    public void UpdateBalls(int ballsCount)
    {
        m_ballsField.text = "Balls: " + ballsCount.ToString();
    }
    public void UpdateWall(float duration, float maxDuration)
    {
        int seconds = (int)maxDuration - (int)duration;

        if (duration != 0)
        {
            m_wallField.text = "Wall time: " + seconds.ToString();
        }
        else
        {
            m_wallField.text = "Wall is off";
        }
    }
    public void UpdateLife(int lifeCount)
    {
        m_lifeField.text = "Life: " + lifeCount.ToString();
    }
    public void UpdatePoints(int points)
    {
        int currPoints = int.Parse(m_pointsField.text);
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
    public void UpdateMultiplier(int multiplitter)
    {
        m_multiplitterField.text = "Multiplier: x" + multiplitter.ToString();
    }
    void AddPointsToText(int points)
    {
        int currPoints = int.Parse(m_pointsField.text);
        int newPoints = currPoints + points;

        m_pointsField.text = newPoints.ToString();
    }
}
