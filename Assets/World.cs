using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;





// class that holds the surrounding blocks of a given block
public class SurroundingBlocks {

    //follows the y axis following unity engine reference frame
    public BlockType upBlock { get; set; }
    public BlockType downBlock { get; set; }

    //follows the x axis following unity engine reference frame
    public BlockType leftBlock { get; set; }
    public BlockType rightBlock { get; set; }

    //follows the z axis following unity engine reference frame
    public BlockType frontBlock { get; set; }
    public BlockType backBlock { get; set; }

    //prints the block that surrounds a block
    public void printDebug() {

        Debug.LogFormat("upblock: {0} downblock: {1} rightblock: {2} leftblock: {3} frontBlock{4} backBlock{5}", Enum.GetName(typeof(BlockType), upBlock), Enum.GetName(typeof(BlockType), downBlock), Enum.GetName(typeof(BlockType), rightBlock), Enum.GetName(typeof(BlockType), leftBlock), Enum.GetName(typeof(BlockType), frontBlock), Enum.GetName(typeof(BlockType), backBlock));

    }

}






public class World : MonoBehaviour
{
    //the world size that the random world generator would create
    public Vector3 worldSize;

    //the block manager that is in charge of spawning and rendering the blocks
    public BlockManager blockManager;

    //the 3D array of the world
    public List<List<List<Block>>> worldData = new List<List<List<Block>>>();

    // Start is called before the first frame update
    void Start()
    {
        // an instance of each block is first initialised to initiliase their rulesets into the block singleton
        AirBlock air = new AirBlock(blockManager);
        GrassBlock grass = new GrassBlock(blockManager);
        DirtBlock dirt = new DirtBlock(blockManager);

        //First fill the world with unknown blocks
        loadWorldWithUnknown();
        
        //generate the world
        iterateGenerateWorld();


    }


    void loadWorldWithUnknown() { 
    
        // loop for the x axis of the array
        for (int x = 0; x < worldSize.x; x++)
        {
            // array to contain rows of Y blocks
            List<List<Block>> tempY = new List<List<Block>>();

            // loop for the y axis of the array
            for (int y = 0; y<worldSize.y; y++)
            {
                // array to contain rows of z axis
                List<Block> tempZ = new List<Block>();

                // loop for individiual z axis
                for (int z = 0; z < worldSize.z; z++)
                {
                    // create a new unknownblock
                    UnknownBlock temp = new();
                    // assign it to the array
                    tempZ.Add(temp);
                }
                //assign Z row to a Y Row
                tempY.Add(tempZ);

            }
            //assign Y row to the x Row
            worldData.Add(tempY);
 
        }
    
    }





    void iterateGenerateWorld()
    {
        // an array of all the coordinate in the world
        List<Vector3> coordinatesToIterate = new List<Vector3>();

        for (int x = 0; x < worldSize.x; x++)
        {
            for (int y = 0; y < worldSize.y; y++)
            {
                for (int z = 0; z < worldSize.z; z++)
                {
                    Vector3 temp = new Vector3(x, y, z);
                    coordinatesToIterate.Add(temp);
                }
            }
        }


        //randomize the coordinates in the array
        for (int i = 0; i < coordinatesToIterate.Count; i++)
        {
            // randomly swapping elements within the array
            Vector3 temp = coordinatesToIterate[i];
            int randomIndex = UnityEngine.Random.Range(i, coordinatesToIterate.Count);
            coordinatesToIterate[i] = coordinatesToIterate[randomIndex];
            coordinatesToIterate[randomIndex] = temp;
        }

        // loop through each vector coordinate
        foreach (var coordinate in coordinatesToIterate)
        {
            // the x y z components of the vector
            int x = (int)coordinate.x;
            int y = (int)coordinate.y;
            int z = (int)coordinate.z;

            // get the block at the given coordinate
            Block tempBlock = getBlockAt(x, y, z);

            // an array of all the possible blocks that can be spawned given the surrounding blocks 
            List<BlockType> possibleBlocks = ((UnknownBlock)tempBlock).CalculatePossibleBlocks(GetSurroundingBlock(x, y, z), z);

            //if there is only one possible block that can be spawned then spawn that block
            if (possibleBlocks.Count == 1)
            {
                replaceBlock(possibleBlocks[0], x, y, z);
                getBlockAt(x, y, z).blockCoordinates = new Vector3(x, z, y);
                getBlockAt(x, y, z).spawnGeometry();
            }
            // else if there are multiple possible blocks then select a random one
            else if (possibleBlocks.Count > 1)
            {
                replaceBlock(possibleBlocks[UnityEngine.Random.Range(0, possibleBlocks.Count)], x, y, z);
                getBlockAt(x, y, z).blockCoordinates = new Vector3(x, z, y);
                Debug.Log(getBlockAt(x, y, z).getBlockName());
                getBlockAt(x, y, z).spawnGeometry();
            }

            //if (possibleBlocks.Count == )
            //{
            //     Debug.Log("NOTHING");
            //}
            





        }






    }

