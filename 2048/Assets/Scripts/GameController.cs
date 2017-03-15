using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public FieldController m_fieldController;
    public FieldViewer m_fieldViewer;

    public GameObject m_gameoverPanel;
    public Button m_newGameButton;

    List<IntPair> m_buttonsToMove;

    bool m_isPlayerMadeTurn = false;
    bool m_isGameover = false;

    private void Awake()
    {

    }

    private void Start()
    {
        SetGameOver(m_isGameover);
        m_fieldController.Start();
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
            m_buttonsToMove = m_fieldController.GetMoveButtons();
            m_fieldViewer.MoveButtons(m_buttonsToMove);

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
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            m_isPlayerMadeTurn = m_fieldController.UpTurn();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            m_isPlayerMadeTurn = m_fieldController.RightTurn();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            m_isPlayerMadeTurn = m_fieldController.DownTurn();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            m_isPlayerMadeTurn = m_fieldController.LeftTurn();
        }
    }
    void UpdateView()
    {
        ushort[,] values = m_fieldController.GetCurrentValues();

        m_fieldViewer.UpdateView(values);
    }

    void CheckGameStatus()
    {

    }

    void SetGameOver(bool isGameover)
    {
        m_isGameover = isGameover;
        m_gameoverPanel.SetActive(m_isGameover);
        m_newGameButton.interactable = m_isGameover;
    }
}
