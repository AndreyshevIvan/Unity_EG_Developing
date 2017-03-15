using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public FieldController m_fieldController;
    public FieldViewer m_fieldViewer;

    IntPair[] m_buttonsToMove;

    bool m_isPlayerMadeTurn = false;

    private void Awake()
    {

    }

    private void Start()
    {
        m_fieldController.Start();
        UpdateView();
    }

    private void FixedUpdate()
    {
        HandleTurn();

        if (m_isPlayerMadeTurn)
        {
            m_buttonsToMove = m_fieldController.GetMoveButtons();
            m_fieldViewer.MoveButtons(m_buttonsToMove);
            UpdateView();

            CheckGameStatus();

            m_isPlayerMadeTurn = false;
        }
    }

    void HandleTurn()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            m_fieldController.UpTurn();
            m_isPlayerMadeTurn = true;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            m_fieldController.RightTurn();
            m_isPlayerMadeTurn = true;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            m_fieldController.DownTurn();
            m_isPlayerMadeTurn = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            m_fieldController.LeftTurn();
            m_isPlayerMadeTurn = true;
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
}
