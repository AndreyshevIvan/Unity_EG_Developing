using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldViewer : MonoBehaviour
{

    public GameObject[] m_tiles;

    public Color[] m_tilesColor;

    public void MoveButtons(IntPair[] buttons)
    {

    }

    public void UpdateView(ushort[,] values)
    {
        int rowsCount = values.GetLength(0);
        int collsCount = values.GetLength(1);

        for (int i = 0; i < rowsCount; i++)
        {
            for (int j = 0; j < collsCount; j++)
            {
                int valueNum = i * rowsCount + j;
                int value = values[i, j];

                Text valueText = m_tiles[valueNum].GetComponentInChildren<Text>();

                if (value < 2)
                {
                    valueText.text = "";
                }
                else
                {
                    valueText.text = value.ToString();
                }
            }
        }

        UpdateTilesColor(values);
    }

    void UpdateTilesColor(ushort[,] values)
    {
        int rowsCount = values.GetLength(0);
        int collsCount = values.GetLength(1);

        for (int i = 0; i < rowsCount; i++)
        {
            for (int j = 0; j < collsCount; j++)
            {
                int colorNum = GetColorNum(values[i,j]);
                int valueNum = i * rowsCount + j;

                Image bg = m_tiles[valueNum].GetComponentInChildren<Image>();
                bg.color = m_tilesColor[colorNum];
            }
        }
    }

    int GetColorNum(ushort value)
    {
        int num = (int)Mathf.Sqrt(value);

        return num;
    }
}