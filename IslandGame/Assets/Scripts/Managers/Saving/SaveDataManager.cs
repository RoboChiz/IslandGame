using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveDataManager : MonoBehaviour
{
    public string saveLocation;
    public SaveData currentSaveData;

    public Dictionary<string, string> customStringDatabase;

    public bool resetOnStart;

    private IEnumerator Start()
    {
        if(resetOnStart)
        {
            yield return new WaitForEndOfFrame();
            ResetSave();
        }
    }

    //------------------------------------Changeable Methods-------------------------------------
    //Reset the save to a blank state
    public void ResetSave()
    {
        Debug.Log("Resetting Save!");
        currentSaveData = new SaveData();

        //Perform Save Resetting Here
        currentSaveData.localVersion = SaveData.saveVersion;

        currentSaveData.customStringKey = new string[] { };
        currentSaveData.customStringValue = new string[] { };

        customStringDatabase = new Dictionary<string, string>();

        //Save the new Game Data
       // SaveGame();
    }

    //Saving for this specific game (Performed Last)
    private void GameSpecificSave(BinaryWriter bw)
    {
        ISavingManager[] managers = FindObjectsOfType<ISavingManager>();
        bw.Write(managers.Length);

        foreach (ISavingManager savingManager in managers)
        {
            //Save UniqueID so we can identify this manager when loading
            bw.Write(savingManager.uniqueID);
            savingManager.DoSave(bw);
        }
    }

    //Loading for this specific game (Performed Last)
    private void GameSpecificLoad(int _version, BinaryReader br)
    {
        ISavingManager[] managers = FindObjectsOfType<ISavingManager>();

        int managersSize = br.ReadInt32();
        for(int i = 0; i < managersSize; i++)
        {
            //Load Unique ID
            char[] uniqueID = br.ReadChars(4);

            foreach (ISavingManager savingManager in managers)
            {
                bool isTheSame = true;
                for(int j = 0; j < 4; j++)
                {
                    if(savingManager.uniqueID[j] != uniqueID[j])
                    {
                        isTheSame = false;
                        break;
                    }
                }

                if(isTheSame)
                {
                    savingManager.DoLoad(_version, br);
                }
            }
        }
    }

    //Used for converting a save to the current version
    private void UpdateSave(string _location)
    {
        //Ensure Save Data has the correct values
        currentSaveData.localVersion = SaveData.saveVersion;

        //Save the Game Data
        SaveGame(_location);
    }

    public void LoadLevelSave(string _level, string _saveLocation)
    {
        StartCoroutine(ActualLoadLevel(_level, _saveLocation));
    }

    private IEnumerator ActualLoadLevel(string _level, string _saveLocation)
    {
        yield return SceneManager.LoadSceneAsync(_level);

        yield return null;

        FindObjectOfType<SaveDataManager>().Load(_saveLocation);
    }

    //----------------------------------Changeable Getters/Setters-----------------------------------

    //-----------------------------------------Useful Methods----------------------------------------

    public static void SaveVector3(BinaryWriter _stream, Vector3 _vector)
    {
        _stream.Write(_vector.x);
        _stream.Write(_vector.y);
        _stream.Write(_vector.z);
    }

    public static Vector3 LoadVector3(BinaryReader _stream)
    {
        return new Vector3(_stream.ReadSingle(), _stream.ReadSingle(), _stream.ReadSingle());
    }

    //------------------------------------Non-Changeable Methods-------------------------------------
    public void Save(string _location)
    {
        //Ensure Save Data has the correct values
        currentSaveData.localVersion = SaveData.saveVersion;

        //Update Custom String Arrays
        List<string> customStringKey = new List<string>();
        List<string> customStringValue = new List<string>();

        foreach (KeyValuePair<string,string> customString in customStringDatabase)
        {
            customStringKey.Add(customString.Key);
            customStringValue.Add(customString.Value);
        }

        currentSaveData.customStringKey = customStringKey.ToArray();
        currentSaveData.customStringValue = customStringValue.ToArray();

        //Save the Game Data
        SaveGame(_location);
    }

    private void SaveGame(string _location)
    {
        saveLocation = _location;
        BinaryWriter bw = new BinaryWriter(File.Create(saveLocation));

        //Load Save Data
        currentSaveData.SaveAllData(bw);

        //Load Game Specific Managers
        GameSpecificSave(bw);

        bw.Close();

        Debug.Log("Game Saved!");
    }

    public void Load(string _location)
    {
        saveLocation = _location;
        bool saveLoaded = false;
        BinaryReader br = null;

        try
        {
            if (File.Exists(saveLocation))
            {
                br = new BinaryReader(File.Open(saveLocation, FileMode.Open));

                int version = br.ReadInt32();

                //Load Save Data
                currentSaveData.LoadAllData(version, br);

                //Load Game Specific Managers
                GameSpecificLoad(version, br);               

                if (currentSaveData.localVersion != SaveData.saveVersion)
                {
                    UpdateSave(_location);
                }

                saveLoaded = true;
            }
        }
        catch (System.Exception err)
        {
            Debug.Log("ERROR LOADING SAVE! " + err.Message);
        }
        finally
        {
            br.Close();
        }

        if (!saveLoaded)
        {
            ResetSave();
        }
        else
        {
            Debug.Log("Save Loaded!");

            Debug.Log("Version:" + currentSaveData.localVersion);

            //Reload Custom String Dictionary
            customStringDatabase = new Dictionary<string, string>();

            for(int i = 0; i < currentSaveData.customStringKey.Length; i++)
            {
                customStringDatabase.Add(currentSaveData.customStringKey[i], currentSaveData.customStringValue[i]);
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
