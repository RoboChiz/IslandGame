using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    //V1 - Inital Setup
    public const int saveVersion = 1;

    /// <summary>
    /// NOTE!
    /// Always add new values to the bottom to avoid breaking old saves.
    /// </summary>

    // Which save version is this
    public int localVersion;

    //Used to store player specific text
    public string[] customStringKey;
    public string[] customStringValue;

    public WorldState worldState;

}
