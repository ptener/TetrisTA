using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Goal: Keep track of blocks, line clears and points
public class BlockManager : MonoBehaviour
{

    public bool[,] ifSolidBlocks = new bool[10,20];
    public GameObject[,] solidBlocks = new GameObject[10, 20];

    private void Update()
    {

    }

    // 
    public void AddSolidBlock(Transform block)
    {

        for (int i = block.childCount - 1; i >= 0; --i)
        {
            Transform child = block.GetChild(i);
            if (child.name.Contains("Tetris"))
            {
                child.SetParent(transform);

                child.gameObject.layer = 8;

                int x = (int)child.transform.localPosition.x;
                int y = (int)Mathf.Abs(child.transform.localPosition.y);

                ifSolidBlocks[x, y] = true;
                solidBlocks[x, y] = child.gameObject;

            }
        }
        CheckClear();

        Destroy(block.gameObject, 1f);
     
    }

    private int CheckClear()
    {
        int rowsCleared = 0;
        for (int x = 0; x < ifSolidBlocks.GetLength(0); x++)
        {
            bool rowCleared = true;
            for (int y = 0; y < ifSolidBlocks.GetLength(1); y++)
            {

                if (!ifSolidBlocks[x,y])
                {
                    rowCleared = false;
                }
                else
                {
                    //Debug.Log("x: "+x+"  y: "+y);
                }
            }
            Debug.Log(rowCleared);

            if (rowCleared)
            {
                Debug.Log("Clearing row: " + x);

                ClearRow(x);
                rowsCleared += 1;
            }
        }
        return rowsCleared;
    }

    private void ClearRow(int row)
    {
        for (int y = 0; y < ifSolidBlocks.GetLength(1); y++)
        {
            ifSolidBlocks[row, y] = false;
        }
        for (int y = 0; y < solidBlocks.GetLength(1); y++)
        {
            Destroy(solidBlocks[row, y]);
        }

    }

}
