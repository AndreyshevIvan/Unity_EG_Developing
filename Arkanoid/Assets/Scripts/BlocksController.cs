using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksController : MonoBehaviour
{

    ArrayList m_blocksOnMap;

    Block m_easyBlock;
    Block m_normalBlock;
    Block m_hardBlock;
    Block m_immortalBlock;

    public EasyBlock easyBlock;
    public NormalBlock normalBlock;
    public HardBlock hardBlock;
    public ImmortalBlock immortalBlock;

    private void Awake()
    {
        m_easyBlock = easyBlock;
        m_normalBlock = normalBlock;
        m_hardBlock = hardBlock;
        m_immortalBlock = immortalBlock;

        m_blocksOnMap = new ArrayList();
    }

    private void FixedUpdate()
    {
        if (m_blocksOnMap != null && m_blocksOnMap.Capacity != 0)
        {
            CheckBlocks();
        }
    }

    private void CheckBlocks()
    {
        List<Block> toDelete = new List<Block>();

        foreach(Block block in m_blocksOnMap)
        {
            if (!block.IsImmortal() && !block.IsLive())
            {
                toDelete.Add(block);
            }
        }

        foreach(Block block in toDelete)
        {
            block.DestroyBlock();
            m_blocksOnMap.Remove(block);
        }

        toDelete.Clear();
    }

    public void AddBlockToList(Block blocks)
    {
        m_blocksOnMap.Add(blocks);
    }

    public Block GetEasyBlock()
    {
        return (m_easyBlock);
    }
    public Block GetNormalBlock()
    {
        return (m_normalBlock);
    }
    public Block GetHardBlock()
    {
        return (m_hardBlock);
    }
    public Block GetImmortalBlock()
    {
        return (m_immortalBlock);
    }
    public Vector3 GetBlockScale()
    {
        Vector3 scale = GetEasyBlock().m_body.transform.localScale;

        return scale;
    }
}
