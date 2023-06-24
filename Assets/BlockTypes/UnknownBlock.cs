using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnknownBlock : Block
{
    public bool collapsed = false;



    public UnknownBlock() : base(null)
    {
        blockType = BlockType.Unknown;
    }

    public List<BlockType> CalculatePossibleBlocks(SurroundingBlocks surroundingBlocks, int height) {

        List<BlockType> possibleBlocks = new List<BlockType>();

        for (int i = 2; i < BlockType.GetNames(typeof(BlockType)).Length; i++) {

            if (Block.ruleSet[(BlockType)i].maxHeight < height && Block.ruleSet[(BlockType)i].maxHeight != -1) continue;

            if (!Block.ruleSet[(BlockType)i].upBlocks.Exists(bT => surroundingBlocks.upBlock == bT || surroundingBlocks.upBlock == BlockType.Unknown)) continue;
            
            if (!Block.ruleSet[(BlockType)i].downBlocks.Exists(bT => surroundingBlocks.downBlock == bT || surroundingBlocks.downBlock == BlockType.Unknown)) continue;

            if (!Block.ruleSet[(BlockType)i].leftBlocks.Exists(bT => surroundingBlocks.leftBlock == bT || surroundingBlocks.leftBlock == BlockType.Unknown)) continue;

            if (!Block.ruleSet[(BlockType)i].rightBlocks.Exists(bT => surroundingBlocks.rightBlock == bT || surroundingBlocks.rightBlock == BlockType.Unknown)) continue;

            if (!Block.ruleSet[(BlockType)i].frontBlocks.Exists(bT => surroundingBlocks.frontBlock == bT || surroundingBlocks.frontBlock == BlockType.Unknown)) continue;
            
            if (!Block.ruleSet[(BlockType)i].backBlocks.Exists(bT => surroundingBlocks.backBlock == bT || surroundingBlocks.backBlock == BlockType.Unknown)) continue;

            possibleBlocks.Add((BlockType)i);

        }
        return possibleBlocks;


    }



    public override void spawnGeometry() {
        Debug.Log("error spawning a unknownblock");
    }

}
