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
    public GameObject m_announceParent;
    GameObject m_lastAnnounce;

    public Button m_newGameButton;
    public Button m_finishButton;

    public User m_user;
    Vector2 m_parentSize;

    private void Awake()
    {
        RectTransform parentTransform = m_announceParent.GetComponent<RectTransform>();
        m_parentSize = parentTransform.rect.size;
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
        Destroy(m_lastAnnounce);

        Vector3 initPosition = m_announceParent.transform.position;
        m_lastAnnounce = Instantiate(m_announcement, initPosition, Quaternion.identity);
        m_lastAnnounce.GetComponentInChildren<PointsAnnouncement>().SetPoints(addPoints);
        m_lastAnnounce.transform.SetParent(m_announceParent.transform);

        SetAnnouncementSize();
    }
    void SetAnnouncementSize()
    {
        RectTransform transform = m_lastAnnounce.GetComponent<RectTransform>();
        transform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, m_parentSize.x);
        transform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, m_parentSize.y);
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
