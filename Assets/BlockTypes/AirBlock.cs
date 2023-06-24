using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBlock : Block
{
    private static Rules blockRules = null;

    public AirBlock(BlockManager manager) : base(manager)
    {

        // block setup
        blockName = "Air";
        blockType = BlockType.Air;
        initialiseBlockRules();
    }

    //setup block rules
    private void initialiseBlockRules() {

        if (blockRules == null)
        {

            blockRules = new Rules();
            //all the blocks allowed below this block
            blockRules.downBlocks = new List<BlockType> {
                BlockType.Grass,
                BlockType.Air
            };

            // all the blocks allowed above this block
            blockRules.upBlocks = new List<BlockType> {
                BlockType.Air,
                BlockType.Empty
            };
            // all the blocks allowed to the left of this block
            blockRules.leftBlocks = new List<BlockType>
            {
                BlockType.Air,
                BlockType.Empty,
                BlockType.Grass
            };

            blockRules.rightBlocks = blockRules.leftBlocks;
            blockRules.frontBlocks = blockRules.leftBlocks;
            blockRules.backBlocks = blockRules.leftBlocks;

            //there is no max height
            blockRules.maxHeight = -1;

            // apply the rules
            ruleSet.Add(BlockType.Air, blockRules);

        }

    }
    public override void spawnGeometry()
    {
        //Debug.Log("error spawning a Air block");
    }

    protected override void blockSetup()
    {


    }

}
