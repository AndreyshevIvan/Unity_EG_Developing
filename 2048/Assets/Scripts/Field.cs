using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{

    FieldController m_fieldController;
    FieldViewer m_fieldViewer;

    public GameObject m_backgroundsParent;
    public GameObject m_tile;
    public GameObject m_tileBackground;

    byte m_fieldSize;
    Vector2 m_tileSize;
    Vector2 m_fieldRectSize;
    Vector2 m_backTileSize;
    public int m_offset;
    int m_tilesCount;

    private void Awake()
    {
        m_fieldController = GetComponent<FieldController>();
        m_fieldViewer = GetComponent<FieldViewer>();

        m_backgroundsParent = Instantiate(m_backgroundsParent);
        m_backgroundsParent.transform.SetParent(transform);
    }
    public void Create(byte size)
    {
        m_fieldSize = size;
        m_tilesCount = m_fieldSize * m_fieldSize;
        m_fieldRectSize = GetComponent<RectTransform>().rect.size;

        SpawnTiles();

        m_fieldController.Init(m_fieldSize);
    }
    void SpawnTiles()
    {
        SetTileSize();

        GameObject[] tiles = new GameObject[m_tilesCount];
        Vector2 startPos = transform.position + (new Vector3(m_offset, -m_offset, 0));
        Vector2 spawnPos = startPos;
        int tileInRowNum = m_fieldSize;
        Vector3 pivotOffset = new Vector3(m_tileSize.x / 2, -m_tileSize.y / 2, 0);

        for (int i = 0; i < m_tilesCount; i++)
        {
            SpawnTileBackground(spawnPos);

            tileInRowNum--;
            tiles[i] = Instantiate(m_tile);
            tiles[i].transform.SetParent(transform);

            tiles[i].transform.position = spawnPos;
            tiles[i].transform.position += pivotOffset;

            spawnPos.x += m_tileSize.x + m_offset;

            if (tileInRowNum == 0)
            {
                spawnPos.x = startPos.x;
                spawnPos.y -= m_tileSize.y + m_offset;
                tileInRowNum = m_fieldSize;
            }
        }

        m_fieldViewer.Init(ref tiles, m_fieldSize);
    }
    void SpawnTileBackground(Vector2 position)
    {
        GameObject back = Instantiate(m_tileBackground, position, Quaternion.identity);
        back.transform.SetParent(m_backgroundsParent.transform);

        RectTransform backTransform = back.GetComponent<RectTransform>();

        backTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, m_backTileSize.x);
        backTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, m_backTileSize.y);
    }
    public void StartEvents()
    {
        m_fieldController.StartEvents();
        UpdateValuesInView();
        AnimateAutoTurnTiles();
    }

    void UpdateValuesInView()
    {
        byte[,] newValues = m_fieldController.GetCurrentValues();
        m_fieldViewer.UpdateView(newValues);
    }
    void AnimateSumTiles()
    {
        bool[,] sumMask = m_fieldController.GetSumMap();
        m_fieldViewer.CreateTileAnimationFromMask(sumMask);
    }
    void AnimateAutoTurnTiles()
    {
        bool[,] changeMask = m_fieldController.GetChangeMask();
        m_fieldViewer.CreateTileAnimationFromMask(changeMask);
    }

    public uint GetPointsFromLastTurn()
    {
        uint lastPoints = m_fieldController.GetPointsFromLastTurn();

        return lastPoints;
    }

    public void SetAutoTurn(ushort turnsCount, bool isFourAllowed)
    {
        m_fieldController.SetAutoTurn(turnsCount, isFourAllowed);
        UpdateValuesInView();

        AnimateSumTiles();
        AnimateAutoTurnTiles();
    }
    void SetTileSize()
    {
        float spaceForTile = m_fieldRectSize.x - (m_fieldSize + 1) * m_offset;
        float tileSize = spaceForTile / m_fieldSize;

        RectTransform tileTransform = m_tile.GetComponent<RectTransform>();

        tileTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, tileSize);
        tileTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, tileSize);

        m_tileSize = tileTransform.rect.size;
        m_backTileSize = tileTransform.rect.size;
    }

    public bool IsAutoTurnAllowed()
    {
        return (m_fieldController.IsPlayerMadeTurn() && !m_fieldViewer.IsMoveAnimationWork());
    }
    public bool IsTurnPossible()
    {
        return (m_fieldController.IsTurnPossible());
    }
}
