using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtBlock : Block
{
    private static Rules blockRules = null;

    public DirtBlock(BlockManager manager) : base(manager)
    {
        // block setup
        blockName = "Dirt";
        blockType = BlockType.Dirt;
        initialiseBlockRules();
    }

    private void initialiseBlockRules()
    {

        if (blockRules == null)
        {
            blockRules = new Rules();

            // blocks allowed below this block
            blockRules.downBlocks = new List<BlockType> {
                BlockType.Dirt,
                BlockType.Empty

            };

            // blocks allowed above this block
            blockRules.upBlocks = new List<BlockType> {
                BlockType.Air,
                BlockType.Empty,
                BlockType.Dirt,
                BlockType.Grass
            };

            // blocks allowed to the left of this block
            blockRules.leftBlocks = new List<BlockType>
            {
                BlockType.Empty,
                BlockType.Dirt,
            };

            blockRules.rightBlocks = blockRules.leftBlocks;
            blockRules.frontBlocks = blockRules.leftBlocks;
            blockRules.backBlocks = blockRules.leftBlocks;

            // the max spawnable height of this block
            blockRules.maxHeight = 3;

            // apply the rules
            ruleSet.Add(BlockType.Dirt, blockRules);

        }

    }

}
