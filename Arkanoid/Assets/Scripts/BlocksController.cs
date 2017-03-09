using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksController : MonoBehaviour
{

    ArrayList m_blocksOnMap;
    ArrayList m_toDelete;

    public Spawner m_spawner;
    public AbstractPlayer m_player;

    void Awake()
    {
        m_blocksOnMap = new ArrayList();
        m_toDelete = new ArrayList();
    }
    public void CreateLevel()
    {
        ClearBlocks();
        m_blocksOnMap = m_spawner.SpawnLevel();
    }

    void FixedUpdate()
    {
        CheckBlocksLife();
    }
    void CheckBlocksLife()
    {
        if (m_blocksOnMap.Capacity != 0)
        {
            foreach (Block block in m_blocksOnMap)
            {
                if (block != null && !block.IsLive())
                {
                    m_toDelete.Add(block);
                    m_player.AddPoints(block.GetPoints());
                }
            }
        }

        foreach (Block deleteBlock in m_toDelete)
        {
            m_blocksOnMap.Remove(deleteBlock);
            deleteBlock.DestroyBlock();
        }

        m_toDelete.Clear();
    }

    public void ClearBlocks()
    {
        foreach (Block block in m_blocksOnMap)
        {
            block.DestroyBlock();
        }

        m_blocksOnMap.Clear();
    }
}
