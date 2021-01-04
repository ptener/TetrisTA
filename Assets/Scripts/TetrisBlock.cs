using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{

    public bool currentBlock = true;
    public BlockSpawner blockspawner;
    private Transform pivotPoint;

    public float currentDelay;
    private float totalDelay = 1.5f;


    // Start is called before the first frame update
    void Start()
    {
        blockspawner = GameObject.Find("BlockSpawner").GetComponent<BlockSpawner>();
        currentDelay = totalDelay;
        pivotPoint = transform.Find("PivotPoint").transform;
    }

    private void Update()
    {
        if (currentBlock)
        {
            // Right and left
            if (Input.GetButtonDown("Horizontal"))
            {
                if (Input.GetAxis("Horizontal") > 0)
                {
                    transform.position += new Vector3(1, 0, 0);
                    currentDelay = totalDelay;
                }
                if (Input.GetAxis("Horizontal") < 0)
                {
                    transform.position += new Vector3(-1, 0, 0);
                    currentDelay = totalDelay;
                }
            }
            // Up and down
            if (Input.GetButtonDown("Vertical"))
            {
                if (Input.GetAxis("Vertical") > 0)
                {
                    // Rotate
                }
                if (Input.GetAxis("Vertical") < 0)
                {
                    transform.position += new Vector3(0, -1, 0);
                }
            }

            // Rotate (Jump for space bar)
            if (Input.GetButtonDown("Jump"))
            {
                transform.RotateAround(pivotPoint.position, Vector3.forward, 90);
                currentDelay = totalDelay;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        currentDelay = totalDelay;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (currentBlock)
        {

            currentDelay -= Time.deltaTime;

            if (currentDelay < 0)
            {
                if (!blockspawner.blockPlaced)
                {
                    transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z));
                    GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                    blockspawner.blockPlaced = true;
                    currentBlock = false;
                }
            }
            
        }
        
    }

}