    // replace a block in the world array with another block
    void replaceBlock(BlockType replaceTo, int x, int y, int z)
    {
        Block newBlock;
        if(replaceTo == BlockType.Air)
        {
            newBlock = new AirBlock(blockManager);
        } else if (replaceTo == BlockType.Dirt)
        {
            newBlock = new DirtBlock(blockManager);
        } else if (replaceTo == BlockType.Grass)
        {
            newBlock = new GrassBlock(blockManager);
        } else
        {
            newBlock = new UnknownBlock();
        }



        worldData[x][y][z] = newBlock;

    }








    // Update is called once per frame
    void Update()
    {
        
    }
    //get the block at a given x y z coordinate
    public Block getBlockAt(int x, int y, int z)
    {
        return worldData[x][y][z];
    }
    // get the block at a given vector coordinate
    public Block getBlockAt(Vector3 blockPosition)
    {

        return worldData[Mathf.RoundToInt(blockPosition.x)][Mathf.RoundToInt(blockPosition.y)][Mathf.RoundToInt(blockPosition.z)];

    }

    // return the surrounding block with a vector coordinate
    public SurroundingBlocks GetSurroundingBlock(Vector3 blockPosition) {

        return GetSurroundingBlock(Mathf.RoundToInt(blockPosition.x),Mathf.RoundToInt(blockPosition.y),Mathf.RoundToInt(blockPosition.z));

    }

    // return the surroundingblocks given an x y z coordinate
    public SurroundingBlocks GetSurroundingBlock(int x, int y, int z)
    {
        //create a new surrounding blocks object
        SurroundingBlocks surroundingBlocks = new SurroundingBlocks();

        //if the right block is outside the array then its empty
        if (x + 2 > worldData.Count)
        {
            surroundingBlocks.rightBlock = BlockType.Empty;
        }
        //else its the block next to it
        else {
            surroundingBlocks.rightBlock = worldData[x + 1][y][z].blockType;
        }

        //if the left block is outside the array then its empty
        if (x - 1 < 0)
        {
            surroundingBlocks.leftBlock = BlockType.Empty;
        }
        // else its the block next to it
        else
        {
            surroundingBlocks.leftBlock = worldData[x - 1][y][z].blockType;
        }


        //if the front block is outside the array then its empty
        if (y + 2 > worldData[x].Count)
        {
            surroundingBlocks.frontBlock = BlockType.Empty;
        }
        // else its the block next to it
        else
        {
            surroundingBlocks.frontBlock = worldData[x][y + 1][z].blockType;
        }

        //if the back block is outside the array then its empty
        if (y - 1 < 0)
        {
            surroundingBlocks.backBlock = BlockType.Empty;
        }
        // else its the block next to it
        else
        {
            surroundingBlocks.backBlock = worldData[x][y - 1][z].blockType;
        }



        // if the up block is outside the array then its empty
        if (z + 2 > worldData[x][y].Count)
        {
            surroundingBlocks.upBlock = BlockType.Empty;
        }
        // else its the block next to it

        else
        {
            surroundingBlocks.upBlock = worldData[x][y][z+1].blockType;
        }

        //if the downblock is outside the array then its empty
        if (z - 1 < 0)
        {
            surroundingBlocks.downBlock = BlockType.Empty;
        }
        // else its the block next to it
        else
        {
            surroundingBlocks.downBlock = worldData[x][y][z - 1].blockType;
        }

        return surroundingBlocks;

    }



}
