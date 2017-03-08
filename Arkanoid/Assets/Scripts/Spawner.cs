using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Spawner : MonoBehaviour
{
    public BlocksController m_blocksController;

    StreamReader m_reader;
    public string[] m_levels;

    private int m_blocksInLine = 16;

    Vector3 m_blockScale;
    public GameObject m_floor;
    public float m_heightOnFloor;
    public float m_posZFactor = 0.95f;
    public float m_offsetSize = 0.05f;

    char m_stopReadId = '-';
    char m_easyBlockId = 'E';
    char m_normalBlockId = 'N';
    char m_hardBlockId = 'H';
    char m_immortalBlockId = 'I';

    void Awake()
    {
        m_blocksController.SetColliderWithOffset(m_offsetSize);
        m_blockScale = m_blocksController.GetBlockScale();

        SpawnLevel();
    }

    public void SpawnLevel()
    {
        SetStartPosition();

        int levelNumber = PlayerPrefs.GetInt("SpawnLevel", 0);

        Debug.Log(levelNumber);
        m_reader = new StreamReader("Assets/Maps/" + m_levels[levelNumber]);

        string line = m_reader.ReadLine();
        while (line != null && line[0] != m_stopReadId)
        {
            SpawnLine(line);
            line = m_reader.ReadLine();
        }

        m_reader.Close();
    }
    void SetStartPosition()
    {
        int offsetCount = (m_blocksInLine - 1);
        float offset = (m_blockScale.x + m_offsetSize);

        Vector3 floorPos = m_floor.transform.position;
        Vector3 floorScale = m_floor.transform.localScale;

        float posX = floorPos.x - (offsetCount * offset) / 2;
        float posZ = (floorPos.z + floorScale.z / 2.0f) * m_posZFactor;

        gameObject.transform.position = new Vector3(posX, m_heightOnFloor, posZ);
    }

    void SpawnLine(string line)
    {
        for (int i = 0; i < m_blocksInLine; i++)
        {
            SpawnBlock(line[i]);
            MoveToNextBlockPos();
        }
        MoveToNextLine();
    }
    void SpawnBlock(char blockId)
    {
        Block spawnBlock = null;

        if (blockId == m_easyBlockId)
        {
            spawnBlock = m_blocksController.GetEasyBlock();
        }
        else if (blockId == m_normalBlockId)
        {
            spawnBlock = m_blocksController.GetNormalBlock();
        }
        else if (blockId == m_hardBlockId)
        {
            spawnBlock = m_blocksController.GetHardBlock();
        }
        else if (blockId == m_immortalBlockId)
        {
            spawnBlock = m_blocksController.GetImmortalBlock();
        }

        if (spawnBlock != null)
        {
            Vector3 spawnPosition = gameObject.transform.position;
            Block block = Instantiate(spawnBlock, spawnPosition, Quaternion.identity);
            m_blocksController.AddBlock(block);
        }
    }
    void MoveToNextBlockPos()
    {
        gameObject.transform.position += new Vector3(m_blockScale.x + m_offsetSize, 0, 0);
    }
    void MoveToNextLine()
    {
        float blockOffset = m_blockScale.x + m_offsetSize;
        float rowOffset = m_blockScale.z + m_offsetSize;
        gameObject.transform.position -= new Vector3(blockOffset * m_blocksInLine, 0, rowOffset);
    }
}
