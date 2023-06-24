using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GrassBlock : Block
{
    private static Rules blockRules = null;

    public GrassBlock(BlockManager manager) : base(manager)
    {
        // block setup
        blockName = "Grass";
        blockType = BlockType.Grass;
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
                BlockType.Grass
            };

            // blocks allowed above this block
            blockRules.upBlocks = new List<BlockType> {
                BlockType.Air,
                BlockType.Empty
                
            };

            // blocks allowed left of this block
            blockRules.leftBlocks = new List<BlockType>
            {
                BlockType.Empty,
                BlockType.Air,
                BlockType.Grass
            };

            blockRules.rightBlocks = blockRules.leftBlocks;
            blockRules.frontBlocks = blockRules.leftBlocks;
            blockRules.backBlocks = blockRules.leftBlocks;

            // set the max height
            blockRules.maxHeight = 6;

            // apply the rules
            ruleSet.Add(BlockType.Grass, blockRules);

        }

    }

}
