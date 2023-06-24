using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GrassBlock : Block
{
    private static Rules blockRules = null;

    public GrassBlock(BlockManager manager) : base(manager)
    {
        blockName = "Grass";
        blockType = BlockType.Grass;
        initialiseBlockRules();
    }

    private void initialiseBlockRules()
    {

        if (blockRules == null)
        {
            blockRules = new Rules();
            blockRules.downBlocks = new List<BlockType> {
                BlockType.Dirt,
                BlockType.Grass
            };

            blockRules.upBlocks = new List<BlockType> {
                BlockType.Air,
                BlockType.Empty
                
            };
            blockRules.leftBlocks = new List<BlockType>
            {
                BlockType.Empty,
                BlockType.Air,
                BlockType.Grass
            };

            blockRules.rightBlocks = blockRules.leftBlocks;
            blockRules.frontBlocks = blockRules.leftBlocks;
            blockRules.backBlocks = blockRules.leftBlocks;

            blockRules.maxHeight = 6;

            ruleSet.Add(BlockType.Grass, blockRules);

        }

    }

}
