using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveScreen : MonoBehaviour
{
    public GameObject saveTemplate, pauseMenu;

    struct SaveSlot
    {
        public string location;
        public GameObject gameObject;
    }

    private List<SaveSlot> saveSlots;
    private SaveDataManager saveDataManager;

    private List<string> validFiles;
    public InputField inputField;

    private bool lockSlots = false;

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
        validFiles = new List<string>();

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

                                slot.gameObject.transform.parent = saveTemplate.transform.parent;

                                RectTransform rectTransform = slot.gameObject.GetComponent<RectTransform>();
                                rectTransform.anchoredPosition = saveTemplate.GetComponent<RectTransform>().anchoredPosition - new Vector2(0, (rectTransform.sizeDelta.y + 10) * (saveSlots.Count - 1));

                                Text text = slot.gameObject.GetComponentInChildren<Text>();
                                string[] fileNameSplit = saveFile.Split('\\');

                                text.text = fileNameSplit[fileNameSplit.Length - 1].Remove(fileNameSplit[fileNameSplit.Length - 1].Length - 3);

                                int count = validFiles.Count;
                                slot.gameObject.transform.Find("Save_Button").GetComponent<Button>().onClick.AddListener(delegate () { OnOverwriteSlot(count); } );
                                slot.gameObject.transform.Find("Load_Button").GetComponent<Button>().onClick.AddListener(delegate () { OnLoadSlot(count); }); 
                                slot.gameObject.transform.Find("Delete_Button").GetComponent<Button>().onClick.AddListener(delegate () { OnDeleteSlot(count); });

                                Debug.Log("validFiles.Count:" + validFiles.Count);

                                Debug.Log("Loaded Save:" + text.text);
                                validFiles.Add(saveFile);
                            }
                        }
                    }
                }
            }
        }

        saveTemplate.SetActive(false);
    }

    public void OnOverwriteSlot(int _slot)
    {
        if (!lockSlots)
        {
            if (File.Exists(validFiles[_slot]))
            {
                saveDataManager.Save(validFiles[_slot]);
            }
            UpdateSaveList();
        }
    }

    public void OnLoadSlot(int _slot)
    {
        if (!lockSlots)
        {
            lockSlots = true;
            if (File.Exists(validFiles[_slot]))
            {
                saveDataManager.Load(validFiles[_slot]);
            }

            lockSlots = false;

            UpdateSaveList();
        }
    }

    public void OnDeleteSlot(int _slot)
    {
        if (!lockSlots)
        {
            if (File.Exists(validFiles[_slot]))
            {
                File.Delete(validFiles[_slot]);
            }

            UpdateSaveList();
        }
    }

    public void SaveToNewSlot()
    {
        if (!lockSlots)
        {
            if (inputField.text != null && inputField.text != "")
            {
                string validCharacters = "abcdefghijklmnopqrstuvwxyz0123456789 _-";
                bool valid = true;

                foreach (char character in inputField.text)
                {
                    if (!validCharacters.Contains(character.ToString().ToLower()))
                    {
                        valid = false;
                    }
                }

                if (valid)
                {
                    saveDataManager.Save(Application.persistentDataPath + "\\" + inputField.text + ".gd");
                    UpdateSaveList();
                }
            }
        }
    }

    public void ReturnToPause()
    {
        pauseMenu.SetActive(true);
        gameObject.SetActive(false);
    }
	
}
