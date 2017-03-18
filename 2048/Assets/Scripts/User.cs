using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{
    public UIController m_UIController;
    DataController m_data;

    int m_points = 0;
    string m_name = "@username";

    private void Awake()
    {
        m_data = new DataController();
    }

    public void Reset()
    {
        m_points = 0;
        m_UIController.SetPoints(0);
    }
    public void Save()
    {
        int mapIndex = m_data.GetMapIndex();
        m_data.SetBestScore(mapIndex, m_points, m_name);
    }

    public void AddPoints(int points)
    {
        if (points != 0)
        {
            m_points += points;
            m_UIController.SetPoints(m_points);
            m_UIController.CreateAnnouncement(points);
        }
    }

    public int GetPoints()
    {
        return m_points;
    }
    public string GetName()
    {
        return m_name;
    }

    public void SetName(string name)
    {
        if (name != "")
        {
            m_name = name;
        }
    }

    private void OnDestroy()
    {

    }
}