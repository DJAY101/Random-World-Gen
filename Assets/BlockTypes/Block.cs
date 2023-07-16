using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;


// enumerator of all block types
public enum BlockType { 
    Unknown,
    Empty,
    Air,
    Dirt,
    Grass
}

//the class for all the rules a block must follow
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

    //max spawn height
    public int maxHeight {get; set;}

}

//base class for all blocks
public abstract class Block
{
    //name of the block 
    protected string blockName = "";
    //enum block type
    public BlockType blockType;
    //current block coordinate in the world
    public Vector3 blockCoordinates = Vector3.zero;

    // The spawned block within the world
    public GameObject gameBlock;
    // The block manager to spawn the block into the world
    protected BlockManager blockManager;
    
    //a singleton dictionary that contains the rules of all the blocks
    protected static Dictionary<BlockType, Rules> ruleSet = new Dictionary<BlockType, Rules>();

    //on initialisation the block requires a blockmanager to spawn blocks
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
    //get the block name
    public virtual string getBlockName() {
        return blockName;
    }

    //spawn the block
    public virtual void spawnGeometry()
    {
        gameBlock = blockManager.spawnBlock(this);
    }

    public virtual void deleteGeometry()
    {
        if (!blockManager) return;
        blockManager.deleteBlock(this);       
    }


}
