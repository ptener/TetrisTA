using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{

    public bool currentBlock = true;
    private BlockSpawner blockSpawner;
    private BlockManager blockManager;
    private Transform pivotPoint;

    // Timer vars
    public float currentDelay;
    private float totalDelay = 1f;
    public float fallSpeed = 1;
    private float fallDelay;

    // Collision layers
    public LayerMask solidLayer;

    // Start is called before the first frame update
    void Start()
    {
        // Get references
        blockSpawner = GameObject.Find("BlockSpawner").GetComponent<BlockSpawner>();
        blockManager = GameObject.Find("BlockManager").GetComponent<BlockManager>();
        pivotPoint = transform.Find("PivotPoint").transform;

        // Set delays
        currentDelay = totalDelay;
        fallDelay = fallSpeed;

    }

    private void Update()
    {
        if (currentBlock)
        {

            // Right and left
            if (Input.GetButton("Horizontal"))
            {
                if (!BlockRight())
                {
                    if (Input.GetAxis("Horizontal") > 0)
                    {
                        transform.position += new Vector3(1, 0, 0);
                        currentDelay = totalDelay;
                    }
                }
                if (!BlockLeft())
                {
                    if (Input.GetAxis("Horizontal") < 0)
                    {
                        transform.position += new Vector3(-1, 0, 0);
                        currentDelay = totalDelay;
                    }
                }
                
            }
            // Rotate (Jump for space bar)
            if (Input.GetButtonDown("Jump"))
            {
                // Check if can rotate
                transform.RotateAround(pivotPoint.position, Vector3.forward, 90);
                currentDelay = totalDelay;
            }

            if (!BlockBeneath())
            {
                // Up and down
                if (Input.GetButton("Vertical"))
                {
                    if (Input.GetAxis("Vertical") > 0)
                    {
                        // Press up
                    }
                    if (Input.GetAxis("Vertical") < 0)
                    {
                        transform.position += new Vector3(0, -1, 0);
                    }
                }


                fallDelay -= Time.deltaTime;

                if (fallDelay < 0)
                {
                    fallDelay = fallSpeed;

                    transform.position += new Vector3(0, -1, 0);
                }
            }
            else
            {
                currentDelay -= Time.deltaTime;

                if (currentDelay < 0)
                {
                    if (!blockSpawner.blockPlaced)
                    {
                        // Make sure block is aligned properly
                        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z));

                        blockManager.AddSolidBlock(transform);

                        currentBlock = false;

                        // Ready the block spawner to send next block
                        blockSpawner.blockPlaced = true;
                    }
                }
            }

            
        }
    }

    // Check if there are solid blocks beneath
    private bool BlockBeneath()
    {

        foreach(Transform child in transform)
        {
            if (child.name.Contains("Tetris"))
            {
                RaycastHit2D hit = Physics2D.Raycast(child.position, Vector2.down, 0.5f, solidLayer);

                if (hit.collider != null)
                {
                    return true;
                }
            }
        }

        return false;
    }
    // Check if there are solid blocks to the left
    private bool BlockLeft()
    {

        foreach (Transform child in transform)
        {
            if (child.name.Contains("Tetris"))
            {
                RaycastHit2D hit = Physics2D.Raycast(child.position, Vector2.left, 0.5f, solidLayer);

                if (hit.collider != null)
                {
                    return true;
                }
            }
        }

        return false;
    }
    // Check if there are solid blocks to the right
    private bool BlockRight()
    {

        foreach (Transform child in transform)
        {
            if (child.name.Contains("Tetris"))
            {
                RaycastHit2D hit = Physics2D.Raycast(child.position, Vector2.right, 0.5f, solidLayer);

                if (hit.collider != null)
                {
                    return true;
                }
            }
        }

        return false;
    }

}
