using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FieldController : MonoBehaviour
{
    const ushort m_fieldSize = 4;
    ushort[,] m_fieldValues;

    StreamWriter m_log;


    private void Awake()
    {
        m_log = new StreamWriter("Assets/Debug/log.txt");
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
    }
    public void DownTurn()
    {

    }
    public void LeftTurn()
    {
        ushort[] line = new ushort[m_fieldSize];

        for (int i = 0; i < m_fieldSize; i++)
        {
            for (int j = 0; j < m_fieldSize; j++)
            {
                line[j] = m_fieldValues[i, j];
            }

            line = ConvertLine(line);

            for (int j = 0; j < m_fieldSize; j++)
            {
                m_fieldValues[i, j] = line[j];
            }
        }
    }
    public void RightTurn()
    {

    }
    ushort[] ConvertLine(ushort[] line)
    {
        for (int i = 1; i < line.Length; i++)
        {
            ushort value = line[i];
            int position = i;
            bool isPrevMore = false;

            while (position > 0 && !isPrevMore)
            {
                if (line[position - 1] != value && line[position - 1] != 0)
                {
                    isPrevMore = true;
                }
                else if (line[position - 1] == value)
                {
                    line[position - 1] += value;
                    line[position] = 0;
                }
                else if (line[position - 1] == 0)
                {
                    line[position - 1] = value;
                    line[position] = 0;
                }

                position--;
            }
        }

        return line;
    }

    public void SetTurn(bool isFourEnable)
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
        Debug.Log("COUNT " + freeTiles.Count);

        int randomTileNum = Random.Range(0, freeTiles.Count);

        Debug.Log("RANDOM " + randomTileNum);

        for (int i = 0; i < m_fieldSize; i++)
        {
            for (int j = 0; j < m_fieldSize; j++)
            {
                if (m_fieldValues[i,j] == 0)
                {
                    if (randomTileNum == 0)
                    {
                        m_fieldValues[i, j] = (ushort)value;
                        Debug.Log("randomTileNum = " + randomTileNum);
                        Debug.Log("Random adress [" + i + ", " + j + "]");

                    }

                    randomTileNum--;
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

    void WriteArrayToLog(ushort[] line)
    {
        string str = "";

        for (int i = 0; i < line.Length; i++)
        {
            str += line[i].ToString();

            if (i != line.Length - 1)
            {
                str += ", ";
            }
        }

        m_log.WriteLine(str);
    }

    private void OnDestroy()
    {
        m_log.Close();
    }
}
