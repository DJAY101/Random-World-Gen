using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//this was testing code and is not used within the world generation, it was used to learn unity and spawn random blocks

public class LoadWorld : MonoBehaviour
{


    public GameObject[] Blocks;
    public Camera mainCamera;
    float timeTracker = 0;
    System.Random rnd = new System.Random();
    List<List<GameObject>> worldBlocks = new List<List<GameObject>>();


    // Start is called before the first frame update
    void Start()
    {
        mainCamera.transform.position = new Vector3(-8, 8, -8);
        generateWorld();



    }


    void generateWorld() {

        for (int i = 0; i < 16; i++)
        {
            List<GameObject> yBlocks = new List<GameObject>();
            for (int j = 0; j < 16; j++)
            {
                yBlocks.Add( Instantiate(Blocks[rnd.Next(0, 2)], new Vector3Int(i, 0, j), transform.rotation) );
                
            }
            worldBlocks.Add(yBlocks);
        }
    }

    void deleteWorld() {

        foreach (List<GameObject> yBlocks in worldBlocks)
        {
            foreach(GameObject block in yBlocks)
            {
                Destroy(block);
            }
            yBlocks.Clear();

        }
        worldBlocks.Clear();
    }


    // Update is called once per frame
    void Update()
    {

        if(timeTracker > 2.0f)
        {
            timeTracker = 0;
            deleteWorld();
            generateWorld();
        }

        timeTracker += Time.deltaTime;
    }
}
