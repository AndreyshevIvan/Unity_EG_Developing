using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbstractPlayer : MonoBehaviour
{
    public Text m_UIpoints;
    public Text m_UIlife;
    public GameObject m_wall;

    float m_addingPointsSpeed = 0.01f;
    float m_addingTime = 0;
    int m_pointsPerOneAdd = 1;

    int m_points;
    int m_pointsToAdd;

    int m_life;

    public void Start()
    {
        m_life = 3;

    }
    public void ResetToNextLife()
    {

    }

    void FixedUpdate()
    {
        UpdateUI();
        UpdateEffects();
    }
    void UpdateUI()
    {
        AddPointsToUI();
        SetLifeToUI();
        UpdateWallInUI();
    }
    void UpdateEffects()
    {

    }

    public void SetWallActive(bool isActive)
    {
        m_wall.SetActive(isActive);
    }
    void UpdateWallInUI()
    {

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
    void SetLifeToUI()
    {
        m_UIlife.text = "Life: " + m_life.ToString();
    }

    public void AddPoints(int points)
    {
        m_points += points;
        m_pointsToAdd += points;
    }
    void AddPointsToUI()
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
    public void ReducePoints(int reducePoints)
    {
        m_points -= reducePoints;
    }
}
