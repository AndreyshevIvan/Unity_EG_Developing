using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldController : MonoBehaviour
{
    byte m_fieldSize = 4;
    const byte m_power = 2;
    byte[,] m_fieldValues;
    byte[,] m_fieldCopy;
    byte[,] m_moveMap;
    bool[,] m_sumMap;
    bool[,] m_changeMask;

    uint m_points = 0;

    public float m_fourProbability;

    bool m_isPlayerMadeTurn = false;

    public void Init(byte size)
    {
        m_fieldSize = size;

        m_moveMap = new byte[m_fieldSize, m_fieldSize];
        m_fieldValues = new byte[m_fieldSize, m_fieldSize];
        m_fieldCopy = new byte[m_fieldSize, m_fieldSize];
        m_changeMask = new bool[m_fieldSize, m_fieldSize];
        m_sumMap = new bool[m_fieldSize, m_fieldSize];
    }
    public void StartEvents()
    {
        ResetField();
        SetAutoTurn(2, true);
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
    void ResetPoints()
    {
        m_points = 0;
    }
    void ResetMaps()
    {
        for (int i = 0; i < m_fieldSize; i++)
        {
            for (int j = 0; j < m_fieldSize; j++)
            {
                m_moveMap[i, j] = 0;
                m_sumMap[i, j] = false;
            }
        }
    }

    public byte[,] GetCurrentAnimMap()
    {
        return m_moveMap;
    }
    public uint GetPointsFromLastTurn()
    {
        uint points = m_points;
        ResetPoints();

        return points;
    }
    public int GetFieldSize()
    {
        return m_fieldSize;
    }
    public byte[,] GetCurrentValues()
    {
        return m_fieldValues;
    }
    byte GetRandomValue(bool isFourEnable)
    {
        byte value = 1;

        if (isFourEnable)
        {
            float random = Random.Range(0, 1.0f);

            if (random < m_fourProbability)
            {
                value = (byte)(value << 1);
            }
        }

        return value;
    }
    List<int> GetFreeTiles()
    {
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

        return freeTiles;
    }
    public bool[,] GetChangeMask()
    {
        return m_changeMask;
    }
    public bool[,] GetSumMap()
    {
        return m_sumMap;
    }

    public void SetAutoTurn(ushort turnsCount, bool isFourEnable)
    {
        CreateFieldCopy();

        for (int turnNum = 0; turnNum < turnsCount; turnNum++)
        {
            m_isPlayerMadeTurn = false;
            int value = GetRandomValue(isFourEnable);
            List<int> freeTiles = GetFreeTiles();

            int randomTileNum = Random.Range(0, freeTiles.Count);

            for (int i = 0; i < m_fieldSize; i++)
            {
                for (int j = 0; j < m_fieldSize; j++)
                {
                    if (m_fieldValues[i, j] == 0)
                    {
                        if (randomTileNum == 0)
                        {
                            m_fieldValues[i, j] = (byte)value;
                        }

                        randomTileNum--;
                    }
                }
            }
        }

        IsFieldChanged();
    }

    public bool UpTurn()
    {
        ResetMaps();
        CreateFieldCopy();
        byte[] line = new byte[m_fieldSize];

        for (int i = 0; i < m_fieldSize; i++)
        {
            byte[] moveCount = new byte[m_fieldSize];
            bool[] lineSumMap = new bool[m_fieldSize];

            for (int j = 0; j < m_fieldSize; j++)
            {
                lineSumMap[j] = false;
            }

            for (int j = 0; j < m_fieldSize; j++)
            {
                line[j] = m_fieldValues[j, i];
            }

            MoveLine(ref line, ref moveCount, ref lineSumMap);

            for (int j = 0; j < m_fieldSize; j++)
            {
                m_fieldValues[j, i] = line[j];
                m_moveMap[j, i] = moveCount[j];
                m_sumMap[j, i] = lineSumMap[j];
            }
        }
        m_isPlayerMadeTurn = IsFieldChanged();

        return m_isPlayerMadeTurn;
    }
    public bool DownTurn()
    {
        ResetMaps();
        CreateFieldCopy();
        byte[] line = new byte[m_fieldSize];

        for (int i = 0; i < m_fieldSize; i++)
        {
            byte[] moveCount = new byte[m_fieldSize];
            bool[] lineSumMap = new bool[m_fieldSize];

            for (int j = 0; j < m_fieldSize; j++)
            {
                lineSumMap[j] = false;
            }

            for (int j = m_fieldSize; j > 0; j--)
            {
                line[m_fieldSize - j] = m_fieldValues[j - 1, i];
            }

            MoveLine(ref line, ref moveCount, ref lineSumMap);

            for (int j = m_fieldSize; j > 0; j--)
            {
                m_fieldValues[j - 1, i] = line[m_fieldSize - j];
                m_moveMap[j - 1, i] = moveCount[m_fieldSize - j];
                m_sumMap[j - 1, i] = lineSumMap[m_fieldSize - j];
            }
        }
        m_isPlayerMadeTurn = IsFieldChanged();

        return m_isPlayerMadeTurn;
    }
    public bool LeftTurn()
    {
        ResetMaps();
        CreateFieldCopy();
        byte[] line = new byte[m_fieldSize];
        bool[] lineSumMap = new bool[m_fieldSize];

        for (int i = 0; i < m_fieldSize; i++)
        {
            byte[] moveCount = new byte[m_fieldSize];

            for (int j = 0; j < m_fieldSize; j++)
            {
                lineSumMap[j] = false;
            }

            for (int j = 0; j < m_fieldSize; j++)
            {
                line[j] = m_fieldValues[i, j];
            }

            MoveLine(ref line, ref moveCount, ref lineSumMap);

            for (int j = 0; j < m_fieldSize; j++)
            {
                m_fieldValues[i, j] = line[j];
                m_moveMap[i, j] = moveCount[j];
                m_sumMap[i, j] = lineSumMap[j];
            }
        }
        m_isPlayerMadeTurn = IsFieldChanged();

        return m_isPlayerMadeTurn;
    }
    public bool RightTurn()
    {
        ResetMaps();
        CreateFieldCopy();
        byte[] line = new byte[m_fieldSize];
        bool[] lineSumMap = new bool[m_fieldSize];

        for (int i = 0; i < m_fieldSize; i++)
        {
            byte[] moveCount = new byte[m_fieldSize];

            for (int j = 0; j < m_fieldSize; j++)
            {
                lineSumMap[j] = false;
            }

            for (int j = m_fieldSize; j > 0; j--)
            {
                line[m_fieldSize - j] = m_fieldValues[i, j - 1];
            }

            MoveLine(ref line, ref moveCount, ref lineSumMap);

            for (int j = m_fieldSize; j > 0; j--)
            {
                m_fieldValues[i, j - 1] = line[m_fieldSize - j];
                m_moveMap[i, j - 1] = moveCount[m_fieldSize - j];
                m_sumMap[i, j - 1] = lineSumMap[m_fieldSize - j];
            }
        }
        m_isPlayerMadeTurn = IsFieldChanged();

        return m_isPlayerMadeTurn;
    }
    void MoveLine(ref byte[] line, ref byte[] moveCount, ref bool[] lineSumMap)
    {
        for (int i = 1; i < line.Length; i++)
        {
            byte value = line[i];
            int position = i;

            while (position > 0)
            {
                if (line[position - 1] != value && line[position - 1] != 0)
                {
                    break;
                }
                else if (line[position - 1] == value && value != 0)
                {
                    line[position - 1] += 1;
                    lineSumMap[position - 1] = true;
                    AddPoints(line[position - 1]);
                    line[position] = 0;
                    moveCount[i]++;
                }
                else if (line[position - 1] == 0)
                {
                    line[position - 1] = value;
                    line[position] = 0;
                    moveCount[i]++;
                }

                position--;
            }
        }
    }
    void CreateFieldCopy()
    {
        for (int i = 0; i < m_fieldSize; i++)
        {
            for (int j = 0; j < m_fieldSize; j++)
            {
                m_fieldCopy[i, j] = m_fieldValues[i, j];
            }
        }
    }
    void AddPoints(byte value)
    {
        uint addPoints = 1;

        while (value > 0)
        {
            addPoints *= m_power;
            value--;
        }

        m_points += addPoints;
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
                    i = m_fieldSize;
                    j = m_fieldSize;
                }
            }
        }

        return isAnyEmpty;
    }
    public bool IsPlayerMadeTurn()
    {
        return m_isPlayerMadeTurn;
    }
    bool IsFieldChanged()
    {
        bool isFeldChange = false;

        for (int i = 0; i < m_fieldSize; i++)
        {
            for (int j = 0; j < m_fieldSize; j++)
            {
                bool isTileChange = (m_fieldCopy[i, j] != m_fieldValues[i, j]);
                m_changeMask[i, j] = isTileChange;

                if (isTileChange)
                {
                    isFeldChange = true;
                }
            }
        }

        return isFeldChange;
    }
}