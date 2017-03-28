﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Spawner : MonoBehaviour
{
    StreamReader m_reader;
    ArrayList m_map;

    public InfoController m_info;

    const char STOP_READ_KEY = '-';
    const char EASY_BLOCK_KEY = 'E';
    const char NORMAL_BLOCK_KEY = 'N';
    const char HARD_BLOCK_KEY = 'H';
    const char IMMORTAL_BLOCK_KEY = 'I';

    public EasyBlock m_easyBlock;
    public NormalBlock m_normalBlock;
    public HardBlock m_hardBlock;
    public ImmortalBlock m_immortalBlock;

    int m_blocksInLine = 14;

    public Vector3 m_blockScale;
    public GameObject m_floor;
    public float m_heightOnFloor;
    public float m_posZFactor = 0.95f;
    public float m_offsetSize = 0.05f;

    void Awake()
    {
        m_map = new ArrayList();
    }

    public ArrayList SpawnLevel()
    {
        Clear(m_map);
        SetStartPosition();
        InitReader();

        string line = m_reader.ReadLine();
        while (line != null && line[0] != STOP_READ_KEY)
        {
            SpawnLine(line);
            line = m_reader.ReadLine();
        }
        SetParentToBlocks();
        m_reader.Close();

        return m_map;
    }
    void SpawnLine(string line)
    {
        for (int i = 0; i < line.Length; i++)
        {
            CreateBlock(line[i]);
            MoveToNextPosition();
        }
        MoveToNextRow();
    }
    void CreateBlock(char blockId)
    {
        Block spawnBlock = null;

        if (blockId == EASY_BLOCK_KEY)
        {
            spawnBlock = m_easyBlock;
        }
        else if (blockId == NORMAL_BLOCK_KEY)
        {
            spawnBlock = m_normalBlock;
        }
        else if (blockId == HARD_BLOCK_KEY)
        {
            spawnBlock = m_hardBlock;
        }
        else if (blockId == IMMORTAL_BLOCK_KEY)
        {
            spawnBlock = m_immortalBlock;
        }

        AddBlockToList(spawnBlock);
    }
    void InitReader()
    {
        m_reader = m_info.GetSpawnLevelReader();
    }
    void AddBlockToList(Block spawnBlock)
    {
        if (spawnBlock != null)
        {
            Vector3 spawnPosition = transform.position;
            SetColliderWithOffset(spawnBlock);
            Block block = Instantiate(spawnBlock, spawnPosition, Quaternion.identity);

            m_map.Add(block);
        }
    }
    public void SetColliderWithOffset(Block block)
    {
        BoxCollider collider = block.GetComponent<BoxCollider>();
        collider.size = new Vector3(1, 1, 1);
        Vector3 boxSize = block.transform.localScale;

        float newSizeX = 1 + m_offsetSize / boxSize.x;
        float newSizeY = 1 + m_offsetSize / boxSize.y;
        float newSizeZ = 1 + m_offsetSize / boxSize.z;

        Vector3 newSize = new Vector3(newSizeX, newSizeY, newSizeZ);

        collider.size = newSize;
    }
    void SetStartPosition()
    {
        int offsetCount = (m_blocksInLine - 1);
        float offset = (m_blockScale.x + m_offsetSize);

        Vector3 floorPos = m_floor.transform.position;
        Vector3 floorScale = m_floor.transform.localScale;

        float posX = floorPos.x - (offsetCount * offset) / 2;
        float posZ = (floorPos.z + floorScale.z / 2.0f) * m_posZFactor;

        transform.position = new Vector3(posX, m_heightOnFloor, posZ);
    }
    void SetParentToBlocks()
    {
        foreach (Block block in m_map)
        {
            block.transform.SetParent(transform);
        }
    }
    void MoveToNextPosition()
    {
        transform.position += new Vector3(m_blockScale.x + m_offsetSize, 0, 0);
    }
    void MoveToNextRow()
    {
        float blockOffset = m_blockScale.x + m_offsetSize;
        float rowOffset = m_blockScale.z + m_offsetSize;
        transform.position -= new Vector3(blockOffset * m_blocksInLine, 0, rowOffset);
    }

    void Clear(ArrayList mapBlocks)
    {
        foreach (Block block in mapBlocks)
        {
            block.DestroyBlock();
        }

        mapBlocks.Clear();
    }
}
