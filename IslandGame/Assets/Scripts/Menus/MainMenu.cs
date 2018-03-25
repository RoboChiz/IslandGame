using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu, loadScreen, loadButtonPrefab, scrollView;
    private List<string> validFiles;

    public UIConnection backButton;

    struct SaveSlot
    {
        public string location;
        public GameObject gameObject;
    }
    private List<SaveSlot> saveSlots;

    public void NewGame()
    {
        SceneManager.LoadScene("Gameplay_Test");
    }

    public void LoadGame()
    {
        mainMenu.SetActive(false);
        loadScreen.SetActive(true);
        UpdateSaveList();
    }

    public void Options()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadScreen_Back()
    {
        mainMenu.SetActive(true);
        loadScreen.SetActive(false);
    }

    void UpdateSaveList()
    {
        if (saveSlots != null)
        {
            foreach (SaveSlot save in saveSlots)
                Destroy(save.gameObject);
        }

        loadButtonPrefab.SetActive(true);
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
                                slot.gameObject = Instantiate(loadButtonPrefab, transform);
                                saveSlots.Add(slot);

                                slot.gameObject.transform.parent = loadButtonPrefab.transform.parent;

                                RectTransform rectTransform = slot.gameObject.GetComponent<RectTransform>();
                                rectTransform.anchoredPosition = loadButtonPrefab.GetComponent<RectTransform>().anchoredPosition - new Vector2(0, (rectTransform.sizeDelta.y + 10) * (saveSlots.Count - 1));

                                Text text = slot.gameObject.GetComponentInChildren<Text>();
                                string[] fileNameSplit = saveFile.Split('\\');

                                text.text = fileNameSplit[fileNameSplit.Length - 1].Remove(fileNameSplit[fileNameSplit.Length - 1].Length - 3);

                                int count = validFiles.Count;

                                Debug.Log("validFiles.Count:" + validFiles.Count);

                                Debug.Log("Loaded Save:" + text.text);
                                validFiles.Add(saveFile);
                            }
                        }
                    }
                }
            }
        }

        for (int i = 0; i < saveSlots.Count; i++)
        {
            UIConnection button = saveSlots[i].gameObject.GetComponent<UIConnection>();
            string location = saveSlots[i].location;
            button.GetComponent<Button>().onClick.AddListener(delegate { LoadLevel(location); });

            if (i == 0)
            {
                backButton.OnDown = button;
                button.OnUp = backButton;
                button.priority = true;
            }

            if (i < saveSlots.Count - 1)
            {
                UIConnection nextButton = saveSlots[i + 1].gameObject.GetComponent<UIConnection>();
                button.OnDown = nextButton;
                nextButton.OnUp = button;
            }
            else
            {
                button.OnDown = backButton;
                backButton.OnUp = button;
            }
        }

        if(saveSlots.Count == 0)
        {
            backButton.priority = true;
        }
        else
        {
            backButton.priority = false;
        }

        scrollView.GetComponent<RectTransform>().sizeDelta = new Vector2(scrollView.GetComponent<RectTransform>().sizeDelta.x, (saveSlots.Count * 110) + 75);

        loadButtonPrefab.SetActive(false);
    }

    private void LoadLevel(string _fileName)
    {
        FindObjectOfType<SaveDataManager>().LoadLevelSave("Gameplay_Test", _fileName);

        mainMenu.SetActive(false);
        loadScreen.SetActive(false);
    }


}
