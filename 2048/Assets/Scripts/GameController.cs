using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public FieldController m_fieldController;
    public FieldViewer m_fieldViewer;
    public User m_user;
    public UIController m_UIController;
    public DataController m_data;

    int m_mapIndex;

    public GameObject m_gameoverPanel;
    public Button m_newGameButton;

    bool m_isGameover = false;

    private void Start()
    {
        m_mapIndex = m_data.GetMapIndex();

        string userName = m_data.GetUsername();
        m_user.SetName(userName);

        StartGame();
    }

    public void StartGame()
    {
        SetGameOver(false);
        m_fieldController.StartEvent();
        m_user.Reset();
        m_fieldViewer.UpdateView();

        uint bestScore = m_data.GetBestScore(m_mapIndex);
        m_UIController.SetBestScore(bestScore);
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
        if (m_fieldController.IsPlayerMadeTurn() && m_fieldViewer.IsAnimationsEnded())
        {
            m_fieldController.SetAutoTurn(true);
            bool[,] changeMask = m_fieldController.GetChangeMask();

            m_fieldViewer.UpdateView();
            m_fieldViewer.CreateSumAnimationFromMask(changeMask);
            m_user.UpdateInfo();
        }

        CheckGameStatus();
    }
    void GameoverUpdate()
    {

    }

    void CheckGameStatus()
    {
        if (!m_fieldController.IsTurnPossible())
        {
            SetGameOver(true);
        }
    }

    void SetGameOver(bool isGameover)
    {
        m_isGameover = isGameover;
        m_gameoverPanel.SetActive(m_isGameover);
        m_newGameButton.interactable = !m_isGameover;

        if (isGameover)
        {
            GameoverEvents();
        }
    }
    void GameoverEvents()
    {
        m_gameoverPanel.GetComponent<Animation>().Play();

        uint userPoints = m_user.GetPoints();
        string userName = m_user.GetName();

        m_data.SetBestScore(m_mapIndex, userPoints, userName);
    }
}
