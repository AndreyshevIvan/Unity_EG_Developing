using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{

    public FieldController m_fieldController;
    public UIController m_UIController;

    int m_points = 0;

    public void Reset()
    {
        m_points = 0;
        m_UIController.SetPoints(0);
    }

    private void FixedUpdate()
    {

    }

    public void AddPoints()
    {
        int points = m_fieldController.GetPointsFromLastTurn();

        if (points != 0)
        {
            m_points += points;
            m_UIController.SetPoints(m_points);
            m_UIController.CreateAnnouncement(points);
        }
    }
}