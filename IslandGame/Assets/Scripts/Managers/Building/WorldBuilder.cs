using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBuilder : MonoBehaviour
{
    public Block[,,] islandChunk;
    public GameObject box;
    public Vector3 startPos;
    public float boxSize = 1f;

	// Use this for initialization
	void Start ()
    {
        islandChunk = new Block[20, 1, 20];

        islandChunk[0, 0, 0] = new Block(1);
        islandChunk[19, 0, 0] = new Block(1);
        islandChunk[0, 0, 19] = new Block(1);
        islandChunk[19, 0, 19] = new Block(1);

        islandChunk[0, islandChunk.GetLength(1) - 1, 0] = new Block(1);
        islandChunk[19, islandChunk.GetLength(1) - 1, 0] = new Block(1);
        islandChunk[0, islandChunk.GetLength(1) - 1, 19] = new Block(1);
        islandChunk[19, islandChunk.GetLength(1) - 1, 19] = new Block(1);

        LoadLevel();
    }

    public void LoadLevel()
    {
        for(int x = 0; x < islandChunk.GetLength(0); x++)
        {
            for (int y = 0; y < islandChunk.GetLength(1); y++)
            {
                for (int z = 0; z < islandChunk.GetLength(2); z++)
                {
                    Block block = islandChunk[x, y, z];

                    bool blockNeedsCreating = false, blockNeedsDeleting = false;

                    if (block != null)
                    {
                        if (islandChunk[x, y, z].m_id == 1)
                        {
                            if (islandChunk[x, y, z].m_gameOject == null)
                            {
                                blockNeedsCreating = true;                               
                            }
                        }
                        else
                        {
                            blockNeedsDeleting = true;
                        }
                    }
                    else
                    {
                        blockNeedsDeleting = true;
                    }

                    if (block != null)
                    {
                        if (blockNeedsCreating)
                        {
                            block.m_gameOject = Instantiate(box, startPos + new Vector3(boxSize * x, boxSize * y, boxSize * z), Quaternion.identity);
                        }

                        if (blockNeedsDeleting)
                        {
                            if (block.m_gameOject != null)
                            {
                                Destroy(block.m_gameOject);
                            }

                            islandChunk[x, y, z] = null;
                        }
                    }
                }
            }
        }
    }

    public class Block
    {
        public int m_id;
        public GameObject m_gameOject;

        public Block(int _id)
        {
            m_id = _id;
            m_gameOject = null;
        }
    }
}
