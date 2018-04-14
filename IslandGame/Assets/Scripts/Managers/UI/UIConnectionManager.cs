using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIConnectionManager : MonoBehaviour {

    public int currentUI;

    public UIConnection currentUIConnection;
    private List<UIConnection> allConnectors;
    private EventSystem eventSystem;

    public int playerCanUse = 0;//0 = All, 1 = Player 1, 2 = Player 2 .etc

    public bool submitInput { get; private set; }
    public bool cancelInput { get; private set; }

    // Use this for initialization
    void Awake ()
    {
        allConnectors = new List<UIConnection>();    
    }

    public void AddConnector(UIConnection _uiConnection)
    {
        if (allConnectors != null)
        {
            allConnectors.Add(_uiConnection);
        }
    }

    public void RemoveConnector(UIConnection _uiConnection)
    {
        if (allConnectors != null)
        {
            allConnectors.Remove(_uiConnection);
        }
    }

    private static bool IsNull(UIConnection s)
    {
        return s == null;
    }

    // Update is called once per frame
    void Update ()
    {       
        currentUI = allConnectors.Count;

        //Unselect Option if it becomes unavailable
        if(currentUIConnection != null && !currentUIConnection.isActiveAndEnabled)
        {
            currentUIConnection = null;
        }

        //Clear all Null Button
        allConnectors.RemoveAll(IsNull);

        bool mouseOnAnything = false;
        foreach (UIConnection uiConnector in allConnectors.ToArray())
        {
            RectTransform rectTransform = uiConnector.GetComponent<RectTransform>();
            Button button = uiConnector.GetComponent<Button>();
            Toggle toggle = uiConnector.GetComponent<Toggle>();
            InputField inputField = uiConnector.GetComponent<InputField>();

            //Do Open and Close if Button
            if (uiConnector.isActiveAndEnabled && rectTransform != null && (button != null || inputField != null || toggle != null))
            {
                //Set as default
                if(currentUIConnection == null && uiConnector.priority)
                {
                    currentUIConnection = uiConnector;
                }
         
                if (Cursor.visible && (playerCanUse == 0 || (playerCanUse-1) == InputManager.GetMousePlayer()))
                {
                    if (RectTransformUtility.RectangleContainsScreenPoint(rectTransform, Input.mousePosition, Camera.main))
                    {
                        currentUIConnection = uiConnector;
                        mouseOnAnything = true;
                    }
                }
                
                if(currentUIConnection == uiConnector)
                {
                    uiConnector.OnSelected();                  
                }
                else
                {
                    uiConnector.OnUnSelected();
                }
            }
        }

        submitInput = false;
        cancelInput = false;

        if(eventSystem == null)
        {
            eventSystem = FindObjectOfType<EventSystem>();
        }
        else if(eventSystem.isActiveAndEnabled)         
        {
            //Do Input
            for (int i = 0; i < InputManager.controllers.Count; i++)
            {
                if (playerCanUse == 0 || playerCanUse == i + 1)
                {
                    InputDevice player = InputManager.controllers[i];

                    if (currentUIConnection != null)
                    {
                        if (player.GetRawButtonWithLock("Submit", true))
                        {
                            if (currentUIConnection.GetComponent<Button>() != null)
                            {
                                currentUIConnection.GetComponent<Button>().onClick.Invoke();
                            }

                            if (currentUIConnection.GetComponent<Toggle>() != null)
                            {
                                currentUIConnection.GetComponent<Toggle>().onValueChanged.Invoke(!currentUIConnection.GetComponent<Toggle>().isOn);
                            }

                            currentUIConnection.OnClicked(i);
                            submitInput = true;
                        }

                        if (player.GetRawButtonWithLock("Cancel", true))
                        {
                            cancelInput = true;
                        }

                        if (currentUIConnection.GetComponent<InputField>() != null && mouseOnAnything && player.inputType == InputType.Keyboard && InputManager.GetClick(0))
                        {
                            currentUIConnection.OnClicked(i);
                        }

                        if (!mouseOnAnything)
                        {
                            int hori = player.GetRawIntInputWithDelay("MenuHorizontal", 0.25f, Time.unscaledDeltaTime);
                            int vert = player.GetRawIntInputWithDelay("MenuVertical", 0.25f, Time.unscaledDeltaTime);

                            //Navigation
                            if (hori > 0 && currentUIConnection.OnRight != null)
                            {
                                currentUIConnection = currentUIConnection.OnRight;
                            }

                            if (hori < 0 && currentUIConnection.OnLeft != null)
                            {
                                currentUIConnection = currentUIConnection.OnLeft;
                            }

                            if (vert < 0 && currentUIConnection.OnUp != null)
                            {
                                currentUIConnection = currentUIConnection.OnUp;
                            }

                            if (vert > 0 && currentUIConnection.OnDown != null)
                            {
                                currentUIConnection = currentUIConnection.OnDown;
                            }
                        }
                    }
                }
            }
        }
    }
}
