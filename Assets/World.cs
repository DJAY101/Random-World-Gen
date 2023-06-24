using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;






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


    public void printDebug() {

        Debug.LogFormat("upblock: {0} downblock: {1} rightblock: {2} leftblock: {3} frontBlock{4} backBlock{5}", Enum.GetName(typeof(BlockType), upBlock), Enum.GetName(typeof(BlockType), downBlock), Enum.GetName(typeof(BlockType), rightBlock), Enum.GetName(typeof(BlockType), leftBlock), Enum.GetName(typeof(BlockType), frontBlock), Enum.GetName(typeof(BlockType), backBlock));

    }

}






public class World : MonoBehaviour
{
    public Vector3 worldSize;
    public BlockManager blockManager;
    public List<List<List<Block>>> worldData = new List<List<List<Block>>>();

    // Start is called before the first frame update
    void Start()
    {
        AirBlock air = new AirBlock(blockManager);
        GrassBlock grass = new GrassBlock(blockManager);
        DirtBlock dirt = new DirtBlock(blockManager);

        loadWorldWithUnknown();
        iterateGenerateWorld();

        //AirBlock air = new AirBlock(blockManager);
        //GrassBlock test3 = new GrassBlock(blockManager);
        //UnknownBlock test2 = new UnknownBlock();


        //DirtBlock test = new DirtBlock(blockManager);
        //test.blockCoordinates = new Vector3(0, 0, 0);
        //test.spawnGeometry();

        //worldData[0][0][0] = test;

        //GetSurroundingBlock(0, 0, 1).printDebug();
        //foreach (BlockType blockT in ((UnknownBlock)worldData[0][0][1]).CalculatePossibleBlocks(GetSurroundingBlock(0, 0, 1))) {
           
        //    Debug.Log(blockT);

        //}


    }


    void loadWorldWithUnknown() { 
    
        for (int x = 0; x < worldSize.x; x++)
        {

            List<List<Block>> tempY = new List<List<Block>>();
            for (int y = 0; y<worldSize.y; y++)
            {
                List<Block> tempZ = new List<Block>();
                for (int z = 0; z < worldSize.z; z++)
                {
                    UnknownBlock temp = new();
                    tempZ.Add(temp);
                }

                tempY.Add(tempZ);

            }
            worldData.Add(tempY);
 
        }
    
    }





    void iterateGenerateWorld()
    {
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


        //randomize coordinates
        for (int i = 0; i < coordinatesToIterate.Count; i++)
        {
            Vector3 temp = coordinatesToIterate[i];
            int randomIndex = UnityEngine.Random.Range(i, coordinatesToIterate.Count);
            coordinatesToIterate[i] = coordinatesToIterate[randomIndex];
            coordinatesToIterate[randomIndex] = temp;
        }


        foreach (var coordinate in coordinatesToIterate)
        {
            int x = (int)coordinate.x;
            int y = (int)coordinate.y;
            int z = (int)coordinate.z;
            Block tempBlock = getBlockAt(x, y, z);
            if (tempBlock.blockType == BlockType.Unknown)
            {
                List<BlockType> possibleBlocks = ((UnknownBlock)tempBlock).CalculatePossibleBlocks(GetSurroundingBlock(x, y, z), z);
                if (possibleBlocks.Count == 1)
                {
                    replaceBlock(possibleBlocks[0], x, y, z);
                    getBlockAt(x, y, z).blockCoordinates = new Vector3(x, z, y);
                    getBlockAt(x, y, z).spawnGeometry();
                }
                else if (possibleBlocks.Count > 1)
                {
                    replaceBlock(possibleBlocks[UnityEngine.Random.Range(0, possibleBlocks.Count)], x, y, z);
                    getBlockAt(x, y, z).blockCoordinates = new Vector3(x, z, y);
                    Debug.Log(getBlockAt(x, y, z).getBlockName());
                    getBlockAt(x, y, z).spawnGeometry();
                }
                if (possibleBlocks.Count == 1)
                {
                    Debug.Log("NOTHING");
                }
            }





        }






    }


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

    public Block getBlockAt(int x, int y, int z)
    {
        return worldData[x][y][z];
    }

    public Block getBlockAt(Vector3 blockPosition)
    {

        return worldData[Mathf.RoundToInt(blockPosition.x)][Mathf.RoundToInt(blockPosition.y)][Mathf.RoundToInt(blockPosition.z)];

    }


    public SurroundingBlocks GetSurroundingBlock(Vector3 blockPosition) {

        return GetSurroundingBlock(Mathf.RoundToInt(blockPosition.x),Mathf.RoundToInt(blockPosition.y),Mathf.RoundToInt(blockPosition.z));

    }

    public SurroundingBlocks GetSurroundingBlock(int x, int y, int z)
    {
        SurroundingBlocks surroundingBlocks = new SurroundingBlocks();

        if (x + 2 > worldData.Count)
        {
            surroundingBlocks.rightBlock = BlockType.Empty;
        }
        else {
            surroundingBlocks.rightBlock = worldData[x + 1][y][z].blockType;
        }

        if (x - 1 < 0)
        {
            surroundingBlocks.leftBlock = BlockType.Empty;
        }
        else
        {
            surroundingBlocks.leftBlock = worldData[x - 1][y][z].blockType;
        }



        if (y + 2 > worldData[x].Count)
        {
            surroundingBlocks.frontBlock = BlockType.Empty;
        }
        else
        {
            surroundingBlocks.frontBlock = worldData[x][y + 1][z].blockType;
        }

        if (y - 1 < 0)
        {
            surroundingBlocks.backBlock = BlockType.Empty;
        }
        else
        {
            surroundingBlocks.backBlock = worldData[x][y - 1][z].blockType;
        }



        if (z + 2 > worldData[x][y].Count)
        {
            surroundingBlocks.upBlock = BlockType.Empty;
        }
        else
        {
            surroundingBlocks.upBlock = worldData[x][y][z+1].blockType;
        }

        if (z - 1 < 0)
        {
            surroundingBlocks.downBlock = BlockType.Empty;
        }
        else
        {
            surroundingBlocks.downBlock = worldData[x][y][z - 1].blockType;
        }

        return surroundingBlocks;

    }



}
