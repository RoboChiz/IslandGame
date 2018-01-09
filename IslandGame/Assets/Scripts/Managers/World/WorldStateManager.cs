using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldStateManager : MonoBehaviour
{
    private WorldState worldState;

    public Transform player;

    private void Start()
    {
        worldState = new WorldState();
    }

    private void UpdateWorldState ()
    {
        //Player Location
        worldState.playerLocationX = player.transform.position.x;
        worldState.playerLocationY = player.transform.position.y;
        worldState.playerLocationZ = player.transform.position.z;

        worldState.playerRotationX = player.transform.rotation.eulerAngles.x;
        worldState.playerRotationY = player.transform.rotation.eulerAngles.y;
        worldState.playerRotationZ = player.transform.rotation.eulerAngles.z;
    }

    private void UpdateFromWorldState()
    {
        //Player Location
        player.transform.position = new Vector3(worldState.playerLocationX, worldState.playerLocationY, worldState.playerLocationZ);
        player.transform.rotation = Quaternion.Euler(new Vector3(worldState.playerRotationX, worldState.playerRotationY, worldState.playerRotationZ));

        //Disable any Player Velocity
        Rigidbody playerRigidbody = player.GetComponent<Rigidbody>();
        playerRigidbody.velocity = Vector3.zero;
    }

    public void SaveWorldState(SaveDataManager _saveDataManager)
    {
        //Make sure values are up to date
        UpdateWorldState();

        //Send the World State to the Save Data Manager
        _saveDataManager.SetWorldState(worldState);
    }

    public void LoadWorldState(SaveDataManager _saveDataManager)
    {
        //Send the World State to the Save Data Manager
        worldState = _saveDataManager.GetWorldState();

        //Copy values from save to this
        UpdateFromWorldState();
    }
}