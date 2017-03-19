using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public Text m_points;
    public Text m_bestScore;
    public Text m_gameoverScore;
    public Text m_gameOverTitle;

    public GameObject m_announcement;
    public GameObject m_announcementsParent;
    GameObject m_currAnnouncement;

    public Button m_newGameButton;
    public Button m_finishButton;

    public User m_user;

    private void Awake()
    {

    }
 
    public void SetPoints(int points)
    {
        m_points.text = points.ToString();
    }
    public void SetBestScore(int points)
    {
        m_bestScore.text = points.ToString();
    }

    public void CreateAnnouncement(int addPoints)
    {
        Destroy(m_currAnnouncement);

        Vector3 initPosition = m_announcementsParent.transform.position;
        m_currAnnouncement = Instantiate(m_announcement, initPosition, Quaternion.identity);
        m_currAnnouncement.GetComponentInChildren<PointsAnnouncement>().SetPoints(addPoints);
        m_currAnnouncement.transform.SetParent(m_announcementsParent.transform);
    }

    public void SetGameOver(bool isGameOver, string title = "")
    {
        m_newGameButton.interactable = !isGameOver;
        m_finishButton.interactable = !isGameOver;

        if (isGameOver)
        {
            GameOverEvents(title);
        }
    }

    void GameOverEvents(string title = "")
    {
        m_gameOverTitle.text = title;

        int score = m_user.GetPoints();
        m_gameoverScore.text = "Score: " + score.ToString();
    }
}
