using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public MapLoader m_mapLoader;
    Field m_field;

    public User m_user;
    public UIController m_UIController;
    public GameAudio m_audio;
    DataController m_data;
    ScenesController m_scenesController;

    int m_mapIndex;

    public GameObject m_sceneCurtain;
    public GameObject m_gameoverPanel;

    bool m_isGameover = false;

    private void Awake()
    {
        m_data = new DataController();
        m_scenesController = new ScenesController();
        m_mapIndex = m_data.GetMapIndex();
        m_field = m_mapLoader.GetField(m_mapIndex);
        m_sceneCurtain.SetActive(false);
    }
    private void Start()
    {
        string userName = m_data.GetUsername();
        m_user.SetName(userName);

        StartGame();
    }
    public void StartGame()
    {
        SetGameOver(false, "");
        m_user.Reset();
        m_field.StartEvents();
        m_audio.StartBackgroundMusic();

        int mapBest = m_data.GetMapBestScore();
        m_UIController.SetBestScore(mapBest);
    }

    private void FixedUpdate()
    {
        if (!m_isGameover)
        {
            GameplayUpdate();
        }
        else
        {
            GameoverUpdate();
        }
    }
    void GameplayUpdate()
    {
        if (m_field.IsAutoTurnAllowed())
        {
            m_field.SetAutoTurn(1, true);

            int pointsToAdd = m_field.GetPointsFromLastTurn();
            m_user.AddPoints(pointsToAdd);
        }

        CheckGameStatus();
    }
    void GameoverUpdate()
    {

    }

    void CheckGameStatus()
    {
        if (!m_field.IsTurnPossible())
        {
            SetGameOver(true, "Game Over");
        }
    }

    public void FinishGame()
    {
        SetGameOver(true, "Game Finished");
    }

    void SetGameOver(bool isGameover, string title)
    {
        m_isGameover = isGameover;
        m_gameoverPanel.SetActive(m_isGameover);
        m_UIController.SetGameOver(isGameover, title);

        if (isGameover)
        {
            GameoverEvents();
        }
    }
    void GameoverEvents()
    {
        m_user.Save();
        m_audio.GameOver();
        m_audio.StopBackGroundMusic();

        Animation gameOverAnim = m_gameoverPanel.GetComponent<Animation>();
        if (gameOverAnim != null)
        {
            gameOverAnim.Play();
        }
    }

    public void BackTomenu()
    {
        StartCoroutine(m_scenesController.SetMenuScene());
        m_sceneCurtain.SetActive(true);
        m_sceneCurtain.GetComponent<Animation>().Play();
    }
}
