using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class Spawner : MonoBehaviour
{
    StreamReader m_reader;
    ArrayList m_map;

    public string[] m_levels;
    const string m_root = "Assets/Maps/";
    const string m_mapKey = "SpawnLevel";

    const char m_stopReadId = '-';
    const char m_easyBlockId = 'E';
    const char m_normalBlockId = 'N';
    const char m_hardBlockId = 'H';
    const char m_immortalBlockId = 'I';

    public EasyBlock m_easyBlock;
    public NormalBlock m_normalBlock;
    public HardBlock m_hardBlock;
    public ImmortalBlock m_immortalBlock;

    private int m_blocksInLine = 16;

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
        InitLevel();

        string line = m_reader.ReadLine();
        while (line != null && line[0] != m_stopReadId)
        {
            SpawnLine(line);
            line = m_reader.ReadLine();
        }
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

        if (blockId == m_easyBlockId)
        {
            spawnBlock = m_easyBlock;
        }
        else if (blockId == m_normalBlockId)
        {
            spawnBlock = m_normalBlock;
        }
        else if (blockId == m_hardBlockId)
        {
            spawnBlock = m_hardBlock;
        }
        else if (blockId == m_immortalBlockId)
        {
            spawnBlock = m_immortalBlock;
        }

        AddBlockToList(spawnBlock);
    }
    void InitLevel()
    {
        int levelNumber = PlayerPrefs.GetInt(m_mapKey, 0);
        m_reader = new StreamReader(m_root + m_levels[levelNumber]);
    }
    void AddBlockToList(Block spawnBlock)
    {
        if (spawnBlock != null)
        {
            Vector3 spawnPosition = gameObject.transform.position;
            SetColliderWithOffset(spawnBlock, m_offsetSize);
            Block block = Instantiate(spawnBlock, spawnPosition, Quaternion.identity);

            m_map.Add(block);
        }
    }

    public void Clear(ArrayList mapBlocks)
    {
        foreach (Block block in mapBlocks)
        {
            block.DestroyBlock();
        }

        mapBlocks.Clear();
    }

    public void SetColliderWithOffset(Block block, float addingSize)
    {
        BoxCollider collider = block.GetComponent<BoxCollider>();
        collider.size = new Vector3(1, 1, 1);
        Vector3 boxSize = block.transform.localScale;

        float newSizeX = 1 + addingSize / boxSize.x;
        float newSizeY = 1 + addingSize / boxSize.y;
        float newSizeZ = 1 + addingSize / boxSize.z;

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

        gameObject.transform.position = new Vector3(posX, m_heightOnFloor, posZ);
    }
    void MoveToNextPosition()
    {
        gameObject.transform.position += new Vector3(m_blockScale.x + m_offsetSize, 0, 0);
    }
    void MoveToNextRow()
    {
        float blockOffset = m_blockScale.x + m_offsetSize;
        float rowOffset = m_blockScale.z + m_offsetSize;
        gameObject.transform.position -= new Vector3(blockOffset * m_blocksInLine, 0, rowOffset);
    }

    void ClearNulls(ArrayList blocks)
    {
        ArrayList toDelete = new ArrayList();

        if (blocks.Capacity != 0)
        {
            foreach (Block block in blocks)
            {
                if (block == null)
                {
                    toDelete.Add(block);
                }
            }
        }

        foreach (Block deleteBlock in toDelete)
        {
            blocks.Remove(deleteBlock);
        }

        toDelete.Clear();
    }
}
