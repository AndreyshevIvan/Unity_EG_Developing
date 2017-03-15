using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public FieldController m_fieldController;
    public FieldViewer m_fieldViewer;
    public User m_user;
    public HandleTouch m_touchController;

    public GameObject m_gameoverPanel;
    public Button m_newGameButton;

    List<IntPair> m_buttonsToMove;

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
        HandleTurn();

        if (m_isPlayerMadeTurn)
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

    void HandleTurn()
    {
        switch(m_touchController.GetSwipeType())
        {
            case SwipeType.Up:
                UpTurn();
                break;
            case SwipeType.Down:
                DownTurn();
                break;
            case SwipeType.Left:
                LeftTurn();
                break;
            case SwipeType.Right:
                RightTurn();
                break;
        }
        if (m_isPlayerMadeTurn)
        {
            int addPoints = m_fieldController.GetPointsFromLastTurn();
            m_user.AddPoints(addPoints);
        }

        m_touchController.Reset();
    }

    void UpTurn()
    {
        m_isPlayerMadeTurn = m_fieldController.UpTurn();

        m_buttonsToMove = m_fieldController.GetMovedButtonsAdresesFromLastTurn();
        m_fieldViewer.MoveButtons(m_buttonsToMove);
    }
    void RightTurn()
    {
        m_isPlayerMadeTurn = m_fieldController.RightTurn();

        m_buttonsToMove = m_fieldController.GetMovedButtonsAdresesFromLastTurn();
        m_fieldViewer.MoveButtons(m_buttonsToMove);
    }
    void DownTurn()
    {
        m_isPlayerMadeTurn = m_fieldController.DownTurn();

        m_buttonsToMove = m_fieldController.GetMovedButtonsAdresesFromLastTurn();
        m_fieldViewer.MoveButtons(m_buttonsToMove);
    }
    void LeftTurn()
    {
        m_isPlayerMadeTurn = m_fieldController.LeftTurn();

        m_buttonsToMove = m_fieldController.GetMovedButtonsAdresesFromLastTurn();
        m_fieldViewer.MoveButtons(m_buttonsToMove);
    }

    void UpdateView()
    {
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
        m_newGameButton.interactable = !m_isGameover;
    }
}
