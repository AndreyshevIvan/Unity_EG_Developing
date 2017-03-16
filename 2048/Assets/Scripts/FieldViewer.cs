using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FieldViewer : MonoBehaviour
{

    FieldController m_fieldController;

    public GameObject[] m_tiles;
    public Color[] m_tilesColor;

    public Color m_darkColor;
    public Color m_lightColor;
    public ushort m_startLightColorNum;

    float m_animColdown = 0.1f;
    float m_currAnimColdown = 0;

    private void Awake()
    {
        m_fieldController = GetComponent<FieldController>();
    }

    private void FixedUpdate()
    {
        if (!IsAnimationsEnded())
        {
            {
                // Animate turn
            }

            m_animColdown += Time.deltaTime;
        }
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
                else if (valueText.text != value.ToString())
                {
                    CreateSumAnimation(valueText, value, valueNum);
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
    void CreateSumAnimation(Text valueText, int value, int valueNum)
    {
        int currValue = 0;

        if (valueText.text != "")
        {
            currValue = int.Parse(valueText.text);
        }

        if (currValue * 2 == value)
        {
            m_tiles[valueNum].GetComponent<Animation>().Play();
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

    public bool IsAnimationsEnded()
    {
        return (m_currAnimColdown > m_animColdown);
    }
}