using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WorldStateManager : ISavingManager
{
    override public char[] uniqueID { get { return new char[4] { 'W', 'S', 'M', '_' }; } }

    //Player Data
    public Transform player;

    public override void DoSave(BinaryWriter _stream)
    {
        //Update Player Stats
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();

        //Save Player Stats
        SaveDataManager.SaveVector3(_stream, player.position);
        SaveDataManager.SaveVector3(_stream, player.rotation.eulerAngles);
        SaveDataManager.SaveVector3(_stream, playerMovement.lastDirection);
        SaveDataManager.SaveVector3(_stream, playerMovement.pointDirection);
        _stream.Write(playerMovement.expectedSpeed);
    }

    public override void DoLoad(int _version, BinaryReader _stream)
    {
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();

        //Load Player Stats
        Vector3 playerPosition = SaveDataManager.LoadVector3(_stream);
        player.transform.position = playerPosition;

        Vector3 playerRotation = SaveDataManager.LoadVector3(_stream);
        player.transform.rotation = Quaternion.Euler(playerRotation);

        Vector3 lastDirection = SaveDataManager.LoadVector3(_stream);
        Vector3 pointDirection = SaveDataManager.LoadVector3(_stream);
        float expectedSpeed = _stream.ReadSingle();

        playerMovement.GetFromLoad(lastDirection, pointDirection, expectedSpeed);
    }

}