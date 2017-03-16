using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FieldViewer : MonoBehaviour
{

    FieldController m_fieldController;

    public GameObject[] m_tiles;
    public Color[] m_tilesColor;
    int m_fieldSize;

    public Color m_darkColor;
    public Color m_lightColor;
    public ushort m_startLightColorNum;

    Vector2[,] m_startTilesPositions;
    const float m_animColdown = 0.1f;
    float m_currAnimColdown = 0;
    Vector2 m_animDirection;
    ushort[,] m_animnMap;
    const float m_animOneOffset = 120;

    private void Awake()
    {
        m_fieldController = GetComponent<FieldController>();
        m_fieldSize = m_fieldController.GetFieldSize();
        m_currAnimColdown = 2 * m_animColdown;

        SaveStartTilesPositions();
    }

    private void FixedUpdate()
    {
        if (!IsAnimationsEnded())
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
                    Vector2 distance = m_animDirection * m_animOneOffset * offsetCount;

                    m_tiles[tileNum].transform.Translate(distance * time);
                }
            }
        }
    }
    public void AnimateRightTurn()
    {
        if (GetAnimInfo())
        {
            m_animDirection = Vector2.right;
        }
    }
    public void AnimateLeftTurn()
    {
        if (GetAnimInfo())
        {
            m_animDirection = Vector2.left;
        }
    }
    public void AnimateUpTurn()
    {
        if (GetAnimInfo())
        {
            m_animDirection = Vector2.up;
        }
    }
    public void AnimateDownTurn()
    {
        if (GetAnimInfo())
        {
            m_animDirection = Vector2.down;
        }
    }
    bool GetAnimInfo()
    {
        if (m_fieldController.IsPlayerMadeTurn())
        {
            m_animnMap = m_fieldController.GetCurrentAnimMap();
            m_currAnimColdown = 0;

            return true;
        }

        return false;
    }

    public void UpdateView()
    {
        SetStartTilePositions();

        ushort[,] values = m_fieldController.GetCurrentValues();

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
                    valueText.text = value.ToString();
                }

                bool[,] sumMap = m_fieldController.GetSumMap();
                CreateSumAnimationFromMask(sumMap);
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
    public void CreateSumAnimationFromMask(bool[,] mask)
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

    public bool IsAnimationsEnded()
    {
        return (m_currAnimColdown >= m_animColdown);
    }
}