using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveScreen : MonoBehaviour
{
    public GameObject saveTemplate;

    struct SaveSlot
    {
        public string location;
        public GameObject gameObject;
    }

    private List<SaveSlot> saveSlots;
    private SaveDataManager saveDataManager;

	// Use this for initialization
	void Start ()
    {
        saveDataManager = FindObjectOfType<SaveDataManager>();

        UpdateSaveList();
    }

    void UpdateSaveList()
    {
        if(saveSlots != null)
        {
            foreach (SaveSlot save in saveSlots)
                Destroy(save.gameObject);
        }

        saveTemplate.SetActive(true);
        saveSlots = new List<SaveSlot>();

        string[] allSaveFiles = Directory.GetFiles(Application.persistentDataPath);

        foreach (string saveFile in allSaveFiles)
        {
            string[] extensionCheck = saveFile.Split('.');

            if (extensionCheck[extensionCheck.Length - 1] == "gd")
            {
                if (File.Exists(saveFile))
                {
                    BinaryReader br = new BinaryReader(File.Open(saveFile, FileMode.Open));
                    SaveData tempSaveData = new SaveData();

                    int version = br.ReadInt32();

                    if (version <= SaveData.saveVersion)
                    {
                        bool allowSave = false;
                        //Load Save Data
                        try
                        {
                            tempSaveData.LoadAllData(version, br);
                            allowSave = true;
                        }
                        catch
                        {
                            Debug.Log("Dodgy Save");
                        }
                        finally
                        {
                            br.Close();

                            if (allowSave)
                            {
                                SaveSlot slot;
                                slot.location = saveFile;
                                slot.gameObject = Instantiate(saveTemplate, transform);
                                saveSlots.Add(slot);

                                RectTransform rectTransform = slot.gameObject.GetComponent<RectTransform>();
                                rectTransform.anchoredPosition = saveTemplate.GetComponent<RectTransform>().anchoredPosition - new Vector2(0, (rectTransform.sizeDelta.y + 10) * (saveSlots.Count - 1));

                                Text text = slot.gameObject.GetComponentInChildren<Text>();
                                string[] fileNameSplit = saveFile.Split('\\');

                                text.text = fileNameSplit[fileNameSplit.Length - 1].Remove(fileNameSplit[fileNameSplit.Length - 1].Length - 3);

                                Debug.Log("Loaded Save:" + text.text);
                            }
                        }
                    }
                }
            }
        }

        saveTemplate.SetActive(false);
    }
	
}
