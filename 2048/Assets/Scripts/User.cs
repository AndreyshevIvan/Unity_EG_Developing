using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{

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

    public void AddPoints(int addPoints)
    {
        if (addPoints != 0)
        {
            m_points += addPoints;
            m_UIController.SetPoints(m_points);
            m_UIController.CreateAnnouncement(addPoints);
        }
    }
}