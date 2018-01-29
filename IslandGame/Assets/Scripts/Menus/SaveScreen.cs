using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveScreen : MonoBehaviour
{
    public GameObject saveTemplate;

    private List<GameObject> saveSlots;
    private SaveDataManager saveDataManager;


	// Use this for initialization
	void Start ()
    {
        saveDataManager = FindObjectOfType<SaveDataManager>();

    }

    void UpdateSaveList()
    {
        /*if(saveSlots != null)
        {
            foreach (GameObject save in saveSlots)
                Destroy(save);
        }

        saveSlots = new List<GameObject>();

        string[] allSaveFiles = Directory.GetFiles(Application.persistentDataPath);

        if (File.Exists(Application.persistentDataPath + saveLocation))
        {
            BinaryReader br = new BinaryReader(File.Open(Application.persistentDataPath + saveLocation, FileMode.Open));

            int version = br.ReadInt32();

            //Load Save Data
            saveData.LoadAllData(version, br);

            //Load Game Specific Managers
            GameSpecificLoad(version, br);

            br.Close();

            if (saveData.localVersion != SaveData.saveVersion)
            {
                UpdateSave();
            }

            saveLoaded = true;
        }*/
    }
	
}
