using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


abstract class Bot
{
    private float m_time = 0;
    private float m_delay = 1;
    private bool m_isActive = false;
    protected string m_side = "";
    protected string m_searchSide;
    public string GetSide()
    {
        return m_side;
    }
    public bool IsReady()
    {
        return m_time >= m_delay;
    }
    public void Start()
    {
        m_isActive = true;
    }
    public void Stop()
    {
        m_isActive = false;
    }
    public void Restart()
    {
        Start();
        m_time = 0;
    }
    public void Update()
    {
        if (m_isActive && m_time <= m_delay)
        {
            m_time += Time.deltaTime;
        }
    }
    public virtual void Turn(Text[] buttons)
    {
        if (IsEmptyFieldExist(buttons))
        {
            GridSpace button = GetTurn(buttons);

            if (button != null)
            {
                button.SetSpace();

                Restart();
                SetDelay();
            }
        }
    }
    public void InvertSide()
    {
        m_side = (m_side == "X") ? "O" : "X";
        m_searchSide = (m_searchSide == "X") ? "O" : "X";
    }

    public abstract GridSpace GetTurn(Text[] buttons);

    private bool IsEmptyFieldExist(Text[] buttons)
    {
        if (IsButtonValid(buttons[0]) ||
            IsButtonValid(buttons[1]) ||
            IsButtonValid(buttons[2]) ||
            IsButtonValid(buttons[3]) ||
            IsButtonValid(buttons[4]) ||
            IsButtonValid(buttons[5]) ||
            IsButtonValid(buttons[6]) ||
            IsButtonValid(buttons[7]) ||
            IsButtonValid(buttons[8]))
        {
            return true;
        }

        return false;
    }
    protected bool IsButtonValid(Text button)
    {
        return (button != null && button.text == "");
    }
    private void SetDelay()
    {
        m_delay = Random.Range(0.4f, 1.0f);
    }
    protected GridSpace GetRandomTurn(Text[] buttons)
    {
        int turn = 0;
        bool isTurnValid = false;

        while (!isTurnValid)
        {
            turn = Random.Range(0, 9);
            if (IsButtonValid(buttons[turn]))
            {
                isTurnValid = true;
            }
        }

        GridSpace button = buttons[turn].GetComponentInParent<GridSpace>();
        return button;
    }
    protected GridSpace GetBestOfThree(Text[] buttons)
    {
        // 0 1 2
        // 3 4 5
        // 6 7 8

        GridSpace button = null;

        if (button == null) button = GetOneOfThree(buttons, 0, 1, 2);
        if (button == null) button = GetOneOfThree(buttons, 3, 4, 5);
        if (button == null) button = GetOneOfThree(buttons, 6, 7, 8);
        if (button == null) button = GetOneOfThree(buttons, 0, 3, 6);
        if (button == null) button = GetOneOfThree(buttons, 1, 4, 7);
        if (button == null) button = GetOneOfThree(buttons, 2, 5, 8);
        if (button == null) button = GetOneOfThree(buttons, 0, 4, 8);
        if (button == null) button = GetOneOfThree(buttons, 6, 4, 2);

        if (button == null) button = GetRandomTurn(buttons);

        return button;
    }
    protected GridSpace GetOneOfThree(Text[] buttons, int firstNum, int secondNum, int thirdNum)
    {
        bool isSearchSuccess = false;
        int turn = 0;

        Text firstButton = buttons[firstNum];
        Text secondButton = buttons[secondNum];
        Text thirdButton = buttons[thirdNum];

        if (firstButton.text == "" &&
            secondButton.text == m_searchSide &&
            thirdButton.text == m_searchSide)
        {
            isSearchSuccess = true;
            turn = firstNum;
        }
        if (firstButton.text == m_searchSide &&
            secondButton.text == "" &&
            thirdButton.text == m_searchSide)
        {
            isSearchSuccess = true;
            turn = secondNum;
        }
        if (firstButton.text == m_searchSide &&
            secondButton.text == m_searchSide &&
            thirdButton.text == "")
        {
            isSearchSuccess = true;
            turn = thirdNum;
        }

        if (isSearchSuccess)
        {
            return buttons[turn].GetComponentInParent<GridSpace>();
        }

        return null;
    }
}

class RandomBot : Bot
{
    RandomBot(string side)
    {
        m_side = side;
    }

    public override GridSpace GetTurn(Text[] buttons)
    {
        GridSpace button = GetRandomTurn(buttons);

        return button;
    }
}

class DefenseBot : Bot
{
    public DefenseBot(string side)
    {
        m_side = side;
        m_searchSide = (m_side == "X") ? "O" : "X";
    }

    public override GridSpace GetTurn(Text[] buttons)
    {
        GridSpace button = GetBestOfThree(buttons);

        return button;
    }
}

class AttackBot : Bot
{
    public AttackBot(string side)
    {
        m_side = side;
        m_searchSide = m_side;
    }

    public override GridSpace GetTurn(Text[] buttons)
    {
        GridSpace button = GetBestOfThree(buttons);

        return button;
    }
}

class MixedBot : Bot
{
    public MixedBot(string side)
    {
        m_side = side;
        SetRandomDefenseOrAttac();
    }

    public override GridSpace GetTurn(Text[] buttons)
    {
        SetRandomDefenseOrAttac();
        GridSpace button = GetBestOfThree(buttons);

        return button;
    }

    private void SetRandomDefenseOrAttac()
    {
        int randomValue = Random.Range(0, 2);

        if (randomValue == 0)
        {
            m_searchSide = m_side;
        }
        else
        {
            m_searchSide = (m_side == "X") ? "O" : "X";
        }
    }
}

class FullMixedBot : Bot
{
    public FullMixedBot(string side)
    {
        m_side = side;
        SetRandomType();
    }
    enum BotType
    {
        DEFENSE = 0,
        ATTACK,
        RANDOM,
    }

    private int m_type;

    public override GridSpace GetTurn(Text[] buttons)
    {
        GridSpace button;

        SetRandomType();
        button = (m_type == (int)BotType.RANDOM) ? GetRandomTurn(buttons) : GetBestOfThree(buttons);

        return button;
    }
    private void SetRandomType()
    {
        int m_type = Random.Range(0, 3);

        if (m_type == (int)BotType.DEFENSE)
        {
            Debug.Log((int)BotType.DEFENSE);
            m_searchSide = m_side;
        }
        else if (m_type == (int)BotType.ATTACK)
        {
            Debug.Log((int)BotType.ATTACK);
            m_searchSide = (m_side == "X") ? "O" : "X";
        }
        else
        {
            Debug.Log((int)BotType.RANDOM);
        }
    }
}
