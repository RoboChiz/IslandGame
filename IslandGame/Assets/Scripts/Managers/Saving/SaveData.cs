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

    public void SaveAllData(BinaryWriter stream)
    {
        //Save the version
        stream.Write(saveVersion);

        //Save Custom String Keys
        stream.Write(customStringKey.Length);
        for(int i = 0; i < customStringKey.Length; i++)
        {
            stream.Write(customStringKey[i]);
        }

        //Save Custom String Values
        stream.Write(customStringValue.Length);
        for (int i = 0; i < customStringValue.Length; i++)
        {
            stream.Write(customStringValue[i]);
        }
    }

    public void LoadAllData(int _version, BinaryReader stream)
    {
        localVersion = _version;

        //Read Custom String Keys
        int customStringKeySize = stream.ReadInt32();
        customStringKey = new string[customStringKeySize];

        for (int i = 0; i < customStringKeySize; i++)
        {
            customStringKey[i] = stream.ReadString();
        }

        //Read Custom String Values
        int customStringValueSize = stream.ReadInt32();
        customStringValue = new string[customStringValueSize];

        for (int i = 0; i < customStringValueSize; i++)
        {
            customStringValue[i] = stream.ReadString();
        }
    }

}
