using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksController : MonoBehaviour
{

    ArrayList m_blocksOnMap;
    ArrayList m_toDelete;

    public Spawner m_spawner;
    public AbstractPlayer m_player;
    public BonusController m_bonusController;
    public BallsController m_ballsController;

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
        GiveDemage();
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
    void GiveDemage()
    {
        int demage = m_ballsController.GetDmg();

        foreach(Block block in m_blocksOnMap)
        {
            int dmgCount = block.GetDmgCount();

            block.SetDemage(dmgCount * demage);
            block.SetDmgCount(0);
        }
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
