using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksController : MonoBehaviour
{

    ArrayList m_blocksOnMap;

    public Spawner m_spawner;

    private void Awake()
    {
        m_blocksOnMap = new ArrayList();
    }
    public void AddBlock(Block block)
    {
        m_blocksOnMap.Add(block);
    }
    public void CreateLevel()
    {
        ClearBlocks();
        m_blocksOnMap = m_spawner.SpawnLevel();
    }

    private void FixedUpdate()
    {
        ArrayList toDelete = new ArrayList();

        if (m_blocksOnMap.Capacity != 0)
        {
            foreach(Block block in m_blocksOnMap)
            {
                if (!block.IsLive())
                {
                    toDelete.Add(block);
                }
            }
        }

        foreach(Block deleteBlock in toDelete)
        {
            m_blocksOnMap.Remove(deleteBlock);
            deleteBlock.DestroyBlock();
        }

        toDelete.Clear();
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
