using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Goal: Keep track of blocks, line clears and points
public class BlockManager : MonoBehaviour
{

    public bool[,] ifSolidBlocks = new bool[10,20];
    public GameObject[,] solidBlocks = new GameObject[10, 20];
    public int score = 0;
    public Text scoreText;

    // Adds a block to the solid block data structure
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
        // Update score
        score += CheckClear();
        scoreText.text = "Lines Cleared: " + score;
        

        Destroy(block.gameObject, 1f);
     
    }


    // Checks if there is a block in the top row, and restarts the scene if there is
    private void CheckGameOver()
    {
        for (int x = 0; x < ifSolidBlocks.GetLength(0); x++)
        {
            if (ifSolidBlocks[x, 0])
            {
                Debug.Log("Game Over");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    private int CheckClear()
    {
        int rowsCleared = 0;
        for (int y = 0; y < ifSolidBlocks.GetLength(1); y++)
        {
            bool rowCleared = true;
            for (int x = 0; x < ifSolidBlocks.GetLength(0); x++)
            {
                if (!ifSolidBlocks[x,y])
                {
                    rowCleared = false;
                }
            }

            if (rowCleared)
            {
                Debug.Log("Clearing row: " + y);
                ClearRow(y);
                rowsCleared += 1;
            }
        }

        CheckGameOver();

        return rowsCleared;
    }

    private void ClearRow(int row)
    {
        for (int x = 0; x < ifSolidBlocks.GetLength(0); x++)
        {
            ifSolidBlocks[x, row] = false;
        }
        for (int x = 0; x < solidBlocks.GetLength(0); x++)
        {
            Destroy(solidBlocks[x, row]);
            //solidBlocks[x, row] = null;
        }

        // Loop backwards though the rows starting at the one that was just cleared
        for (int y = row; y > 1; y--)
        {
            for (int x = 0; x < ifSolidBlocks.GetLength(0); x++)
            {
                ifSolidBlocks[x, y] = ifSolidBlocks[x, y-1];
                ifSolidBlocks[x, y - 1] = false;
            }

            for (int x = 0; x < solidBlocks.GetLength(0); x++)
            {
                // If there is a block above to move, move it down and update the data structure
                if (solidBlocks[x, y-1])
                {
                    solidBlocks[x, y-1].transform.position += new Vector3(0, -1, 0);
                    solidBlocks[x, y] = solidBlocks[x, y - 1];
                }
                
            }

        }


    }

}
