using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldController : MonoBehaviour
{
    const ushort m_fieldSize = 4;
    ushort[,] m_fieldValues;

    private void Awake()
    {
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

    public IntPair[] GetMoveButtons()
    {
        int i = 3;
        IntPair[] buttonsAdreses = new IntPair[i];

        return buttonsAdreses;
    }

    public ushort[,] GetCurrentValues()
    {
        return m_fieldValues;
    }

    public void UpTurn()
    {
        for(int i = (m_fieldSize - 1); i > 0; i--)
        {
            for (int j = (m_fieldSize - 1); j > 0; j--)
            {
                if ()
            }
        }
    }
    public void DownTurn()
    {

    }
    public void LeftTurn()
    {

    }
    public void RightTurn()
    {

    }
    ushort[] ConvertLine(ushort[] line)
    {
        ushort[] newLine = new ushort[line.Length];

        return newLine;
    }

    void SetTurn(bool isFourEnable)
    {
        int value = 2;

        if (isFourEnable && Random.Range(0, 12) == 0)
        {
            value *= value;
        }

        List<int> freeTiles = new List<int>();

        for (int i = 0; i < m_fieldSize; i++)
        {
            for (int j = 0; j < m_fieldSize; j++)
            {
                if (m_fieldValues[i,j] == 0)
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
                if (i * m_fieldSize + j == randomTileNum)
                {
                    m_fieldValues[i, j] = (ushort)value;
                }
            }
        }
    }

    public bool IsTurnPossible()
    {
        IsAnyTileEmpty();

        return true;
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
