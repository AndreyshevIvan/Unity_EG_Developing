using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public FieldController m_fieldController;
    public FieldViewer m_fieldViewer;
    public User m_user;

    public GameObject m_gameoverPanel;
    public Button m_newGameButton;

    ushort[,] m_buttonsToMove;

    bool m_isPlayerMadeTurn = false;
    bool m_isGameover = false;

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        SetGameOver(false);
        m_fieldController.Start();
        m_user.Reset();
        UpdateView();
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
        if (m_fieldController.IsPlayerMadeTurn())
        {
            m_fieldController.SetTurn(true);
            m_isPlayerMadeTurn = false;
        }

        UpdateView();
        CheckGameStatus();
    }
    void GameoverUpdate()
    {

    }

    void UpdateView()
    {
        int addPoints = m_fieldController.GetPointsFromLastTurn();
        m_user.AddPoints(addPoints);

        ushort[,] values = m_fieldController.GetCurrentValues();
        m_fieldViewer.UpdateView(values);
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
        m_gameoverPanel.GetComponent<Animation>().Play();
        m_newGameButton.interactable = !m_isGameover;
    }
}
