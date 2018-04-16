using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public bool lockPause = false;
    public static bool isPaused { get; private set; }
    public bool isSaving = false;

    private bool isAnimating = false;

    private RectTransform rectTransform;
    private CameraSwap cameraSwap;

    public GameObject pauseMenu, saveMenu, loadMenu, saveButtonPrefab, loadButtonPrefab;
    public EventSystem eventSystem;
    public InputField inputField;
    public UIConnection saveAsNewFile;

    private bool pastPlayerMovements, pastPlayerIsThinking;

    struct SaveSlot
    {
        public string location;
        public GameObject gameObject;
    }
    private List<SaveSlot> saveSlots;
    private List<string> validFiles;

    private void Start()
    {
        rectTransform = pauseMenu.GetComponent<RectTransform>();

        //Hide Pause off screen
        rectTransform.anchoredPosition = new Vector2(0f, 1000f);

        cameraSwap = FindObjectOfType<CameraSwap>();
    }

    // Update is called once per frame
    void Update ()
    {
        //Do the Pause
        if(InputManager.controllers.Count > 0 && !lockPause)
        {
            InputDevice controller = InputManager.controllers[0];

            if (!isAnimating && !isSaving)
            {
                if (controller.GetButtonWithLock("Pause"))
                {
                    isAnimating = true;
                    isPaused = !isPaused;

                    //Freeze Time 
                    if(isPaused)
                    {
                        StartCoroutine(ShowPauseMenu());
                        pastPlayerMovements = FindObjectOfType<PlayerMovement>().lockMovements;
                        pastPlayerIsThinking = FindObjectOfType<PlayerMovement>().isThinking;

                        if (!pastPlayerMovements)
                        {
                            FindObjectOfType<PlayerMovement>().lockMovements = true;
                        }
                        if(pastPlayerIsThinking)
                        {
                            FindObjectOfType<BuildingModeManager>().isLocked = true;
                        }

                        FindObjectOfType<IsoCam>().lockCamera = true;
                    }
                    else
                    {
                        StartCoroutine(HidePauseMenu());

                        FindObjectOfType<PlayerMovement>().lockMovements = pastPlayerMovements;
                        FindObjectOfType<PlayerMovement>().isThinking = pastPlayerIsThinking;

                        if (pastPlayerIsThinking)
                        {
                            FindObjectOfType<BuildingModeManager>().isLocked = false;
                        }

                        FindObjectOfType<IsoCam>().lockCamera = false;
                    }
                }
            }
        }		
	}

    public void LoadOptionState()
    {
    }

    private IEnumerator ShowPauseMenu()
    {
        eventSystem.enabled = false;
        pauseMenu.SetActive(true);
        LoadOptionState();

        yield return SlideTo(new Vector2(0f, 1000f),    new Vector2(0f, -50f),  0.45f);
        yield return SlideTo(new Vector2(0f, -50f),     new Vector2(0f, 0f),    0.1f);

        isAnimating = false;
        eventSystem.enabled = true;
    }

    private IEnumerator HidePauseMenu()
    {
        eventSystem.enabled = false;
        yield return SlideTo(new Vector2(0f, 0f),   new Vector2(0f, 50f),       0.1f);
        yield return SlideTo(new Vector2(0f, 50f),  new Vector2(0f, -1000f),    0.45f);
        
        isAnimating = false;
        pauseMenu.SetActive(false);
        eventSystem.enabled = true;
    } 

    private IEnumerator SlideTo(Vector2 _start, Vector2 _end, float _travelTime)
    {
        rectTransform.anchoredPosition = _start;

        float startTime = Time.unscaledTime;
        while(Time.unscaledTime - startTime < _travelTime)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(_start, _end, (Time.unscaledTime - startTime) / _travelTime);
            yield return null;
        }

        rectTransform.anchoredPosition = _end;
    }

    public void QuitGame()
    {
        FindObjectOfType<Transitions>().DoWave(true);
        FindObjectOfType<SaveDataManager>().ReturnToMenu();
    }

    public void ShowSaveSlots()
    {
        saveMenu.SetActive(true);
        pauseMenu.SetActive(false);
        isSaving = true;
        UpdateSaveList(true);
    }

    public void ShowLoadSlots()
    {
        loadMenu.SetActive(true);
        pauseMenu.SetActive(false);
        isSaving = true;
        UpdateSaveList(false);
    }

    public void BackFromSave()
    {
        saveMenu.SetActive(false);
        loadMenu.SetActive(false);
        pauseMenu.SetActive(true);
        isSaving = false;
    }


    void UpdateSaveList(bool isSave)
    {
        if (saveSlots != null)
        {
            foreach (SaveSlot save in saveSlots)
                Destroy(save.gameObject);
        }

        GameObject menu = isSave ? saveMenu : loadMenu;
        GameObject buttonPrefab = isSave ? saveButtonPrefab : loadButtonPrefab;

        buttonPrefab.SetActive(true);

        saveSlots = new List<SaveSlot>();

        GameObject scrollView = menu.GetComponentInChildren<ScrollRect>().content.gameObject;

        string[] allSaveFiles = Directory.GetFiles(Application.persistentDataPath);
        validFiles = new List<string>();

        UIConnection backButton = menu.transform.Find("Back").GetComponent<UIConnection>();

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
                                slot.gameObject = Instantiate(buttonPrefab, transform);
                                saveSlots.Add(slot);

                                slot.gameObject.transform.SetParent(buttonPrefab.transform.parent);

                                RectTransform rectTransform = slot.gameObject.GetComponent<RectTransform>();
                                rectTransform.anchoredPosition = buttonPrefab.GetComponent<RectTransform>().anchoredPosition - new Vector2(0, (rectTransform.sizeDelta.y + 10) * (saveSlots.Count - 1));

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

            if (!isSave)
            {
                button.GetComponent<Button>().onClick.AddListener(delegate { LoadLevel(location); });
            }
            else
            {
                button.GetComponent<Button>().onClick.AddListener(delegate { OverwriteSave(location); });
            }

            if (i == 0)
            {          
                if (isSave)
                {
                    saveAsNewFile.OnDown = button;
                    button.OnUp = saveAsNewFile;
                    backButton.OnDown = inputField.GetComponent<UIConnection>();
                }
                else
                {
                    backButton.OnDown = button;
                    button.priority = true;
                    button.OnUp = backButton;
                }
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

        if (!isSave)
        {
            if (saveSlots.Count == 0)
            {
                backButton.priority = true;
            }
            else
            {
                backButton.priority = false;
            }
        }

        scrollView.GetComponent<RectTransform>().sizeDelta = new Vector2(scrollView.GetComponent<RectTransform>().sizeDelta.x, (saveSlots.Count * 110f) + 75f);

        buttonPrefab.SetActive(false);
    }

    private void LoadLevel(string _fileName)
    {
        FindObjectOfType<Transitions>().DoWave(true);
        FindObjectOfType<SaveDataManager>().LoadLevelSave("Gameplay_Test", _fileName);

        loadMenu.SetActive(false);
        saveMenu.SetActive(false);
        isPaused = false;
    }

    private void OverwriteSave(string _fileName)
    {
        FindObjectOfType<SaveDataManager>().Save(_fileName);
        BackFromSave();
    }

    public void SaveToNewSlot()
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
                FindObjectOfType<SaveDataManager>().Save(Application.persistentDataPath + "\\" + inputField.text + ".gd");
                BackFromSave();
            }
        }
    }
}
