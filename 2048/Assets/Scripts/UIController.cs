using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public Text m_points;
    public GameObject m_announcement;
    public GameObject m_announcementsParent;
    GameObject m_currAnnouncement;

    private void Awake()
    {

    }
 
    public void SetPoints(int points)
    {
        m_points.text = points.ToString();
    }

    public void CreateAnnouncement(int addPoints)
    {
        Destroy(m_currAnnouncement);

        Vector3 initPosition = m_announcementsParent.transform.position;
        m_currAnnouncement = Instantiate(m_announcement, initPosition, Quaternion.identity);
        m_currAnnouncement.GetComponentInChildren<PointsAnnouncement>().SetPoints(addPoints);
        m_currAnnouncement.transform.SetParent(m_announcementsParent.transform);
    }
}
