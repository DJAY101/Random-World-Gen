using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnknownBlock : Block
{



    public UnknownBlock() : base(null)
    {
        // block setup
        blockType = BlockType.Unknown;
    }

    //calculates the possible blocks that can be spawned given the surrounding blocks and the height
    public List<BlockType> CalculatePossibleBlocks(SurroundingBlocks surroundingBlocks, int height) {

        // an array of all the possible blocks that can be spawned
        List<BlockType> possibleBlocks = new List<BlockType>();

        // loop through all the types of blocks skipping empty and unknown blocks
        for (int i = 2; i < BlockType.GetNames(typeof(BlockType)).Length; i++) {

            if (Block.ruleSet[(BlockType)i].maxHeight < height && Block.ruleSet[(BlockType)i].maxHeight != -1) continue; // if it does not meet the height requirement skip

            // if the surrounding blocks breach any of the rule set skip the bloc

            if (!Block.ruleSet[(BlockType)i].upBlocks.Exists(bT => surroundingBlocks.upBlock == bT || surroundingBlocks.upBlock == BlockType.Unknown)) continue;
            
            if (!Block.ruleSet[(BlockType)i].downBlocks.Exists(bT => surroundingBlocks.downBlock == bT || surroundingBlocks.downBlock == BlockType.Unknown)) continue;

            if (!Block.ruleSet[(BlockType)i].leftBlocks.Exists(bT => surroundingBlocks.leftBlock == bT || surroundingBlocks.leftBlock == BlockType.Unknown)) continue;

            if (!Block.ruleSet[(BlockType)i].rightBlocks.Exists(bT => surroundingBlocks.rightBlock == bT || surroundingBlocks.rightBlock == BlockType.Unknown)) continue;

            if (!Block.ruleSet[(BlockType)i].frontBlocks.Exists(bT => surroundingBlocks.frontBlock == bT || surroundingBlocks.frontBlock == BlockType.Unknown)) continue;
            
            if (!Block.ruleSet[(BlockType)i].backBlocks.Exists(bT => surroundingBlocks.backBlock == bT || surroundingBlocks.backBlock == BlockType.Unknown)) continue;

            // if the block follows all the rules then add it to the possble blocks array
            possibleBlocks.Add((BlockType)i);

        }
        return possibleBlocks;


    }



    public override void spawnGeometry() {
        Debug.Log("error spawning a unknownblock");
    }

}
