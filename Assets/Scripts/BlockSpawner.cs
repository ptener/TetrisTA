using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    // List of avaliable blocks
    public List<GameObject> blocks;

    public bool blockPlaced = true;

    // Vars to keep track of the current and next block
    public GameObject currentBlock;
    public GameObject nextBlock;



    // Start is called before the first frame update
    void Start()
    {
        // Set a starting difficulty
        nextBlock = blocks[Random.Range(0, blocks.Count)];
        currentBlock = blocks[Random.Range(0, blocks.Count)];

    }

    // Update is called once per frame
    void Update()
    {


        if (blockPlaced)
        {
            blockPlaced = false;

            currentBlock = nextBlock;

            nextBlock = blocks[Random.Range(0, blocks.Count)];

            Instantiate(currentBlock, transform);


        }

    }
}
