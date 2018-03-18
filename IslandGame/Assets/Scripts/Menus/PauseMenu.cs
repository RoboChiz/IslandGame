using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused { get; private set; }
    public bool isSaving = false;

    private bool isAnimating = false;

    private RectTransform rectTransform;
    private CameraSwap cameraSwap;

    public GameObject pauseMenu, saveSlots;
    public EventSystem eventSystem;

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
        if(InputManager.controllers.Count > 0)
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
                        FindObjectOfType<PlayerMovement>().lockMovements = true;
                        FindObjectOfType<IsoCam>().lockCamera = true;
                    }
                    else
                    {
                        StartCoroutine(HidePauseMenu());
                        FindObjectOfType<PlayerMovement>().lockMovements = false;
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
        Application.Quit();
    }

    public void ShowSaveSlots()
    {
        saveSlots.SetActive(true);
        pauseMenu.SetActive(false);
        isSaving = true;
    }
}
