using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{
    public UIController m_UIController;
    public DataController m_data;

    uint m_points = 0;
    string m_name = "@username";

    public void Reset()
    {
        int m_mapIndex = m_data.GetMapIndex();
        m_data.SetBestScore(m_mapIndex, m_points, m_name);

        m_points = 0;
        m_UIController.SetPoints(0);
    }

    public void AddPoints(uint points)
    {
        if (points != 0)
        {
            m_points += points;
            m_UIController.SetPoints(m_points);
            m_UIController.CreateAnnouncement(points);
        }
    }

    public uint GetPoints()
    {
        return m_points;
    }
    public string GetName()
    {
        return m_name;
    }

    public void SetName(string name)
    {
        m_name = name;
    }

    private void OnDestroy()
    {
        int m_mapIndex = m_data.GetMapIndex();
        m_data.SetBestScore(m_mapIndex, m_points, m_name);
    }
}