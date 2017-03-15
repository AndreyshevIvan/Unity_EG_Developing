using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldViewer : MonoBehaviour
{

    public GameObject[] m_tiles;

    public Color[] m_tilesColor;
    public Color m_darkColor;
    public Color m_lightColor;
    public ushort m_startLightColorNum;

    public void MoveButtons(List<IntPair> buttons)
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
                Color txtColor = GetTextColor(values[i, j]);
                int valueNum = i * rowsCount + j;

                Image bg = m_tiles[valueNum].GetComponentInChildren<Image>();
                bg.color = m_tilesColor[colorNum];

                Text txt = m_tiles[valueNum].GetComponentInChildren<Text>();
                txt.color = txtColor;
            }
        }
    }

    int GetColorNum(ushort value)
    {
        if (value < 2)
        {
            return 0;
        }

        int degree = 1;
        int num = 2;

        while (num != value)
        {
            num *= 2;
            degree++;
        }

        return degree;
    }
    Color GetTextColor(ushort value)
    {
        Color color = (value >= m_startLightColorNum) ? m_lightColor : m_darkColor;

        return color;
    }
}