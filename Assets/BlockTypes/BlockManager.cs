using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{

    public List<GameObject> BlockTypes;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public GameObject findBlockObject(string blockName) {

    //    return BlockTypes.Find(block => block.GetComponent<Block>().getBlockName() == blockName);
        
    //}

    //spawn a the block
    public GameObject spawnBlock(Block block) {

        return Instantiate(BlockTypes.Find(blockType => blockType.name == block.getBlockName()), block.blockCoordinates, transform.rotation);

    }

}
