using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FieldViewer : MonoBehaviour
{
    GameObject[] m_tiles;
    public Color[] m_tilesColor;
    byte m_fieldSize;

    readonly Color m_instantTileColor = Color.clear;
    readonly Color m_instantValueColor = Color.black;
    public Color m_darkColor;
    public Color m_lightColor;
    const byte m_startLightColorNum = 3;
    const byte m_power = 2;

    Vector2[,] m_startTilesPositions;
    const float m_animColdown = 0.1f;
    const float m_moveFactor = 0.95f;
    float m_currAnimColdown = 0;
    Vector2 m_animDirection;
    byte[,] m_animnMap;
    float m_oneMoveOffset = 0;

    public void Init(ref GameObject[] tiles, byte fieldSize)
    {
        m_tiles = tiles;

        m_fieldSize = fieldSize;
        m_currAnimColdown = m_animColdown;
        m_oneMoveOffset = tiles[0].GetComponent<RectTransform>().rect.size.x;

        SaveStartTilesPositions();
    }

    private void FixedUpdate()
    {
        if (IsMoveAnimationWork())
        {
            MoveTiles();

            m_currAnimColdown += Time.deltaTime;
        }
    }

    void MoveTiles()
    {
        for (int i = 0; i < m_fieldSize; i++)
        {
            for (int j = 0; j < m_fieldSize; j++)
            {
                if (m_animnMap[i, j] != 0)
                {
                    int tileNum = i * m_fieldSize + j;
                    int offsetCount = m_animnMap[i, j];
                    float time = Time.deltaTime / m_animColdown;
                    Vector2 distance = m_animDirection * m_oneMoveOffset * offsetCount;

                    m_tiles[tileNum].transform.Translate(distance * time * m_moveFactor);
                }
            }
        }
    }
    public void AnimateRightTurn(byte[,] animMap)
    {
        m_animDirection = Vector2.right;
        m_animnMap = animMap;
        m_currAnimColdown = 0;
    }
    public void AnimateLeftTurn(byte[,] animMap)
    {
        m_animDirection = Vector2.left;
        m_animnMap = animMap;
        m_currAnimColdown = 0;
    }
    public void AnimateUpTurn(byte[,] animMap)
    {
        m_animDirection = Vector2.up;
        m_animnMap = animMap;
        m_currAnimColdown = 0;
    }
    public void AnimateDownTurn(byte[,] animMap)
    {
        m_animDirection = Vector2.down;
        m_animnMap = animMap;
        m_currAnimColdown = 0;
    }

    public void UpdateView(byte[,] newValues)
    {
        SetStartTilePositions();

        for (int i = 0; i < m_fieldSize; i++)
        {
            for (int j = 0; j < m_fieldSize; j++)
            {
                int valueNum = i * m_fieldSize + j;
                byte value = newValues[i, j];

                Text valueText = m_tiles[valueNum].GetComponentInChildren<Text>();
                valueText.text = ConvertValueToStr(value);
            }
        }

        UpdateTilesColor(newValues);
    }
    void UpdateTilesColor(byte[,] values)
    {
        for (int i = 0; i < m_fieldSize; i++)
        {
            for (int j = 0; j < m_fieldSize; j++)
            {
                Color tileColor = GetTileColor(values[i,j]);
                Color txtColor = GetTextColor(values[i, j]);
                int valueNum = i * m_fieldSize + j;

                Image bg = m_tiles[valueNum].GetComponentInChildren<Image>();
                bg.color = tileColor;

                Text txt = m_tiles[valueNum].GetComponentInChildren<Text>();
                txt.color = txtColor;
            }
        }
    }
    public void CreateTileAnimationFromMask(bool[,] mask)
    {
        for (int i = 0; i < m_fieldSize; i++)
        {
            for (int j = 0; j < m_fieldSize; j++)
            {
                if (mask[i, j])
                {
                    int tile = i * m_fieldSize + j;
                    m_tiles[tile].GetComponent<Animation>().Play();
                }
            }
        }
    }
    void SetStartTilePositions()
    {
        for (int i = 0; i < m_fieldSize; i++)
        {
            for (int j = 0; j < m_fieldSize; j++)
            {
                int tile = i * m_fieldSize + j;

                m_tiles[tile].transform.position = m_startTilesPositions[i, j];
            }
        }
    }

    Color GetTextColor(ushort value)
    {
        Color color = (value >= m_startLightColorNum) ? m_lightColor : m_darkColor;

        if (color.a == 0)
        {
            color = m_instantValueColor;
        }

        return color;
    }
    Color GetTileColor(byte value)
    {
        Color color = m_instantTileColor;
        int colorsCount = m_tilesColor.Length;
        int maxColor = colorsCount - 1;

        if (colorsCount != 0 && value != 0)
        {
            if (value > maxColor)
            {
                color = m_tilesColor[maxColor];
            }
            else
            {
                color = m_tilesColor[value - 1];
            }
        }

        return color;
    }
    string ConvertValueToStr(byte value)
    {
        string str = "";

        if (value > 0)
        {
            ulong valueInNormalFormat = 1;

            while (value > 0)
            {
                valueInNormalFormat *= m_power;
                value--;
            }

            str = valueInNormalFormat.ToString();
        }

        return str;
    }

    void SaveStartTilesPositions()
    {
        m_startTilesPositions = new Vector2[m_fieldSize, m_fieldSize];
        int tile = 0;

        for (int i = 0; i < m_fieldSize; i++)
        {
            for (int j = 0; j < m_fieldSize; j++)
            {
                m_startTilesPositions[i, j] = m_tiles[tile].transform.position;
                tile++;
            }
        }
    }

    public bool IsMoveAnimationWork()
    {
        return (m_currAnimColdown < m_animColdown);
    }
}