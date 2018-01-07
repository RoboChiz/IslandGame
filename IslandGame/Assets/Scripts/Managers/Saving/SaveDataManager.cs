using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    private SaveData saveData;
    public Dictionary<string, string> customStringDatabase;

    private const string saveLocation = "/YogscartSaveData.gd";

    public bool resetOnStart;

    private void Start()
    {
        if(resetOnStart)
        {
            ResetSave();
        }
    }

    //------------------------------------Changeable Methods-------------------------------------
    //Reset the save to a blank state
    public void ResetSave()
    {
        Debug.Log("Resetting Save!");
        saveData = new SaveData();

        //Perform Save Resetting Here
        saveData.localVersion = SaveData.saveVersion;

        saveData.customStringKey = new string[] { };
        saveData.customStringValue = new string[] { };

        customStringDatabase = new Dictionary<string, string>();

        //Save the new Game Data
        SaveGame();
    }

    //Used for converting a save to the current version
    private void UpdateSave()
    {
        //Ensure Save Data has the correct values
        saveData.localVersion = SaveData.saveVersion;

        //Save the Game Data
        SaveGame();
    }

    //------------------------------------Non-Changeable Methods-------------------------------------
    public void Save()
    {
        //Ensure Save Data has the correct values
        saveData.localVersion = SaveData.saveVersion;

        //Update Custom String Arrays
        List<string> customStringKey = new List<string>();
        List<string> customStringValue = new List<string>();

        foreach (KeyValuePair<string,string> customString in customStringDatabase)
        {
            customStringKey.Add(customString.Key);
            customStringValue.Add(customString.Value);
        }

        saveData.customStringKey = customStringKey.ToArray();
        saveData.customStringValue = customStringValue.ToArray();

        //Save the Game Data
        SaveGame();
    }

    private void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + saveLocation);
        bf.Serialize(file, saveData);
        file.Close();
    }

    private void LoadGame()
    {
        bool saveLoaded = false;

        try
        {
            if (File.Exists(Application.persistentDataPath + saveLocation))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + saveLocation, FileMode.Open);
                saveData = (SaveData)bf.Deserialize(file);
                file.Close();

                if (saveData.localVersion != SaveData.saveVersion)
                {
                    UpdateSave();
                }

                saveLoaded = true;
            }
        }
        catch (System.Exception err)
        {
            Debug.Log("ERROR LOADING SAVE! " + err.Message);
        }

        if (!saveLoaded)
        {
            ResetSave();
        }
        else
        {
            Debug.Log("Save Loaded!");

            Debug.Log("Version:" + saveData.localVersion);

            //Reload Custom String Dictionary
            customStringDatabase = new Dictionary<string, string>();

            for(int i = 0; i < saveData.customStringKey.Length; i++)
            {
                customStringDatabase.Add(saveData.customStringKey[i], saveData.customStringValue[i]);
            }
        }
    }

    //Getters/Setters
    public string GetCustomString(string _id)
    {
        if(customStringDatabase.ContainsKey(_id))
        {
            return customStringDatabase[_id];
        }
        else
        {
            return "";
        }
    }

    public void SetCustomString(string _id, string _value)
    {
        if (customStringDatabase.ContainsKey(_id))
        {
            customStringDatabase[_id] = _value;
        }
        else
        {
            customStringDatabase.Add(_id, _value);
        }
    }

    public string FixString(string _original)
    {
        foreach(KeyValuePair<string, string> pair in customStringDatabase)
        {
            string toTest = "[" + pair.Key + "]";

            while (_original.Contains(toTest))
            {
                _original = _original.Replace(toTest, pair.Value);
            }
        }

        return _original;
    }
}
