using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksController : MonoBehaviour
{

    ArrayList m_blocksOnMap;

    public EasyBlock m_easyBlock;
    public NormalBlock m_normalBlock;
    public HardBlock m_hardBlock;
    public ImmortalBlock m_immortalBlock;

    private void Awake()
    {
        m_blocksOnMap = new ArrayList();
    }
    public void AddBlock(Block block)
    {
        m_blocksOnMap.Add(block);
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
        Vector3 scale = GetEasyBlock().gameObject.transform.localScale;

        return scale;
    }

    public void ClearBlocks()
    {
        if (m_blocksOnMap.Capacity != 0)
        {
            foreach(Block block in m_blocksOnMap)
            {
                block.DestroyBlock();
            }
        }

        m_blocksOnMap.Clear();
    }
}
