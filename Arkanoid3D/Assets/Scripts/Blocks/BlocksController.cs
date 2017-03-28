using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksController : MonoBehaviour
{

    ArrayList m_blocksOnMap;
    ArrayList m_toDelete;

    public Spawner m_spawner;
    public AbstractUser m_player;
    public BonusController m_bonusController;
    public BallsController m_ballsController;

    public int m_onFireLayer;

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
    public void PrepareOnFireBlocks()
    {
        int criticalDemage = m_ballsController.GetCriticalDemage();

        foreach(Block block in m_blocksOnMap)
        {
            if (block.GetHealth() <= criticalDemage && !block.IsImmortal())
            {
                block.gameObject.layer = m_onFireLayer;
            }
        }
    }

    void FixedUpdate()
    {
        CheckBlocksLife();
        PrepareOnFireBlocks();
    }
    void CheckBlocksLife()
    {
        foreach (Block block in m_blocksOnMap)
        {
            if (block != null && !block.IsLive())
            {
                m_toDelete.Add(block);
                m_bonusController.DropBonus(block.transform.position);
                m_player.AddPoints(block.GetPoints());
            }
        }

        foreach (Block deleteBlock in m_toDelete)
        {
            m_blocksOnMap.Remove(deleteBlock);
            deleteBlock.DestroyBlock();
        }

        m_toDelete.Clear();
    }

    public int GetBlocksCount(bool isCalculateImmortal)
    {
        int blocksCount = 0;

        foreach (Block block in m_blocksOnMap)
        {
            if (block.IsLive() && (block.IsImmortal() == isCalculateImmortal))
            {
                blocksCount++;
            }
        }

        return blocksCount;
    }
    public int GetOnFireLayer()
    {
        return m_onFireLayer;
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
