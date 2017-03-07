using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Spawner : MonoBehaviour
{
    public string[] m_mapsPath;
    private int m_blocksInLine = 16;

    StreamReader m_reader;
    BlocksController m_blocksController;

    Vector3 m_floorScale;
    Vector3 m_floorPosition;
    Vector3 m_blockScale;
    float m_height;
    float m_posZFactor = 0.95f;
    public float m_offsetSize = 0.05f;

    char m_stopReadId = '-';
    char m_easyBlockId = 'E';
    char m_normalBlockId = 'N';
    char m_hardBlockId = 'H';
    char m_immortalBlockId = 'I';

    public void Init(BlocksController blocksController, GameObject floor, float blocksHeight)
    {
        m_floorScale = floor.transform.localScale;
        m_floorPosition = floor.transform.position;
        m_height = blocksHeight;

        m_blocksController = blocksController;
        m_blocksController.SetColliderWithOffset(m_offsetSize);
        m_blockScale = m_blocksController.GetBlockScale();
    }

    public void SpawnLevel()
    {
        SetStartPosition();

        m_reader = new StreamReader(m_mapsPath[0]);

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

        float posX = m_floorPosition.x - (offsetCount * offset) / 2;
        float posY = m_height;
        float posZ = (m_floorPosition.z + m_floorScale.z / 2.0f) * m_posZFactor;

        gameObject.transform.position = new Vector3(posX, posY, posZ);
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
