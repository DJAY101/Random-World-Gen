using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtBlock : Block
{
    private static Rules blockRules = null;

    public DirtBlock(BlockManager manager) : base(manager)
    {
        blockName = "Dirt";
        blockType = BlockType.Dirt;
        initialiseBlockRules();
    }

    private void initialiseBlockRules()
    {

        if (blockRules == null)
        {
            blockRules = new Rules();
            blockRules.downBlocks = new List<BlockType> {
                BlockType.Dirt,
                BlockType.Empty

            };

            blockRules.upBlocks = new List<BlockType> {
                BlockType.Air,
                BlockType.Empty,
                BlockType.Dirt,
                BlockType.Grass
            };
            blockRules.leftBlocks = new List<BlockType>
            {
                BlockType.Empty,
                BlockType.Dirt,
            };

            blockRules.rightBlocks = blockRules.leftBlocks;
            blockRules.frontBlocks = blockRules.leftBlocks;
            blockRules.backBlocks = blockRules.leftBlocks;

            blockRules.maxHeight = 3;

            ruleSet.Add(BlockType.Dirt, blockRules);

        }

    }

}
