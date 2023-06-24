using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;


public enum BlockType { 
    Unknown,
    Empty,
    Air,
    Dirt,
    Grass
}
public class Rules {

    //follows the y axis following unity engine reference frame
    public List<BlockType> upBlocks { get; set; }
    public List<BlockType> downBlocks { get; set; }

    //follows the x axis following unity engine reference frame
    public List<BlockType> leftBlocks { get; set; }
    public List<BlockType> rightBlocks { get; set; }

    //follows the z axis following unity engine reference frame
    public List<BlockType> frontBlocks { get; set; }
    public List<BlockType> backBlocks { get; set; }

    public int maxHeight {get; set;}

}


public abstract class Block
{

    protected string blockName = "";
    public BlockType blockType;
    public Vector3 blockCoordinates = Vector3.zero;

    protected GameObject gameBlock;
    protected BlockManager blockManager;

    protected static Dictionary<BlockType, Rules> ruleSet = new Dictionary<BlockType, Rules>();

    public Block(BlockManager manager)
    {
        blockManager = manager;
    }


    // Start is called before the first frame update
    void Start()
    {

        blockSetup();

    }

    protected virtual void blockSetup() {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual string getBlockName() {
        return blockName;
    }

    public virtual void spawnGeometry()
    {
        gameBlock = blockManager.spawnBlock(this);
    }

}
