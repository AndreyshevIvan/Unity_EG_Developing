using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Spawner : MonoBehaviour
{

    public EasyBlock m_easyBlock;
    public NormalBlock m_normalBlock;
    public HardBlock m_hardBlock;
    public ImmortalBlock m_immortalBlock;

    ArrayList m_spawnedBlocks;

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
        SetColliderWithOffset(m_offsetSize);
        m_blockScale = GetBlockScale();

        m_spawnedBlocks = new ArrayList();
    }
    public ArrayList SpawnLevel()
    {
        SetStartPosition();
        Clear();

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

        return m_spawnedBlocks;
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
    public void SetColliderWithOffset(float offsetBetweenBlocks)
    {
        SetFactorToCollider(m_easyBlock, offsetBetweenBlocks);
        SetFactorToCollider(m_normalBlock, offsetBetweenBlocks);
        SetFactorToCollider(m_hardBlock, offsetBetweenBlocks);
        SetFactorToCollider(m_immortalBlock, offsetBetweenBlocks);
    }
    void SetFactorToCollider(Block block, float addingSize)
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

    public Vector3 GetBlockScale()
    {
        Vector3 scale = m_easyBlock.gameObject.transform.localScale;

        return scale;
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

        if (spawnBlock != null)
        {
            Vector3 spawnPosition = gameObject.transform.position;
            Block block = Instantiate(spawnBlock, spawnPosition, Quaternion.identity);
            m_spawnedBlocks.Add(block);
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

    void Clear()
    {
        foreach(Block block in m_spawnedBlocks)
        {
            block.DestroyBlock();
        }

        m_spawnedBlocks.Clear();
    }
}
