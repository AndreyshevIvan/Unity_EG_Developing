using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FieldController : MonoBehaviour
{
    const ushort m_fieldSize = 4;
    ushort[,] m_fieldValues;
    List<IntPair> m_movedButtons;
    int m_points = 0;

    public float m_fourProbability;

    private void Awake()
    {
        m_movedButtons = new List<IntPair>();
        m_fieldValues = new ushort[m_fieldSize, m_fieldSize];
    }
    public void Start()
    {
        ResetField();
        SetTurn(true);
        SetTurn(false);
    }
    void ResetField()
    {
        for (int i = 0; i < m_fieldSize; i++)
        {
            for (int j = 0; j < m_fieldSize; j++)
            {
                m_fieldValues[i, j] = 0;
            }
        }
    }
    void ResetMovedButtons()
    {
        m_movedButtons.Clear();
    }
    void ResetPoints()
    {
        m_points = 0;
    }

    public List<IntPair> GetMovedButtonsAdresesFromLastTurn()
    {
        return m_movedButtons;
    }
    public int GetPointsFromLastTurn()
    {
        int points = m_points;
        ResetPoints();

        return points;
    }

    public ushort[,] GetCurrentValues()
    {
        return m_fieldValues;
    }

    public void SetTurn(bool isFourEnable)
    {
        int value = 2;

        if (isFourEnable)
        {
            float random = Random.Range(0, 1.0f);

            if (random < m_fourProbability)
            {
                value = value * 2;
            }
        }

        List<int> freeTiles = new List<int>();

        for (int i = 0; i < m_fieldSize; i++)
        {
            for (int j = 0; j < m_fieldSize; j++)
            {
                if (m_fieldValues[i, j] == 0)
                {
                    freeTiles.Add(i * m_fieldSize + j);
                }
            }
        }

        int randomTileNum = Random.Range(0, freeTiles.Count);

        for (int i = 0; i < m_fieldSize; i++)
        {
            for (int j = 0; j < m_fieldSize; j++)
            {
                if (m_fieldValues[i, j] == 0)
                {
                    if (randomTileNum == 0)
                    {
                        m_fieldValues[i, j] = (ushort)value;
                    }

                    randomTileNum--;
                }
            }
        }
    }

    public bool UpTurn()
    {
        ResetMovedButtons();
        bool isMoveWasDone = false;
        ushort[] line = new ushort[m_fieldSize];

        for (int i = 0; i < m_fieldSize; i++)
        {
            List<ushort> movedAdreses = new List<ushort>();

            for (int j = 0; j < m_fieldSize; j++)
            {
                line[j] = m_fieldValues[j, i];
            }

            if (MoveLine(ref line, ref movedAdreses) && !isMoveWasDone)
            {
                isMoveWasDone = true;
            }

            for (int j = 0; j < m_fieldSize; j++)
            {
                m_fieldValues[j, i] = line[j];
            }

            for (int k = 0; k < movedAdreses.Count; k++)
            {
                m_movedButtons.Add(new IntPair(movedAdreses[k], i));
            }
        }

        return isMoveWasDone;
    }
    public bool DownTurn()
    {
        ResetMovedButtons();
        bool isMoveWasDone = false;
        ushort[] line = new ushort[m_fieldSize];

        for (int i = 0; i < m_fieldSize; i++)
        {
            List<ushort> movedAdreses = new List<ushort>();

            for (int j = m_fieldSize; j > 0; j--)
            {
                line[m_fieldSize - j] = m_fieldValues[j - 1, i];
            }

            if (MoveLine(ref line, ref movedAdreses) && !isMoveWasDone)
            {
                isMoveWasDone = true;
            }

            for (int j = m_fieldSize; j > 0; j--)
            {
                m_fieldValues[j - 1, i] = line[m_fieldSize - j];
            }

            for (int k = 0; k < movedAdreses.Count; k++)
            {
                m_movedButtons.Add(new IntPair(movedAdreses[k], i));
            }
        }

        return isMoveWasDone;
    }
    public bool LeftTurn()
    {
        ResetMovedButtons();
        bool isMoveWasDone = false;
        ushort[] line = new ushort[m_fieldSize];

        for (int i = 0; i < m_fieldSize; i++)
        {
            List<ushort> movedAdreses = new List<ushort>();

            for (int j = 0; j < m_fieldSize; j++)
            {
                line[j] = m_fieldValues[i, j];
            }

            if (MoveLine(ref line, ref movedAdreses) && !isMoveWasDone)
            {
                isMoveWasDone = true;
            }

            for (int j = 0; j < m_fieldSize; j++)
            {
                m_fieldValues[i, j] = line[j];
            }

            for (int k = 0; k < movedAdreses.Count; k++)
            {
                m_movedButtons.Add(new IntPair(i, movedAdreses[k]));
            }
        }

        return isMoveWasDone;
    }
    public bool RightTurn()
    {
        ResetMovedButtons();
        bool isMoveWasDone = false;
        ushort[] line = new ushort[m_fieldSize];

        for (int i = 0; i < m_fieldSize; i++)
        {
            List<ushort> movedAdreses = new List<ushort>();

            for (int j = m_fieldSize; j > 0; j--)
            {
                line[m_fieldSize - j] = m_fieldValues[i, j - 1];
            }

            if (MoveLine(ref line, ref movedAdreses) && !isMoveWasDone)
            {
                isMoveWasDone = true;
            }

            for (int j = m_fieldSize; j > 0; j--)
            {
                m_fieldValues[i, j - 1] = line[m_fieldSize - j];
            }

            for (int k = 0; k < movedAdreses.Count; k++)
            {
                m_movedButtons.Add(new IntPair(i, movedAdreses[k]));
            }
        }

        return isMoveWasDone;
    }
    bool MoveLine(ref ushort[] line, ref List<ushort> movedAdreses)
    {
        bool isLineMoved = false;

        for (int i = 1; i < line.Length; i++)
        {
            ushort value = line[i];
            int position = i;

            while (position > 0)
            {
                if (line[position - 1] != value && line[position - 1] != 0)
                {
                    break;
                }
                else if (line[position - 1] == value)
                {
                    line[position - 1] += value;
                    m_points += line[position - 1];
                    line[position] = 0;
                    isLineMoved = true;
                }
                else if (line[position - 1] == 0)
                {
                    line[position - 1] = value;
                    line[position] = 0;
                    isLineMoved = true;
                }

                position--;
            }
        }

        return isLineMoved;
    }

    public bool IsTurnPossible()
    {
        if (IsAnyTileEmpty())
        {
            return true;
        }

        bool isAnyPairValid = false;

        for (int i = 0; i < m_fieldSize; i++)
        {
            for (int j = 1; j < m_fieldSize - 1; j++)
            {
                if (m_fieldValues[i,j - 1] == m_fieldValues[i, j] ||
                    m_fieldValues[i, j] == m_fieldValues[i, j + 1] ||
                    m_fieldValues[j - 1, i] == m_fieldValues[j, i] ||
                    m_fieldValues[j, i] == m_fieldValues[j + 1, i])
                {
                    isAnyPairValid = true;
                }
            }
        }

        return isAnyPairValid;
    }
    bool IsAnyTileEmpty()
    {
        bool isAnyEmpty = false;

        for (int i = 0; i < m_fieldSize; i++)
        {
            for (int j = 0; j < m_fieldSize; j++)
            {
                if (m_fieldValues[i,j] == 0)
                {
                    isAnyEmpty = true;
                }
            }
        }

        return isAnyEmpty;
    }
}
