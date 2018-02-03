using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused { get; private set; }
    private bool isAnimating = false;

    private RectTransform rectTransform;
    private CameraSwap cameraSwap;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();

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

            if (!isAnimating)
            {
                if (controller.GetButtonWithLock("Pause"))
                {
                    isAnimating = true;
                    isPaused = !isPaused;

                    //Freeze Time 
                    if(isPaused)
                    {
                        Time.timeScale = 0f;
                        StartCoroutine(ShowPauseMenu());
                    }
                    else
                    {
                        Time.timeScale = 1f;
                        StartCoroutine(HidePauseMenu());
                    }
                }
            }
        }		
	}

    public void LoadOptionState()
    {

        //Setup Invert Toggles
        Toggle invertVertical = transform.Find("InvertVertical_Toggle").GetComponent<Toggle>();
        Toggle invertHorizontal = transform.Find("InvertHorizontal_Toggle").GetComponent<Toggle>();

        invertVertical.isOn = cameraSwap.GetInvertVertical();
        invertHorizontal.isOn = cameraSwap.GetInvertHorizontal();
    }

    private IEnumerator ShowPauseMenu()
    {
        LoadOptionState();

        yield return SlideTo(new Vector2(0f, 1000f),    new Vector2(0f, -50f),  0.45f);
        yield return SlideTo(new Vector2(0f, -50f),     new Vector2(0f, 0f),    0.1f);

        isAnimating = false;
    }

    private IEnumerator HidePauseMenu()
    {
        yield return SlideTo(new Vector2(0f, 0f),   new Vector2(0f, 50f),       0.1f);
        yield return SlideTo(new Vector2(0f, 50f),  new Vector2(0f, -1000f),    0.45f);
        
        isAnimating = false;
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
}
