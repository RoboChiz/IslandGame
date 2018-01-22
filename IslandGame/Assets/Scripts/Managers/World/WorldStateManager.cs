using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WorldStateManager : ISavingManager
{
    override public char[] uniqueID { get { return new char[4] { 'W', 'S', 'M', '_' }; } }

    //Player Data
    public Transform player;

    //Word Data
    private List<WorldChunk> worldChunks;

    public void Start()
    {
        worldChunks = new List<WorldChunk>();
        worldChunks.Add(new WorldChunk(worldChunks.Count, new Vector3(0f, 0f, 0f)));
    }

    public override void DoSave(BinaryWriter _stream)
    {
        //Update Player Stats
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        CameraSwap cameraSwap = GetComponent<CameraSwap>();

        //Save Player Stats
        SaveDataManager.SaveVector3(_stream, player.position);
        SaveDataManager.SaveVector3(_stream, player.rotation.eulerAngles);
        SaveDataManager.SaveVector3(_stream, playerMovement.lastDirection);
        SaveDataManager.SaveVector3(_stream, playerMovement.pointDirection);
        SaveDataManager.SaveVector3(_stream, playerMovement.lastCameraQuat.eulerAngles);
        _stream.Write(playerMovement.expectedSpeed);

        //Save Camera Stats
        _stream.Write(cameraSwap.GetCameraState());
        _stream.Write(cameraSwap.GetInvertHorizontal());
        _stream.Write(cameraSwap.GetInvertVertical());

        //Save World Chunks
        _stream.Write(worldChunks.Count);

        for(int i = 0; i < worldChunks.Count; i++)
        {
            WorldChunk chunk = worldChunks[i];
            SaveDataManager.SaveVector3(_stream, chunk.centrePoint);
            //SaveDataManager.SaveVector3(_stream, chunk.Size());

            for (int x = 0; x < chunk.gridData.GetLength(0); x++)
            {
                for (int y = 0; y < chunk.gridData.GetLength(1); y++)
                {
                    for (int z = 0; z < chunk.gridData.GetLength(2); z++)
                    {
                        _stream.Write(chunk.gridData[x,y,z]);
                        _stream.Write(chunk.gridRotationAngles[x, y, z]);

                        if (chunk.gridData[x, y, z] != 0)
                        {
                            Debug.Log("Saved Block at " + x + "," + y + "," + z);
                        }
                    }
                }
            }
        }

    }

    public override void DoLoad(int _version, BinaryReader _stream)
    {
        //Clear Existing Chunks
        foreach(WorldChunk chunk in worldChunks)
        {
            for (int x = 0; x < chunk.gridData.GetLength(0); x++)
            {
                for (int y = 0; y < chunk.gridData.GetLength(1); y++)
                {
                    for (int z = 0; z < chunk.gridData.GetLength(2); z++)
                    {
                       if(chunk.gridObjects[x,y,z] != null)
                        {
                            Destroy(chunk.gridObjects[x, y, z]);
                        }
                    }
                }
            }
        }

        worldChunks = new List<WorldChunk>();

        //Get Components
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        CameraSwap cameraSwap = GetComponent<CameraSwap>();

        //Load Player Stats
        Vector3 playerPosition = SaveDataManager.LoadVector3(_stream);
        player.transform.position = playerPosition;

        Vector3 playerRotation = SaveDataManager.LoadVector3(_stream);
        player.transform.rotation = Quaternion.Euler(playerRotation);

        Vector3 lastDirection = SaveDataManager.LoadVector3(_stream);
        Vector3 pointDirection = SaveDataManager.LoadVector3(_stream);
        Vector3 lastCameraQuat = SaveDataManager.LoadVector3(_stream);
        float expectedSpeed = _stream.ReadSingle();

        playerMovement.GetFromLoad(lastDirection, pointDirection, expectedSpeed, lastCameraQuat);

        //Load Camera Stats
        cameraSwap.SetCameraState(_stream.ReadBoolean());
        cameraSwap.SetInvertHorizontal(_stream.ReadBoolean());
        cameraSwap.SetInvertVertical(_stream.ReadBoolean());

        //Load World Chunks
        int worldChunkCount = _stream.ReadInt32();

        for(int i = 0; i < worldChunkCount; i++)
        {
            worldChunks.Add(new WorldChunk(i, SaveDataManager.LoadVector3(_stream)));

            WorldChunk chunk = worldChunks[worldChunks.Count - 1];

            //Load the Data
            for (int x = 0; x < chunk.gridData.GetLength(0); x++)
            {
                for (int y = 0; y < chunk.gridData.GetLength(1); y++)
                {
                    for (int z = 0; z < chunk.gridData.GetLength(2); z++)
                    {
                        int _id = _stream.ReadInt32();
                        float _rotation = _stream.ReadSingle();

                        if (_id != 0)
                        {
                            Debug.Log("Loaded Block at " + x + "," + y + "," + z);
                            CreateItem(_id, ChunkToWorld(chunk, new Vector3(x, y, z)), _rotation);                        
                        }
                    }
                }
            }
        }

    }

    public WorldChunk IsInsideWorldChunk(Vector3 _position)
    {
        foreach(WorldChunk worldChunk in worldChunks)
        {
            if(worldChunk.IsInsideChunk(_position))
            {
                return worldChunk;
            }
        }

        return null;
    }

    public void CreateItem(int _databaseID, Vector3 _position, float _rotation)
    {
        WorldChunk insideChunk = IsInsideWorldChunk(_position);

        if (insideChunk != null)
        {
            Vector3 chunkPos = WorldToChunk(insideChunk, _position);

            Debug.Log("Created Block at " + chunkPos.x + "," + chunkPos.y + "," + chunkPos.z);

            if (insideChunk.gridData[(int)chunkPos.x, (int)chunkPos.y, (int)chunkPos.z] == 0)
            {
                insideChunk.gridData[(int)chunkPos.x, (int)chunkPos.y, (int)chunkPos.z] = _databaseID;

                GameObject prefab = Instantiate(FindObjectOfType<BuildingPartDatabaseManager>().GetBuildingPart(_databaseID).prefab);
                prefab.transform.position = _position;
                prefab.transform.rotation = Quaternion.AngleAxis(_rotation, Vector3.up);

                insideChunk.gridObjects[(int)chunkPos.x, (int)chunkPos.y, (int)chunkPos.z] = prefab;
                insideChunk.gridRotationAngles[(int)chunkPos.x, (int)chunkPos.y, (int)chunkPos.z] = _rotation;
            }
        }
    }

    public void DeleteItem(Vector3 _position)
    {
        WorldChunk insideChunk = IsInsideWorldChunk(_position);

        if (insideChunk != null)
        {
            Vector3 chunkPos = WorldToChunk(insideChunk, _position);
            int xChunk = (int)chunkPos.x;
            int yChunk = (int)chunkPos.y;
            int zChunk = (int)chunkPos.z;

            if (insideChunk.gridData[xChunk, yChunk, zChunk] != 0)
            {
                insideChunk.gridData[xChunk, yChunk, zChunk] = 0;
                insideChunk.gridRotationAngles[xChunk, yChunk, zChunk] = 0;
                Debug.Log("Deleted Block at " + xChunk + "," + yChunk + "," + zChunk);

                Destroy(insideChunk.gridObjects[xChunk, yChunk, zChunk]);
            }
        }
    }

    public Vector3 ChunkToWorld(WorldChunk _chunk, Vector3 _pos)
    {
        //Check that position is an int
        if(_pos.x % 1 != 0)
        {
            Debug.LogError("X is not an int");
        }

        if (_pos.y % 1 != 0)
        {
            Debug.LogError("Y is not an int");
        }

        if (_pos.z % 1 != 0)
        {
            Debug.LogError("Z is not an int");
        }

        int gridWidth = (int)_chunk.Size().x, gridHeight = (int)_chunk.Size().y, gridLength = (int)_chunk.Size().z;
        float halfWidth = gridWidth / 2f, halfLength = gridLength / 2f;

        Vector3 bottomEdge  = _chunk.centrePoint - new Vector3((halfWidth - 0.5f), 0f, (halfLength - 0.5f));

        return bottomEdge + _pos;
    }

    public Vector3 WorldToChunk(WorldChunk _chunk, Vector3 _pos)
    {
        Vector3 local = _pos - _chunk.centrePoint;
        local += new Vector3(_chunk.Size().x / 2f, 0f, _chunk.Size().z / 2f);

        return new Vector3(Mathf.Floor(local.x), Mathf.Floor(Mathf.Clamp(local.y, 0.5f, 10.5f)), Mathf.Floor(local.z));
    }
}

public class WorldChunk
{
    public static readonly int[,,] gridSize = new int[20, 10, 20];

    public int ID;
    public Vector3 centrePoint; //Should be a multiple of twenty

    public int[,,] gridData;
    public GameObject[,,] gridObjects;
    public float[,,] gridRotationAngles;

    public Vector3 Size()
    {
        return new Vector3(gridSize.GetLength(0), gridSize.GetLength(1), gridSize.GetLength(2));
    }

    public WorldChunk(int _id, Vector3 _centrePoint)
    {
        ID = _id;

        gridData = new int[20, 10, 20];
        gridObjects = new GameObject[20, 10, 20];
        gridRotationAngles = new float[20, 10, 20];

        centrePoint = _centrePoint;
    }

    public bool IsInsideChunk(Vector3 _position)
    {
        Vector3 size = Size(), halfSize = Size() / 2f;

        if (_position.x >= centrePoint.x - halfSize.x && _position.x < centrePoint.x + halfSize.x &&
             _position.y >= 0f && _position.y < size.y &&
             _position.z >= centrePoint.z - halfSize.z && _position.z < centrePoint.z + halfSize.z)
        {
            return true;
        }

        return false;
    }
}