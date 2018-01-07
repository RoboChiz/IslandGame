using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    public GameObject cursor;

    private WorldBuilder worldBuilder;
    private PlayerMovement playerMovement;

    private void Start()
    {
        worldBuilder = FindObjectOfType<WorldBuilder>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        //Snap Cursor to grid location
        float boxSize = worldBuilder.boxSize;

        Vector3 cursorPos = transform.position + (playerMovement.pointDirection * (boxSize + 0.2f));

        cursorPos.x = (Mathf.Floor(cursorPos.x / boxSize) * boxSize) + (boxSize / 2f);
        cursorPos.z = (Mathf.Floor(cursorPos.z / boxSize) * boxSize) + (boxSize / 2f);
        cursorPos.y = 1.25f;

        cursor.transform.position = cursorPos;

        if (InputManager.controllers.Count > 0)
        {
            InputDevice inputDevice = InputManager.controllers[0];

            if(inputDevice.GetButton("Create"))
            {
                int xChunk = (int)((cursorPos.x - worldBuilder.startPos.x) / worldBuilder.boxSize);
                int yChunk = (int)((cursorPos.y - worldBuilder.startPos.y) / worldBuilder.boxSize);
                int zChunk = (int)((cursorPos.z - worldBuilder.startPos.z) / worldBuilder.boxSize);

                Debug.Log("Create");
                if (worldBuilder.islandChunk[xChunk, yChunk, zChunk] == null)
                {
                    worldBuilder.islandChunk[xChunk, yChunk, zChunk] = new WorldBuilder.Block(1);
                    worldBuilder.LoadLevel();
                }
            }

            if (inputDevice.GetButton("Delete"))
            {
                Debug.Log("Delete");
                int xChunk = (int)((cursorPos.x - worldBuilder.startPos.x) / worldBuilder.boxSize);
                int yChunk = (int)((cursorPos.y - worldBuilder.startPos.y) / worldBuilder.boxSize);
                int zChunk = (int)((cursorPos.z - worldBuilder.startPos.z) / worldBuilder.boxSize);

                if (worldBuilder.islandChunk[xChunk, yChunk, zChunk] != null)
                {
                    worldBuilder.islandChunk[xChunk, yChunk, zChunk].m_id = 0;
                    worldBuilder.LoadLevel();
                }
            }
        }
    }
}
